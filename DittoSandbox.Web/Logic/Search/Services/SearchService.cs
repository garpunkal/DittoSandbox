using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DittoSandbox.Web.Logic.Search.Config;
using DittoSandbox.Web.Logic.Search.ModelBuilders;
using DittoSandbox.Web.Logic.Search.Models;
using Examine;
using Examine.SearchCriteria;
using Umbraco.Core.Logging;
using Umbraco.Web;

namespace DittoSandbox.Web.Logic.Search.Services
{
    public class SearchService
    {
        private readonly SearchConfig _config;
        private readonly Func<SearchResultModelBuilder> _searchResultModelBuilder;
        private readonly UmbracoHelper _helper;

        public SearchService(Func<SearchResultModelBuilder> searchResultModelBuilder, SearchConfig config)
        {
            _searchResultModelBuilder = searchResultModelBuilder;
            _config = config;

            _helper = new UmbracoHelper(UmbracoContext.Current);
        }

        public SearchViewModel Search(SearchViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.SearchTerm)) return model;

            SearchResultModelBuilder modelBuilder = _searchResultModelBuilder();

            // Build the query
            StringBuilder query = modelBuilder.BuildQuery(model);

            // Perform the search
            var results = PerformSearch(query, model);

            // complete model
            model.AllResults = results
                .Select(x => modelBuilder.BuildViewModels(x, model))
                .ToList();

            model.TotalResults = results.Count;
            model.TotalPages = (int)Math.Ceiling((decimal)model.TotalResults / model.PageSize);
            model.CurrentPage = Math.Max(1, Math.Min(model.TotalPages, model.CurrentPage));
            model.PagedResults = model.AllResults.Skip(model.PageSize * (model.CurrentPage - 1)).Take(model.PageSize);

            LogHelper.Debug<string>("[Search] Searching Lucene with the following query: " + query);
            
            return model;
        }

        public List<SearchResult> PerformSearch(StringBuilder query, SearchViewModel model)
        {
            var searcher = ExamineManager.Instance.SearchProviderCollection[_config.SearchSearcher];
            var criteria = searcher.CreateSearchCriteria();

            ISearchCriteria criteria2 = criteria.RawQuery(query.ToString());

            var results = searcher.Search(criteria2)
                .Where(x => (
                                !_helper.IsProtected(x.Fields[StaticValues.Properties.Path]) || (_helper.IsProtected(x.Fields[StaticValues.Properties.Path]) && _helper.MemberHasAccess(x.Fields[StaticValues.Properties.Path]))) &&
                            (
                                (x.Fields[StaticValues.Properties.__IndexType] == UmbracoExamine.IndexTypes.Content && _helper.TypedContent(int.Parse(x.Fields[StaticValues.Properties.Id])) != null) ||
                                (x.Fields[StaticValues.Properties.__IndexType] == UmbracoExamine.IndexTypes.Media && _helper.TypedMedia(int.Parse(x.Fields[StaticValues.Properties.Id])) != null)
                            ))
                .OrderByDescending(x => x.Score)
                .ToList();

            return results;
        }
    }
}