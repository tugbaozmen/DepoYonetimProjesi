using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonetim.UI.MVC.Models;

namespace Yonetim.UI.MVC.Controllers
{
    public class UrunController : Controller
    {
        Depo_DBEntities db;
        public UrunController()
        {
            db = new Depo_DBEntities();
        }
        public ActionResult UrunleriListele()
        {
            List<Urun> urun;
            urun = db.Uruns.ToList();
            return View(urun);
        }


        public ActionResult UrunEkle()
        {
            DepoBilgileriniBul();
            return View();
        }

        [HttpPost]
        public ActionResult UrunEkle(Urun model)
        {

            try
            {
                db.Uruns.Add(model);
                db.SaveChanges();
                ViewBag.DogruMu = true;
            }
            catch (Exception ex)
            {
                ViewBag.DogruMu = false;
            }

            DepoBilgileriniBul();
            return View();
        }


        public ActionResult UrunGuncelle(int? id)
        {
            Urun urun = null;

            if (id == null)
            {
                ViewBag.Sonuc = false;
            }
            else
            {
                urun = db.Uruns.Find(id);
                if (urun != null)
                {
                    DepoBilgileriniBul();
                    return View(urun);
                }
                else
                {
                    ViewBag.Sonuc = true;
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult UrunGuncelle(Urun model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            ViewBag.Sonuc = false;
            return View();
        }

        private void DepoBilgileriniBul()
        {
            List<SelectListItem> depolar = new List<SelectListItem>();
            foreach (Depo item in db.Depoes)
            {
                depolar.Add(new SelectListItem() { Text = item.Ad, Value = item.DepoId.ToString() });
            }

            ViewBag.Depolar = depolar;
        }

    }
}