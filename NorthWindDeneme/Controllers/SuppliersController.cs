using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthWindDeneme.Models.EntityFramework;

namespace NorthWindDeneme.Controllers
{
    public class SuppliersController : Controller
    {
        NorthWindModel db = new NorthWindModel();
        // GET: Invoices
        public ActionResult Index()
        {
            var model = db.Suppliers.ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Ara(string ara)
        {
            List<Suppliers> bul = db.Suppliers.Where(x => x.Address.Contains(ara) || x.CompanyName.Contains(ara)
            || x.City.Contains(ara) || x.ContactName.Contains(ara) || x.Country.Contains(ara) || x.Phone.Contains(ara)).ToList();
            if (bul == null)
                return HttpNotFound();

            return View("index", bul);
        }
        public ActionResult Guncelle(int id)
        {
            var model = db.Suppliers.Find(id);
            if (model == null)
                return HttpNotFound();

            return View("Kayıt", model);
        }
        [HttpGet]

        public ActionResult Kayıt()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Kayıt(Suppliers categories)
        {
            if (categories.SupplierID == 0)
            {
                db.Suppliers.Add(categories);
            }
            else
            {
                var update = db.Suppliers.Find(categories.SupplierID);
                if (update == null)
                {
                    return HttpNotFound();
                }
                update.CompanyName = categories.CompanyName;
                update.ContactName = categories.ContactName;
                update.Country = categories.Country;
                update.Phone = categories.Phone;
                update.Address = categories.Address;
                update.City = categories.City;
            }
            db.SaveChanges();
            return RedirectToAction("index", "Suppliers");

        }
        public ActionResult Sil(int id)
        {
            var delete = db.Suppliers.Find(id);
            if (delete == null)
                return HttpNotFound();
            db.Suppliers.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}