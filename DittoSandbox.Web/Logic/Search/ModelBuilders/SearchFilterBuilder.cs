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
        
        public SearchViewModel BuildViewModel(SearchRequestModel request, int? topAncestorId)
        {
            var model = new SearchViewModel
            {
                SearchTerm = request.Query != null ? _helper.CleanseSearchTerm(request.Query.ToLower(CultureInfo.InvariantCulture)) : string.Empty,
                CurrentPage = request.Page,
                PageSize = _config.PageSize,
                RootContentNodeId = _config.RootContentNodeId,
                RootMediaNodeId = _config.RootMediaNodeId,
                HideFromSearchField = _config.HideFromSearchField,
                SearchFields = _config.SearchFields.SplitToList(),
                FuzzyValue = decimal.Parse(_config.FuzzySearch),
                IndexType = SearchHelpers.EnumTryParse<Enums.IndexTypes>(_config.IndexType) ?? Enums.IndexTypes.Both,
                SearchFormLocation = SearchHelpers.EnumTryParse<Enums.SearchFormLocation>(_config.FormLocation) ?? Enums.SearchFormLocation.Bottom,
            };
            
            if (model.RootContentNodeId <= 0 && topAncestorId.HasValue)
                model.RootContentNodeId = topAncestorId.Value;

            if (model.SearchFields.Contains(StaticValues.Properties.UmbracoFile) && !model.SearchFields.Contains(StaticValues.Properties.UmbracoFileName))
                model.SearchFields.Add(StaticValues.Properties.UmbracoFileName);

            model.SearchTerms = SearchHelpers.Tokenize(model.SearchTerm);

            return model;
        }
    }
}