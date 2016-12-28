using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DittoSandbox.Web.Logic.Search
{
    public class Enums
    {
        public enum IndexTypes
        {
            Both, 
            Content, 
            Media
        }


        public enum SearchFormLocation
        {
            None,
            Top, 
            Bottom, 
            Both
        }
    }
}