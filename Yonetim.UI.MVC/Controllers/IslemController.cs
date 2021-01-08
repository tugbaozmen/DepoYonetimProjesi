using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonetim.UI.MVC.Models;

namespace Yonetim.UI.MVC.Controllers
{
    public class IslemController : Controller
    {
        Depo_DBEntities db;
        public IslemController()
        {
            db = new Depo_DBEntities();
        }
        public ActionResult AnaSayfa()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("GirisYap");
            }
            else
            {
                return View();
            }
        }
        public ActionResult GirisYap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GirisYap(Kisi kisi)
        {
            List<Kisi> kisiler = db.Kisis.ToList();
            foreach (var item in kisiler)
            {
                if (kisi.Mail == item.Mail && kisi.Sifre == item.Sifre)
                {
                    Session["kisi"] = db.Kisis.Find(item.KisiId);
                    Session["ID"] = item.KisiId;
                    //return RedirectToAction("Index", "Account", new { id = item.KisiId });
                    return RedirectToAction("AnaSayfa", "Islem");
                }
                else
                {
                    continue;
                }
            }
            ViewBag.Hata = "Kullanıcı Bilgilerinizi Kontrol Ediniz.";
            return View();
        }
        public ActionResult UyeOL()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UyeOl(Kisi model)
        {
            if (ModelState.IsValid)
            {
                Kisi kisi = new Kisi();
                kisi.Ad = model.Ad;
                kisi.Soyad = model.Soyad;
                kisi.Mail = model.Mail;
                kisi.Sifre = model.Sifre;
                try
                {
                    db.Kisis.Add(kisi);
                    db.SaveChanges();
                    return RedirectToAction("GirisYap");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            else
            {
                return View();
            }
            return View();
        }
    }
}