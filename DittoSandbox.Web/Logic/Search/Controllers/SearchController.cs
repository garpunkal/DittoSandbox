using System;
using System.Web.ModelBinding;
using System.Web.Mvc;
using DittoSandbox.Web.Logic.Search.ModelBuilders;
using DittoSandbox.Web.Logic.Search.Models;
using DittoSandbox.Web.Logic.Search.Services;
using Our.Umbraco.Ditto;
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
            var requestModel = searchFilterBuilder.BuildViewModel(request, model?.Content?.AncestorOrSelf(1)?.Id);

            return CurrentView(searchService.Search(requestModel));
        }
    }
}