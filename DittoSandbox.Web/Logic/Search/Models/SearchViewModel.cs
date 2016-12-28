using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Examine;
using Our.Umbraco.Ditto;

namespace DittoSandbox.Web.Logic.Search.Models
{
    public class SearchViewModel
    {
        public string SearchTerm { get; set; }
        public IEnumerable<string> SearchTerms { get; set; }
        public int CurrentPage { get; set; }

        // Options
        public int RootContentNodeId { get; set; }
        public int RootMediaNodeId { get; set; }
        public Enums.IndexTypes IndexType { get; set; }
        public IList<string> SearchFields { get; set; }
        public IList<string> PreviewFields { get; set; }
        public int PageSize { get; set; }
        public string HideFromSearchField { get; set; }
        public Enums.SearchFormLocation SearchFormLocation { get; set; }

        // Results
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }

        public IEnumerable<SearchResultViewModel> AllResults { get; set; }
        public IEnumerable<SearchResultViewModel> PagedResults { get; set; }

        // Methods
        public int StartRecord => ((CurrentPage - 1) * PageSize) + 1;
        public int EndRecord => Math.Min(TotalResults, (StartRecord - 1) + PageSize);
        public bool HasSearchTerm => !string.IsNullOrWhiteSpace(SearchTerm);
        public bool FormLocationTop => SearchFormLocation == Enums.SearchFormLocation.Top || SearchFormLocation == Enums.SearchFormLocation.Both;
        public bool FormLocationBottom => SearchFormLocation == Enums.SearchFormLocation.Bottom || SearchFormLocation == Enums.SearchFormLocation.Both;
        public bool FormLocationNotNone => SearchFormLocation != Enums.SearchFormLocation.None;

        // Dictionary Labels
        [UmbracoDictionary(StaticValues.Dictionary.Search.NoResultsLabel)]
        public string NoResultsLabel { get; set; } = "No results found for search term <strong>{0}</strong>.";

        [UmbracoDictionary(StaticValues.Dictionary.Search.SearchLabel)]
        public string SearchLabel { get; set; } = "Search";

        [UmbracoDictionary(StaticValues.Dictionary.Search.PreviousLabel)]
        public string PreviousLabel { get; set; } = "Previous";

        [UmbracoDictionary(StaticValues.Dictionary.Search.NextLabel)]
        public string NextLabel { get; set; } = "Next";

        [UmbracoDictionary(StaticValues.Dictionary.Search.ShowingResultsLabel)]
        public string ShowingResultsLabel { get; set; } = "Showing results <strong>{0}</strong> to <strong>{1}</strong>.";

        [UmbracoDictionary(StaticValues.Dictionary.Search.YouSearchedForLabel)]
        public string YourSearchForLabel { get; set; } = "Your search for <strong>\"{0}\"</strong> matched <strong>{1}</strong> page(s).";
    }
}