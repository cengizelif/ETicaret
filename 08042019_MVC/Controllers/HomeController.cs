using _08042019_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _08042019_MVC.Controllers
{
    public class HomeController : Controller
    {
        ETICARETEntities ctx = new ETICARETEntities();
        public ActionResult Index()
        {
            ViewBag.KategoriListesi = ctx.Kategori.ToList();

            ViewBag.SonUrunler = ctx.Urunler.OrderByDescending(a => a.UrunID).Skip(0).Take(12).ToList();
            return View();
        }

        public ActionResult Kategori(int id)
        {
            ViewBag.KategoriListesi = ctx.Kategori.ToList();
            ViewBag.kategori = ctx.Kategori.Find(id);
            return View(ctx.Urunler.Where(x => x.RefKatID == id).ToList());
        }

        public ActionResult UrunDetay(int id)
        {
            ViewBag.KategoriListesi = ctx.Kategori.ToList();

            return View(ctx.Urunler.Find(id));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}