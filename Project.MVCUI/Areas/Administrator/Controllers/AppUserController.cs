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
    [ActFilter, ResFilter, AdminAuthentication]

    public class AppUserController : Controller //Tamamlandı
    {
        #region Açıklama
        /*
         * Burada kullanıcı ekleme, güncelleme, silme ve listeleme işlemleri tanımlandı. Başka bir admin daha tanımlancak ise buradan tanımlancak. Doğası gereği admin içerden biri yani admin tarafından yetkilendirilir.
         * 
         */
        #endregion

        AppUserRepository arep;

        AppUserDetailRepository adrep;

        public AppUserController()
        {
            arep = new AppUserRepository();

            adrep = new AppUserDetailRepository();
        }


        public ActionResult List()
        {

            return View(arep.GetActives());
        }

        // appUser'ın role lerini günceller. admin isterse kullanıcıyı aktifliğini değiştirebilir.
        public ActionResult AppUserRoleUpdate(int id, int secilenindex, int IsActive)
        {
            if (secilenindex != -1 && IsActive != -1)
            {
                AppUser kullanici = arep.GetByID(id);
                kullanici.Role = (UserRole)secilenindex;
                kullanici.IsActive = Convert.ToBoolean(IsActive);
                arep.Update(kullanici);
                return RedirectToAction("List");
            }
            else if (secilenindex != -1 && IsActive == -1)
            {
                AppUser kullanici = arep.GetByID(id);
                kullanici.Role = (UserRole)secilenindex;
                arep.Update(kullanici);
                return RedirectToAction("List");


            }
            else if (secilenindex == -1 && IsActive != -1)
            {
                AppUser kullanici = arep.GetByID(id);
                kullanici.IsActive = Convert.ToBoolean(IsActive);
                arep.Update(kullanici);
                return RedirectToAction("List");


            }
            ViewBag.hataliIslem = "Kullanıcı Güncellenemedi";
            return RedirectToAction("List");

        }

        //kullanıcıyı siler(pasif hale getirir

            
        public ActionResult Delete(int id)
        {
            AppUser a = arep.GetByID(id);

            if (a != null)
            {
                arep.Delete(a);
            }
            else
            {
                TempData["Hata"] = "Hata Oluştu";
            }
            return RedirectToAction("List");
        }


    }
}