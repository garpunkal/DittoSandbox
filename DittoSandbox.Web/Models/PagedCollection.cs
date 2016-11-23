﻿using System.Collections.Generic;

namespace DittoSandbox.Web.Models
{
    public class PagedCollection
    {
        public long TotalItems { get; set; }
        public long CurrentPage { get; set; }
        public long PageSize { get; set; }
        public long TotalPages { get; set; }
        public bool IsFirstPage => CurrentPage <= 1;
        public bool IsLastPage => CurrentPage >= TotalPages;
    }

    public class PagedCollection<TResultType> : PagedCollection
    {
        public IEnumerable<TResultType> Items { get; set; }
    }
}