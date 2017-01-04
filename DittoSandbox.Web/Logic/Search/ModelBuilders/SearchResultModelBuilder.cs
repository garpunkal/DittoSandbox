using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DittoSandbox.Web.Logic.Search.Config;
using DittoSandbox.Web.Logic.Search.Models;
using Examine;
using Examine.SearchCriteria;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace DittoSandbox.Web.Logic.Search.ModelBuilders
{
    public class SearchResultModelBuilder
    {
        private readonly UmbracoHelper _helper;

        public SearchResultModelBuilder()
        {
            _helper = new UmbracoHelper(UmbracoContext.Current);
        }

        public StringBuilder BuildQuery(SearchViewModel model)
        {
            var query = new StringBuilder();

            query.Append(AddHiddenSearchField(model));
            query.Append(SetPathFilters(model));
            query.Append(BuildSearchTerms(model));
            query.Append(RankContent(model));

            return query;
        }
        
        public SearchResultViewModel BuildViewModel(SearchResult result, IEnumerable<string> searchTerms)
        {
            var viewModel = new SearchResultViewModel
            {
                IndexType = result?.Fields[StaticValues.Properties.IndexType],
                SearchTerms = searchTerms
            };

            switch (viewModel.IndexType)
            {
                case UmbracoExamine.IndexTypes.Content:
                    IPublishedContent content = _helper.TypedContent(result?.Fields[StaticValues.Properties.Id]);
                    viewModel.Content = content?.As<Link>();
                    break;

                case UmbracoExamine.IndexTypes.Media:
                    IPublishedContent media = _helper.TypedMedia(result?.Fields[StaticValues.Properties.Id]);
                    viewModel.Media = media?.As<Image>();
                    break;
            }
            return viewModel;
        }

        private string AddHiddenSearchField(SearchViewModel model)
        {
            // add hidden search field
            return $"-{model.HideFromSearchField}:1 ";
        }

        private string SetPathFilters(SearchViewModel model)
        {
            // Set search path
            var query = new StringBuilder();

            var contentPathFilter = model.RootContentNodeId > 0
                ? $"{StaticValues.Properties.IndexType}:{UmbracoExamine.IndexTypes.Content} +searchPath:{model.RootContentNodeId} -template:0"
                : $"{StaticValues.Properties.IndexType}:{UmbracoExamine.IndexTypes.Content} -template:0";

            var mediaPathFilter = model.RootMediaNodeId > 0
                ? $"{StaticValues.Properties.IndexType}:{UmbracoExamine.IndexTypes.Media} +searchPath:{model.RootMediaNodeId} -__NodeTypeAlias:folder"
                : $"{StaticValues.Properties.IndexType}:{UmbracoExamine.IndexTypes.Media} -__NodeTypeAlias:folder";

            switch (model.IndexType.ToString().ToLower())
            {
                case UmbracoExamine.IndexTypes.Content:
                    query.Append($"+({contentPathFilter}) ");
                    break;
                case UmbracoExamine.IndexTypes.Media:
                    query.Append($"+({mediaPathFilter}) ");
                    break;
                default:
                    query.Append($"+(({contentPathFilter}) ({mediaPathFilter})) ");
                    break;
            }

            return query.ToString();
        }

        private string RankContent(SearchViewModel model)
        {
            // Rank content based on positon of search terms in fields
            var query = new StringBuilder();

            for (var i = 0; i < model.SearchFields.Count; i++)
                foreach (string term in model.SearchTerms)
                {
                    //check if phrase or keyword
                    bool isPhraseTerm = term.IndexOf(' ') != -1; //contains space - is phrase
                    if (isPhraseTerm)
                    {
                        query.Append($@"{model.SearchFields[i]}:""{term}""^{model.SearchFields.Count - i} ");
                        if (model.IsFuzzySearch) query.Append($@"OR {model.SearchFields[i]}:""{term}""~{model.FuzzyValue} ");
                    }
                    else
                    {
                        query.Append($"{model.SearchFields[i]}:{term}*^{model.SearchFields.Count - i} ");
                        if (model.IsFuzzySearch) query.Append($"OR {model.SearchFields[i]}:{term}~{model.FuzzyValue} ");
                    }
                }

            return query.ToString();
        }

        private string BuildSearchTerms(SearchViewModel model)
        {
            // Ensure page contains all search terms in some way
            var query = new StringBuilder();

            foreach (string term in model.SearchTerms)
            {
                var groupedOr = new StringBuilder();
                foreach (string searchField in model.SearchFields)
                {
                    //check if phrase or keyword
                    bool isPhraseTerm = term.IndexOf(' ') != -1; //contains space - is phrase
                    if (isPhraseTerm)
                    {
                        groupedOr.Append($@"{searchField}:""{term}"" ");
                        if (model.IsFuzzySearch) groupedOr.Append($@"OR {searchField}:""{term}""~{model.FuzzyValue} ");
                    }
                    else
                    {
                        groupedOr.Append($"{searchField}:{term}* ");
                        if (model.IsFuzzySearch) groupedOr.Append($"OR {searchField}:{term}~{model.FuzzyValue} ");
                    }

                }
                query.Append("+(" + groupedOr + ") ");
            }

            return query.ToString();
        }

    }
}