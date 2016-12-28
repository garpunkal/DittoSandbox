using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using DittoSandbox.Web.Logic.Search.Config;
using DittoSandbox.Web.Logic.Search.Extensions;
using DittoSandbox.Web.Logic.Search.Helpers;
using DittoSandbox.Web.Logic.Search.ModelBuilders;
using DittoSandbox.Web.Logic.Search.Models;
using DittoSandbox.Web.Logic.Search.Services;
using Our.Umbraco.Ditto;
using Umbraco.Core.Logging;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace DittoSandbox.Web.Logic.Search.Controllers
{
    public class SearchController : DittoController
    {
        private readonly Func<SearchService> _searchService;
        private readonly Func<SearchFilterBuilder> _searchModelBuilder;

        public SearchController(Func<SearchService> searchService, Func<SearchFilterBuilder> searchModelBuilder)
        {
            _searchService = searchService;
            _searchModelBuilder = searchModelBuilder;
        }

        public ActionResult Index(RenderModel model, [QueryString] SearchRequestModel request)
        {
            var searchService = _searchService();
            var searchFilterBuilder = _searchModelBuilder(); 

            var requestModel = searchFilterBuilder.BuildViewModels(request, model?.Content?.AncestorOrSelf(1)?.Id);

            return CurrentView(searchService.Search(requestModel));
        }
    }
}