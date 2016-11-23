﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DittoSandbox.Web.Models;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace DittoSandbox.Web.Extensions
{
    public static class PublishedContentExtensions
    {
        public static object Get(this IPublishedContent content,
            string propertyAlias,
            bool recursive = false,
            object defaultValue = null)
        {
            return content.GetPropertyValue(propertyAlias, recursive, defaultValue);
        }

        public static T Get<T>(this IPublishedContent content,
            string propertyAlias,
            bool recursive = false,
            T defaultValue = default(T))
        {
            if (content == null)
                return defaultValue;

            return content.GetPropertyValue<T>(propertyAlias, recursive, defaultValue);
        }
    }
}