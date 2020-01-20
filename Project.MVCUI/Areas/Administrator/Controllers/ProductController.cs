using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.COMMON.CommonTools;
using Project.MODEL.Entities;
using Project.MODEL.Enums;
using Project.MVCUI.Areas.Administrator.Models.VMClasses;
using Project.MVCUI.AuthenticationClasses;
using Project.MVCUI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Administrator.Controllers
{
    [ActFilter, ResFilter, AdminAuthentication]
    public class ProductController : Controller
    {
        ProductRepository prep;

        ProductCategoryRepository pcrep;

        ProductFeatureRepository pfrep;

        FeatureRepository frep;

        CategoryRepository crep;

        public ProductController()
        {
            prep = new ProductRepository();

            pcrep = new ProductCategoryRepository();

            frep = new FeatureRepository();

            crep = new CategoryRepository();

            pfrep = new ProductFeatureRepository();
        }

        // ürünleri listeler.
        public ActionResult ListProduct()
        {
            return View();
        }

        public PartialViewResult ProductList()
        {
            return PartialView("_ProductList", prep.GetActives());
        }


        // ürünleri ekler
        public ActionResult AddProduct()
        {
            return View(Tuple.Create(new Product(), frep.GetActives(), crep.GetActives()));
        }

        [HttpPost]
        public ActionResult AddProduct([Bind(Prefix = "item1")]Product item, FormCollection koleksiyon)
        {
            if (item != null || koleksiyon != null)
            {
                prep.Add(item);

                var kategoriler = koleksiyon["kategoriler"];
                #region Açıklama
                /*
                         * Koleksiyon tüm girdileri taşıdığı yani product nesnesinin de girdilerini taşıdığı ve product nesnesinin zorunlu alanlar olduğu için koleksiyon null gelmiyor. Koleksiyon null gelmese de kategoriler ve özellikler null gelebilir. Çünkü bu alanlar zorunlu alanlardan değil.
                         */
                #endregion

                if (kategoriler != null)
                {
                    foreach (char kategori in kategoriler)
                    {
                        if (kategori != 44)
                        {
                            ProductCategory pc = new ProductCategory();

                            pc.CategoryID = Convert.ToInt32(kategori.ToString());
                            pc.ProductID = item.ID;
                            pcrep.Add(pc);
                        }
                    }
                }

                var ozellikler = koleksiyon["ozellikler"];

                if (ozellikler != null)
                {
                    foreach (char ozellik in ozellikler)
                    {
                        if (ozellik != 44)
                        {
                            ProductFeature pf = new ProductFeature();

                            pf.FeatureID = Convert.ToInt32(ozellik.ToString());
                            pf.ProductID = item.ID;
                            pfrep.Add(pf);
                        }
                    }
                }

                return RedirectToAction("Detail", new { id = item.ID });   //Admin ürün ekledikten sonra özeliklere değer atayabilmesi için detail sayfasına ürünün id'si ile birlikte yönlendirildi
            }
            ViewBag.Hata = "Ürün ekleme sırasında hata oluştu.";
            return View();
        }
       
        public ActionResult Detail(int id)
        {
            //Product product = prep.GetByID(id);

            List<ProductFeature> liste = pfrep.Where(x => x.ProductID == id);

            return View(liste);

            //Bu sayfada kullanıcı ürünün tüm özelliklerini görecek ve sadece ürün özelliklerine atama yapabilecek. Yani güncelleme sayfası gibi olmayacak. Ürün özellikleri dışındaki tüm özellikler readonly olacak, kaydet butonu ile özelliklere atanan değerler kaydedilecek ve ayrıca sayfada Güncelleme butonu olacak ve güncellemek isterse bu butona basarak güncelleme sayfasına gidebilecek. Yani sayfamız her ne kadar güncelleme sayfasına benzese de güncelleme sayfasından farklı.
        }

        [HttpPost]
        public ActionResult Detail(List<ProductFeature> liste)
        {
            if (liste != null)
            {
                foreach (ProductFeature item in liste)
                {
                    pfrep.Update(item);
                }
            }
            return RedirectToAction("ListProduct");
        }

        // ürünleri günceller
        public ActionResult Update(int id)
        {
            //Product product = prep.GetByID(id);
            //ProductCategory productCategory = pcrep.Where(x => x.CategoryID==id).FirstOrDefault();


            ProductVM productVM = new ProductVM();
            ViewBag.kategoriler = crep.GetActives(); // Dropdownlistfor da kullanıcı tum kategorileri gorsun dıye
            ViewBag.ozellikler = frep.GetActives(); // Dropdownlistfor da kullanıcı tum ozellıklerı gorsun dıye



            productVM.Product = prep.GetByID(id);
            productVM.SelectedCategory = pcrep.Where(x => x.ProductID == id);
            productVM.SelectedFetaure = pfrep.Where(x => x.ProductID == id);


            //foreach (ProductCategory item in pcrep.Where(x => x.ProductID == id).ToList())
            //{
            //    productVM.SelectedCategory.Add(pcrep.Select(x=> new  {x.CategoryID,x.Category.CategoryName }))
            //}

            //List<string> products = productVM.SelectedCategory.Select(x => x.Category.CategoryName).ToList();
            //for (int i = 0; i < products.Count; i++)
            //{
            //    string[] produc = products.Select(x => x.ToString()).ToArray();
            //    ViewData["produc"] = produc;
            //}


            return View(productVM);
        }

        [HttpPost]
        public ActionResult Update(ProductVM item, FormCollection koleksiyon, HttpPostedFileBase resim)
        {
            Product urunumuz = prep.Where(x => x.ID == item.Product.ID).FirstOrDefault();
            urunumuz.ProductName = item.Product.ProductName;
            urunumuz.UnitPrice = item.Product.UnitPrice;
            urunumuz.UnitsInStock = item.Product.UnitsInStock;
            

            if (urunumuz.ImagePath != item.imagePath)
            {
                urunumuz.ImagePath = ImageUploader.UploadImage("~/Pictures/", resim);
            }

            /*
             item.ImagePath = ImageUploader.UploadImage("~/Images/", resim);
            db.TestClasses.Add(item);
            db.SaveChanges();
            return RedirectToAction("Index");
             */

            List<string> kategoriler = koleksiyon["kategoriler"].Split(',').ToList();
            List<string> ozellikler = koleksiyon["ozellikler"].Split(',').ToList();

            // suanda secılı olarak gelen kategori ve ozellıklerı elımızde tutuyoruz...

            foreach (string item1 in kategoriler)
            {
                int catID = Convert.ToInt32(item1);
                if (pcrep.Any(x => x.ProductID == item.Product.ID && x.CategoryID == catID) == false)
                {
                    ProductCategory productCategory = new ProductCategory();

                    productCategory.ProductID = item.Product.ID;
                    productCategory.CategoryID = catID;

                    pcrep.Add(productCategory);
                }



            }

            foreach (ProductCategory item2 in pcrep.Where(x => x.ProductID == item.Product.ID))
            {

                if (kategoriler.IndexOf($"{item2.CategoryID}") == -1)
                {
                    pcrep.SpecialDelete(item2);
                }

            }

            foreach (string item3 in ozellikler)
            {
                int özelID = Convert.ToInt32(item3);
                if (pfrep.Any(x => x.ProductID == item.Product.ID && x.FeatureID == özelID) == false)
                {
                    ProductFeature productFeature = new ProductFeature();

                    productFeature.ProductID = item.Product.ID;
                    productFeature.FeatureID = özelID;

                    pfrep.Add(productFeature);
                }

            }
            foreach (ProductFeature item4 in pfrep.Where(x => x.ProductID == item.Product.ID))
            {

                if (ozellikler.IndexOf($"{item4.FeatureID}") == -1)
                {
                    pfrep.SpecialDelete(item4);
                }

            }

            prep.Update(urunumuz);
            return RedirectToAction("ListProduct", new { @id = item.Product.ID });
        }

        // ürünleri siler.
        public ActionResult Delete(int id)
        {
            Product p = prep.GetByID(id);

            if (p != null)
            {
                prep.Delete(p);
            }
            else
            {
                TempData["Hata"] = "Hata Oluştu";
            }
            return RedirectToAction("ListProduct");
        }
    }
}
