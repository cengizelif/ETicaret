using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _08042019_MVC.Models;
using Microsoft.AspNet.Identity;

namespace _08042019_MVC.Controllers
{
    public class SepetsController : Controller
    {
        private ETICARETEntities ctx = new ETICARETEntities();

        // GET: Sepets
        public ActionResult Index()
        {
            var sepet = ctx.Sepet.Include(s => s.AspNetUsers).Include(s => s.Urunler);
            return View(sepet.ToList());
        }

        public ActionResult SepeteEkle(int? adet,int id)
        {
            string UserID = User.Identity.GetUserId();

            Urunler urun = ctx.Urunler.Find(id);
            Sepet sepettekiurunler = ctx.Sepet.FirstOrDefault(x => x.RefUrunID == id && x.RefKulID == UserID);

            if (sepettekiurunler==null)
            {
                Sepet yeniurun = new Sepet();
                yeniurun.RefKulID = UserID;
                yeniurun.RefUrunID = id;
                yeniurun.Toplam =(adet??1) * urun.UrunFiyati;
                yeniurun.Adet = adet??1;
                ctx.Sepet.Add(yeniurun);
            }
            else
            {
                sepettekiurunler.Adet = sepettekiurunler.Adet + (adet??1);
                sepettekiurunler.Toplam = sepettekiurunler.Adet * urun.UrunFiyati;
            }

            ctx.SaveChanges(); 
            
            return RedirectToAction("Index");
        }

        public ActionResult SepetGuncelle(int id, int? adet)
        {
            Sepet sepet = ctx.Sepet.Find(id);
            Urunler urun = ctx.Urunler.Find(sepet.RefUrunID);

            sepet.Adet = adet ?? 1;
            sepet.Toplam = sepet.Adet * urun.UrunFiyati;
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
            
        

        // GET: Sepets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sepet sepet = ctx.Sepet.Find(id);
            if (sepet == null)
            {
                return HttpNotFound();
            }

            ctx.Sepet.Remove(sepet);
            ctx.SaveChanges();

            return RedirectToAction("Index");
            //return View(sepet);
        }

        // POST: Sepets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sepet sepet = ctx.Sepet.Find(id);
            ctx.Sepet.Remove(sepet);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ctx.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
