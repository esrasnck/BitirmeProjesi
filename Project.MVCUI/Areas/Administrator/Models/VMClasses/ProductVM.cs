using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Areas.Administrator.Models.VMClasses
{
    public class ProductVM
    {
        ProductCategoryRepository crep;

        ProductFeatureRepository frep;

        public ProductVM()
        {
            crep = new ProductCategoryRepository();

            frep = new ProductFeatureRepository();

            
            //Categories = crep.GetActives();

            //Features = frep.GetActives();

            SelectedCategory = new List<ProductCategory>();

            SelectedFetaure = new List<ProductFeature>();
        }

        public Product Product { get; set; }

        //public List<Category> Categories { get;}

        //public List<Feature> Features { get;}

        public string imagePath { get; set; }

        public List<ProductCategory> SelectedCategory { get; set; }

        public List<ProductFeature> SelectedFetaure { get; set; }
    }
}