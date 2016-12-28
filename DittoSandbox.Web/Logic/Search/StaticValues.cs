using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DittoSandbox.Web.Logic.Search
{
    public class StaticValues
    {
        public class Properties
        {
            public const string UmbracoNaviHide = "umbracoNaviHide";
            public const string UmbracoFile = "umbracoFile";
            public const string UmbracoFileName = "umbracoFileName";
            public const string Path = "path";
            public const string SearchPath = "searchPath";
            public const string Contents = "contents";
            public const string Id = "id";
            public const string __IndexType = "__IndexType";

            public const string Extract = "extract";
            public const string BodyText = "bodyText";
            public const string Name = "name";
        }

        public class Dictionary
        {
            public class Search
            {
                public const string NoResultsLabel = "Search.NoResultsLabel";
                public const string SearchLabel = "Search.SearchLabel";
                public const string PreviousLabel = "Search.PreviousLabel";
                public const string NextLabel = "Search.NextLabel";
                public const string ShowingResultsLabel = "Search.ShowingResultsLabel";
                public const string YouSearchedForLabel = "Search.YouSearchedForLabel";
            }
        }
    }
}