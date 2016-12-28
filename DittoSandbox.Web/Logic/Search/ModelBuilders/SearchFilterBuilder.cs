using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DittoSandbox.Web.Logic.Search.Config;
using DittoSandbox.Web.Logic.Search.Extensions;
using DittoSandbox.Web.Logic.Search.Helpers;
using DittoSandbox.Web.Logic.Search.Models;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace DittoSandbox.Web.Logic.Search.ModelBuilders
{
    public class SearchFilterBuilder
    {
        private readonly SearchConfig _config;
        private readonly UmbracoHelper _helper;

        public SearchFilterBuilder(SearchConfig config)
        {
            _helper = new UmbracoHelper(UmbracoContext.Current);
            _config = config;
        }
        
        public SearchViewModel BuildViewModels(SearchRequestModel request, int? topAncestorId)
        {
            var model = new SearchViewModel
            {
                SearchTerm = request.Query != null ? _helper.CleanseSearchTerm(request.Query.ToLower(CultureInfo.InvariantCulture)) : string.Empty,
                CurrentPage = request.Page,
                PageSize = request.PageSize != 0 ? request.PageSize : _config.PageSize,
                RootContentNodeId = request.RootContentNodeId,
                RootMediaNodeId = request.RootMediaNodeId,
                IndexType = !string.IsNullOrEmpty(request.IndexType) ? request.IndexType.ToLower(CultureInfo.InvariantCulture) : string.Empty,
                SearchFields = request.SearchFields?.Any() ?? false ? request.SearchFields : _config.SearchFields.SplitToList(),
                HideFromSearchField = request.HideFromSearchField,
                SearchFormLocation = SearchHelpers.GetFormLocation(request.FormLocation)
            };

            if (model.IndexType != UmbracoExamine.IndexTypes.Content && model.IndexType != UmbracoExamine.IndexTypes.Media)
                model.IndexType = string.Empty;

            if (model.RootContentNodeId <= 0 && topAncestorId.HasValue)
                model.RootContentNodeId = topAncestorId.Value;

            if (model.SearchFields.Contains(StaticValues.Properties.UmbracoFile) && !model.SearchFields.Contains(StaticValues.Properties.UmbracoFileName))
                model.SearchFields.Add(StaticValues.Properties.UmbracoFileName);

            model.SearchTerms = SearchHelpers.Tokenize(model.SearchTerm);

            return model;
        }
    }
}