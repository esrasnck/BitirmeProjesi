using PagedList;
using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.MODEL.Entities;
using Project.MVCUI.AuthenticationClasses;
using Project.MVCUI.Filters;
using Project.MVCUI.Models;
using Project.MVCUI.Models.VMClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    [ActFilter, ResFilter]
    public class MemberController : Controller
    {
        ProductRepository pRep;
        OrderRepository oRep;
        OrderDetailRepository odRep;
        CategoryRepository cRep;
        ProductCategoryRepository pcRep;
        public MemberController()
        {
            cRep = new CategoryRepository();
            pcRep = new ProductCategoryRepository();
            pRep = new ProductRepository();
            oRep = new OrderRepository();
            odRep = new OrderDetailRepository();
            ViewData.Add("kategoriler", cRep.GetActives());
        }
        // GET: Member
        public ActionResult ProductList(string item, int sayfa = 1)
        {
            try
            {
                ViewBag.kategori = item;
                ViewBag.kategoriListesi = cRep.GetActives();
                if (item == null)
                {
                    IPagedList degerler = pRep.GetActives().ToPagedList(sayfa, 10);
                    return View(degerler);

                }
                else
                {
                    // sorun category sını belırledıgımız ıstedıgımız urunlerı getıremıyoruz
                    //productın ıcınden cekmelıyız
                    return View(pRep.KategoriyeGoreUrunGetir(item).ToPagedList(sayfa, 10));

                    //return View(pRep.where(x => x.Categories.FirstOrDefault().Category.CategoryName == item).ToList().ToPagedList(sayfa, 10));
                }
            }
            catch (Exception)
            {

                return RedirectToAction("Login", "Home");
            }

        }
        public PartialViewResult Slider(decimal sayi1, decimal sayi2, int sayfa = 1)
        {
            //ViewBag.sayi1 = sayi1;
            //ViewBag.sayi2 = sayi2;
            try
            {
                if (pRep.Slider(sayi1, sayi2) != null)
                {
                    return PartialView(pRep.Slider(sayi1, sayi2).ToPagedList(sayfa, 10));
                }
                else
                {
                    return PartialView();
                }
            }
            catch (Exception)
            {

                return PartialView();
            }
            
            
        }


        [HttpGet]
        public PartialViewResult Filtre(string item)

        {
            return PartialView("Filtre", pRep.Where(x => x.ProductName.StartsWith(item)));
        }
        public ActionResult ProductDetail(int item)
        {
            if (pRep.Any(x => x.ID == item))
            {
                Product bizimurun = pRep.GetByID(item);
                using (HttpClient client = new HttpClient())
                {


                    client.BaseAddress = new Uri("http://localhost:61379/api/");
                    //var postTask = client.PostAsJsonAsync("Product/GetProducts", item);
                    var getTask = client.GetAsync("Product/GetProducts");
                    var message = getTask.Result.Content.ReadAsAsync<List<ProductVM>>();

                    List<ProductVM> digerfirma = message.Result as List<ProductVM>;

                    if (digerfirma.Any(x => x.ProductName == bizimurun.ProductName))
                    {
                        ProductVM adaminurunu = digerfirma.Where(x => x.ProductName == bizimurun.ProductName).FirstOrDefault();
                        TempData["karsılastırma"] = "Eşleşen ürünler vardır";
                        ViewData.Add("adaminurunu", adaminurunu);

                        if (bizimurun.UnitPrice < adaminurunu.UnitPrice)
                        {
                            ViewBag.info = $"{adaminurunu.UnitPrice - bizimurun.UnitPrice} ₺ Kardasınız";
                            ViewBag.info2 = $"{adaminurunu.UnitPrice - bizimurun.UnitPrice} ₺ Zarardasınız";
                        }
                        else if (bizimurun.UnitPrice> adaminurunu.UnitPrice)  // sonradan eklendi.
                        {
                            ViewBag.info = $"{adaminurunu.UnitPrice - bizimurun.UnitPrice} ₺ Kardasınız";
                            ViewBag.info2 = $"{adaminurunu.UnitPrice - bizimurun.UnitPrice} ₺ Zarardasınız";
                        }
                        

                    }

                }
                return View(bizimurun);
            }

            TempData["karsılastırma"] = "Eşleşen ürünler yoktur";
            return RedirectToAction("ProductList");
        }

        [MemberAuthentication] //  sonradan eklendi
        public ActionResult SepeteAt(int id)
        {
            try
            {
                if (Session["member"] == null)
                {
                    return RedirectToAction("ProductList"); // home/login i dene
                }
                else 
                {
                    Cart c = Session["scart"] == null ? new Cart() : Session["scart"] as Cart;

                    Product eklenenUrun = pRep.GetByID(id);

                    CartItem ci = new CartItem();

                    ci.ID = eklenenUrun.ID;
                    ci.Name = eklenenUrun.ProductName;
                    ci.Price = eklenenUrun.UnitPrice;
                    ci.ImagePath = eklenenUrun.ImagePath;
                    c.SepeteEkle(ci);

                    Session["scart"] = c;
                    ViewBag.SepeteAtma = "Urun Sepete Atıldı";
                    return RedirectToAction("ProductList");
                }


               
            }

            catch (Exception)
            {
                ViewBag.SepeteAtma = "Urun Sepete Atılamadı";
                return RedirectToAction("ProductList");
            }

        }

        [MemberAuthentication] // sonradan eklendi
        public ActionResult CartPage() // suraya bak!!!
        {
            try
            {
                if (Session["scart"] != null)
                {
                    Cart c = Session["scart"] as Cart;
                    return View(c);
                }
                else if (Session["member"] == null)
                {
                    TempData["UyeDegil"] = "Lutfen önce üye olun";
                    return RedirectToAction("Login", "Home");  // sonradan değişti. önceden registerdı
                }
                TempData["message"] = "Sepetinizde ürün bulunmamaktadır";
                return RedirectToAction("ProductList");
            }
            catch (Exception)
            {

                return RedirectToAction("ProductList");
            }

        }

        [MemberAuthentication] // sonradan eklendi
        public ActionResult SiparisiOnayla()
        {
            
            try
            {
       
                return View(Tuple.Create(new Order(), new PaymentVM()));
            }
            catch (Exception)
            {
                
                return RedirectToAction("CartPage", "Member");
            }

        }

        [HttpPost]

        [MemberAuthentication] // sonradan eklendi
        public ActionResult SiparisiOnayla([Bind(Prefix = "Item1")] Order item, [Bind(Prefix = "Item2")] PaymentVM item2)
        {
            try
            {
                bool result = false;
                bool result2 = false;

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:61183/api/");

                    Cart sepet = Session["scart"] as Cart;
                    item2.PaymentPrice = sepet.TotalPrice.Value;

                    var postTask = client.PostAsJsonAsync("Payment/ReceivePayment", item2);



                    HttpResponseMessage sonuc = postTask.Result;


                    if (sonuc.IsSuccessStatusCode)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }


                }

                if (result)
                {
                    item.AppUserID = (Session["member"] as AppUser).ID;

                    oRep.Add(item);    
                    Cart sepet = Session["scart"] as Cart;


                    foreach (CartItem urun in sepet.Sepetim)
                    {

                        

                        OrderDetail od = new OrderDetail();
                        od.OrderID = item.ID;
                        od.ProductID = urun.ID;
                        od.TotalPrice = urun.SubTotal;
                        od.Amount = urun.Amount;
                        od.PaymentDate = DateTime.Now;

                        odRep.Add(od);
                        
                    }

                    TempData["odeme1"] = "Siparişiniz bize ulasmıstır..Tesekkür ederiz";


                    using (HttpClient client = new HttpClient())
                    {
                        //furkan denedi
                        KargoVM kargo = new KargoVM();

                        client.BaseAddress = new Uri("https://localhost:44333/api/");

                        kargo.Adi = (Session["member"] as AppUser).Profile.FirstName;
                        kargo.Soyadi = (Session["member"] as AppUser).Profile.LastName;
                        kargo.TCKimlikNumarası = item.TC;
                        kargo.Adres = item.Address;
                        kargo.Mail = (Session["member"] as AppUser).Email;
                        kargo.Il = item.City;
                        kargo.Ilce = item.District;
                        kargo.Mahalle = item.Town;
                        kargo.Telefon = item.Phone;




                        var postTask = client.PostAsJsonAsync("Home/KargoOlustur", kargo);

                        HttpResponseMessage sonuc = postTask.Result;

                        var geriyedonen = sonuc.Content.ReadAsAsync<string>();

                        string kargoyazisi = geriyedonen.Result;
                        TempData["Kargo"] = kargoyazisi;

                        if (sonuc.IsSuccessStatusCode)
                        {
                            result2 = true;

                            ViewData.Add("orderbilgileri", item);

                        }
                        else
                        {
                            result2 = false;

                        }

             
                    }

                }
                else
                {
                    TempData["odeme"] = "Odeme ile ilgili bir sıkıntı olustu. Lütfen banka ile iletişime geciniz";
                  
                    return RedirectToAction("ProductList");
                }
               
                return RedirectToAction("ProductList");


            }
            catch (Exception)
            {
                
                TempData["SiparisOnaylanmadı"] = "Siparişiniz Onaylanamadı";
                return RedirectToAction("ProductList", "Member");

            }




        }

     
        [HttpGet]

        [MemberAuthentication] // sonradan eklendi
        public ActionResult Fatura()
        {

            try
            {
                if (Session["scart"] != null)
                {
                    Cart c = Session["scart"] as Cart;
                    ViewBag.kullaniciad = (Session["member"] as AppUser).Profile.FirstName;
                    ViewBag.soyad = (Session["member"] as AppUser).Profile.LastName;
                    ViewBag.kullaniciAdres = (Session["member"] as AppUser).Profile.Address;
                    //ViewBag.kullaniciSehir= (Session["member"] as Order).City;
                   
                    return View(c);
                   
                }
                else if (Session["member"] == null)
                {
                    
                    TempData["UyeDegil"] = "Lutfen önce üye olun";
                    return RedirectToAction("Register", "Home");
                }
                TempData["message"] = "Sepetinizde ürün bulunmamaktadır";
                return RedirectToAction("ProductList");
            }
            catch (Exception)
            {

                return RedirectToAction("ProductList");
            }


        }

        [MemberAuthentication] // sonradan eklendi
        public ActionResult Delete(int id)
        {
            try
            {
                if (Session["scart"] != null)
                {
                    Cart c = Session["scart"] as Cart;
                    c.Sepettensil(id);

                    if ((Session["scart"] as Cart).Sepetim.Count == 0)
                    {
                        Session.Remove("scart");
                        TempData["gitti"] = "Sepetiniz bosaltılmıstır";

                        return RedirectToAction("ProductList");
                    }
                    return RedirectToAction("CartPage");
                }

                TempData["silinecekYok"] = "Sepetiniz bos oldugu icin bu sayfaya gitmenizde bir mantık yok";

                return RedirectToAction("ProductList");
            }
            catch (Exception)
            {
                ViewBag.UrunSil = "Sepetten Urun Çıkartılamadı";
                return RedirectToAction("CartPage", "Member");
            }

        }


    }
}