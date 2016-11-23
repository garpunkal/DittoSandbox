using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DittoSandbox.Web.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNullOrEmptyString(this object value)
        {
            return value == null || value as string == string.Empty;
        }
    }
}