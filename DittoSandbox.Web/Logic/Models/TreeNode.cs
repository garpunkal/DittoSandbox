﻿using System.Collections.Generic;

namespace DittoSandbox.Web.Logic.Models
{
    public class TreeNode
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public List<TreeNode> Children { get; set; }
        public bool NewWindow { get; set; }

        public bool Current { get; set; } 
    }
}