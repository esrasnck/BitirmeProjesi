using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.MODEL.Entities;
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
    public class FeatureController : Controller
    {
        FeatureRepository frep;

        public FeatureController()
        {
            frep = new FeatureRepository();
        }

        // özellikleri listeler
        public ActionResult List()
        {
            return View(frep.GetActives());
        }

        // özellikleri ekler
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Feature item)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (item != null)
            {
                if (frep.Any(x => x.FeatureName == item.FeatureName && x.Description == item.Description && x.Status != MODEL.Enums.DataStatus.Deleted))
                {
                    ViewBag.Mevcut = "Eklemek istediğiniz özellik kayıtlarda mevcuttur.";
                    return View();
                }
                frep.Add(item);
                return RedirectToAction("List");
            }
            ViewBag.Hata = "Hata oluştu";
            return View();
        }

        // özellikleri günceller.
        public ActionResult Update(int id)
        {
            Feature f = frep.GetByID(id);

            if (f != null)
            {
                return View(f);
            }
            ViewBag.Hata = "Hata oluştu.";
            return View();
        }

        [HttpPost]
        public ActionResult Update(Feature item)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (item != null)
            {
                frep.Update(item);
                return RedirectToAction("List");
            }
            ViewBag.Hata = "Hata Oluştu";
            return View();
        }

        // özellikleri siler.
        public ActionResult Delete(int id)
        {
            Feature f = frep.GetByID(id);

            if (f!=null)
            {
                frep.Delete(f);
            }
            else
            {
                TempData["Hata"] = "Hata Oluştu";
            }
            return RedirectToAction("List");
        }
    }
}
