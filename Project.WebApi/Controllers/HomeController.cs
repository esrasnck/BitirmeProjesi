using Project.BLL.DesignPatterns.SingletonPattern;
using Project.DAL.Context;
using Project.MODEL.Entities;
using Project.WebApi.Models.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.WebApi.Controllers
{
    public class HomeController : ApiController
    {
        MyContext db;
        public HomeController()
        {
            db = DBTool.DBInstance;
        }

        public HttpResponseMessage GetProducts()
        {
            List<ProductVM> productVMlist = new List<ProductVM>();

            foreach (ProductCategory item in db.ProductCategories.ToList())
            {
                ProductVM pvm = new ProductVM();
                pvm.CategoryName = item.Category.CategoryName;
                pvm.ProductName = item.Product.ProductName;
                //pvm.FeatureName = item.Product.ProductFeatures.FirstOrDefault().Feature.FeatureName;
                //pvm.Value = item.Product.ProductFeatures.FirstOrDefault().Value;
                pvm.ImagePath = item.Product.ImagePath;
                pvm.UnitPrice = item.Product.UnitPrice;
                productVMlist.Add(pvm);      // müsait olduğunda üstteki mevzuyu furkana sor!!!
            }

            return Request.CreateResponse(HttpStatusCode.OK, productVMlist);
        }


        // bu kısım da bitti ama emin olmak için maalesef web kısmını yazmak için bekleyeceğiz:(
        [HttpPost]

        public HttpResponseMessage Login(AppUserVM item)
        {

            if (item != null && db.AppUsers.Any(x => x.UserName == item.UserName && x.Password == item.Password))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Welcome");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Not-Welcome");
            }
        }
    }
}
