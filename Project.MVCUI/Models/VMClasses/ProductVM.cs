using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.VMClasses
{
    public class ProductVM
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImagePath { get; set; }
        public string CategoryName { get; set; }
        public string FeatureName { get; set; }
        public string Value { get; set; }
    }
}