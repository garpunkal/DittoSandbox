﻿using System.Collections.Generic;
using System.Web;
using DittoSandbox.Web.Models.Processors;
using Our.Umbraco.Ditto;

namespace DittoSandbox.Web.Models
{
    public class BaseModel
    {
        [PrimaryNavigation]
        public IEnumerable<TreeNode> NavItems { get; set; } 
    }
}