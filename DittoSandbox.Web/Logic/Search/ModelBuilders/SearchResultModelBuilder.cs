﻿using System;
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

            // add hidden search field
            query.AppendFormat("-{0}:1 ", model.HideFromSearchField);

            // Set search path
            var contentPathFilter = model.RootContentNodeId > 0
                ? $"{StaticValues.Properties.IndexType}:{UmbracoExamine.IndexTypes.Content} +searchPath:{model.RootContentNodeId} -template:0"
                : $"{StaticValues.Properties.IndexType}:{UmbracoExamine.IndexTypes.Content} -template:0";

            var mediaPathFilter = model.RootMediaNodeId > 0
                ? $"{StaticValues.Properties.IndexType}:{UmbracoExamine.IndexTypes.Media} +searchPath:{model.RootMediaNodeId} -__NodeTypeAlias:folder"
                : $"{StaticValues.Properties.IndexType}:{UmbracoExamine.IndexTypes.Media} -__NodeTypeAlias:folder";

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
                    if (isPhraseTerm)
                    {
                        if (model.IsFuzzySearch)
                            groupedOr.Append($@"{searchField}:""{term}"" OR {searchField}:""{term}""~{model.FuzzyValue} ");
                        else
                            groupedOr.Append($@"{searchField}:""{term}"" ");
                    }
                    else
                    {
                        if (model.IsFuzzySearch)
                            groupedOr.Append($"{searchField}:{term}* OR {searchField}:{term}~{model.FuzzyValue} ");
                        else
                            groupedOr.Append($"{searchField}:{term}* ");
                    }

                }
                query.Append("+(" + groupedOr + ") ");
            }

            // Rank content based on positon of search terms in fields
            for (var i = 0; i < model.SearchFields.Count; i++)
                foreach (string term in model.SearchTerms)
                {
                    //check if phrase or keyword
                    bool isPhraseTerm = term.IndexOf(' ') != -1; //contains space - is phrase
                    if (isPhraseTerm)
                    {
                        if (model.IsFuzzySearch)
                            query.Append($@"{model.SearchFields[i]}:""{term}""^{model.SearchFields.Count - i} OR {model.SearchFields[i]}:""{term}""~{model.FuzzyValue} ");
                        else
                            query.Append($@"{model.SearchFields[i]}:""{term}""^{model.SearchFields.Count - i} ");
                    }
                    else
                    {
                        if (model.IsFuzzySearch)
                            query.Append($"{model.SearchFields[i]}:{term}*^{model.SearchFields.Count - i} OR {model.SearchFields[i]}:{term}~{model.FuzzyValue} ");
                        else
                            query.Append($"{model.SearchFields[i]}:{term}*^{model.SearchFields.Count - i} ");
                    }
                }

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
    }
}