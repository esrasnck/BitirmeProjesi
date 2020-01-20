using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.COMMON.CommonTools;
using Project.MODEL.Entities;
using Project.MODEL.Enums;
using Project.MVCUI.AuthenticationClasses;
using Project.MVCUI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Administrator.Controllers
{
    [ActFilter, ResFilter]
    public class HomeController : Controller
    {
        #region Açıklama
        /*
        * Admin'ler için giriş sayfası ayrı olacak. Admin, member ile aynı sayfadan giriş yapmamalı. Admin'nin girişleri bu Controller'da kontrol edilecek. Ayrıca kişi admin olarak tanımlanması için mevcut edamin tarafından yetki verilmesi gerektiği yani memmber gibi bir üyelik işlemi olmayacağı için burada sadece admin girişleri kontrol altına alındı. Admin ekleme, silme, ve güncelleme işlemleri için AppUser Controller'ı oluşturuldu.
        * Aynı şekilde member da buradan giriş yapmamalı, yapamamlı. Bu nedenle burada sadece kullanıcını sadece admin olup olmadığı kontrol edildi ve admin değilse girişe izin verilmedi.
        * 
        */
        #endregion

        AppUserRepository arep;

        public HomeController()
        {
            arep = new AppUserRepository();
        }

        // admin için login 
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AppUser item) 
        {
            try
            {
                foreach (AppUser item2 in arep.GetAll())
                {
                    string cozulmusSifre = DantexCrypt.DeCrypt(item2.Password);

                    if (arep.Any(x => x.UserName == item.UserName && cozulmusSifre == item.Password && x.IsActive == true && x.Role == UserRole.Admin) == true)
                    {

                        Session["admin"] = arep.Where(x => x.UserName == item.UserName && cozulmusSifre == item.Password && x.IsActive == true && x.Role == UserRole.Admin).FirstOrDefault();

                        AppUser kullanici = Session["admin"] as AppUser;
                        return RedirectToAction("ListProduct", "Product");
                    }
                }

                ViewBag.Hatali = "Hatalı giriş yaptınız.";
                return View();
            }
            catch (Exception)
            {
                ViewBag.Hatali = "Hatalı giriş yaptınız";
                return View();
            }
        }

        public ActionResult LogOut()
        {
            if (Session["admin"] != null)
            {
                Session.Remove("admin");
            }
            return RedirectToAction("Login");
        }
    }
}