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

            // add hidden search field
            query.AppendFormat("-{0}:1 ", model.HideFromSearchField);

            // Set search path
            var contentPathFilter = model.RootContentNodeId > 0
                ? $"{StaticValues.Properties.__IndexType}:{UmbracoExamine.IndexTypes.Content} +searchPath:{model.RootContentNodeId} -template:0"
                : $"{StaticValues.Properties.__IndexType}:{UmbracoExamine.IndexTypes.Content} -template:0";

            var mediaPathFilter = model.RootMediaNodeId > 0
                ? $"{StaticValues.Properties.__IndexType}:{UmbracoExamine.IndexTypes.Media} +searchPath:{model.RootMediaNodeId} -__NodeTypeAlias:folder"
                : $"{StaticValues.Properties.__IndexType}:{UmbracoExamine.IndexTypes.Media} -__NodeTypeAlias:folder";

            switch (model.IndexType.ToString().ToLower())
            {
                case UmbracoExamine.IndexTypes.Content:
                    query.AppendFormat("+({0}) ", contentPathFilter);
                    break;
                case UmbracoExamine.IndexTypes.Media:
                    query.AppendFormat("+({0}) ", mediaPathFilter);
                    break;
                default:
                    query.AppendFormat("+(({0}) ({1})) ", contentPathFilter, mediaPathFilter);
                    break;
            }

            // Ensure page contains all search terms in some way
            foreach (string term in model.SearchTerms)
            {
                var groupedOr = new StringBuilder();
                foreach (string searchField in model.SearchFields)
                {
                    //check if phrase or keyword
                    bool isPhraseTerm = term.IndexOf(' ') != -1; //contains space - is phrase
                    groupedOr.AppendFormat(!isPhraseTerm ? "{0}:{1}* " : @"{0}:""{1}"" ", searchField, term);
                }
                query.Append("+(" + groupedOr + ") ");
            }

            // Rank content based on positon of search terms in fields
            for (var i = 0; i < model.SearchFields.Count; i++)
            {
                foreach (string term in model.SearchTerms)
                {
                    //check if phrase or keyword
                    bool isPhraseTerm = term.IndexOf(' ') != -1; //contains space - is phrase
                    query.AppendFormat(!isPhraseTerm ? "{0}:{1}*^{2} " : @"{0}:""{1}""^{2} ", model.SearchFields[i], term, model.SearchFields.Count - i);
                }
            }

            return query;
        }

      
        public SearchResultViewModel BuildViewModels(SearchResult result, SearchViewModel model)
        {
            var viewModel = new SearchResultViewModel
            {
                IndexType = result?.Fields[StaticValues.Properties.__IndexType],
                SearchTerms = model?.SearchTerms
            };

            switch (viewModel.IndexType)
            {
                case UmbracoExamine.IndexTypes.Content:
                    var content = _helper.TypedContent(result?.Fields[StaticValues.Properties.Id]);
                    viewModel.Content = content?.As<Link>();
                    break;

                case UmbracoExamine.IndexTypes.Media:
                    var media = _helper.TypedMedia(result?.Fields[StaticValues.Properties.Id]);
                    viewModel.Media = media?.As<Image>();
                    break;
            }
            return viewModel;
        }
    }
}