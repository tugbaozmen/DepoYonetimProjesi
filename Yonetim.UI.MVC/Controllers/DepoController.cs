using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonetim.UI.MVC.Models;

namespace Yonetim.UI.MVC.Controllers
{
    public class DepoController : Controller
    {
        Depo_DBEntities db;
        public DepoController()
        {
            db = new Depo_DBEntities();
        }
        public ActionResult DepoListele()
        {
            List<Depo> depo;
            depo = db.Depoes.ToList();
            return View(depo);
        }


        public ActionResult DepoEkle()
        {
            UrunleriBul();
            KisiBilgileriniBul();
            return View();
        }

        [HttpPost]
        public ActionResult DepoEkle(Depo model)
        {
            if (ModelState.IsValid)
            {
                Depo depo = new Depo();
                depo.Ad = model.Ad;
                depo.Tanim = model.Tanim;
                depo.KisiId = model.KisiId;
                depo.UrunId = model.UrunId;
                try
                {
                    db.Depoes.Add(depo);
                    db.SaveChanges();
                    return RedirectToAction("DepoListele");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError("", "Girdiğiniz bilgileri kontrol ediniz");
            }

            UrunleriBul();
            KisiBilgileriniBul();
            return View();
        }

   
        public ActionResult DepoGuncelle(int? id)
        {
            Depo depo = null;

            if (id == null)
            {
                ViewBag.Sonuc = false;
            }
            else
            {
                depo = db.Depoes.Find(id);
                if (depo != null)
                {
                    UrunleriBul();
                    KisiBilgileriniBul();
                    return View(depo);
                }
                else
                {
                    ViewBag.Sonuc = true;
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult DepoGuncelle(Depo model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            ViewBag.Sonuc = false;
            return View();
        }

        private void KisiBilgileriniBul()
        {
            List<SelectListItem> kisiler = new List<SelectListItem>();
            foreach (Kisi item in db.Kisis)
            {
                kisiler.Add(new SelectListItem() { Text = item.Ad + " " + item.Soyad, Value = item.KisiId.ToString() });
            }

            ViewBag.Kisiler = kisiler;
        }

        private void UrunleriBul()
        {
            List<SelectListItem> urunler = new List<SelectListItem>();
            foreach (Urun item in db.Uruns)
            {
                urunler.Add(new SelectListItem() { Text = item.Ad, Value = item.UrunId.ToString() });
            }

            ViewBag.Urunler = urunler;
        }


    }
}