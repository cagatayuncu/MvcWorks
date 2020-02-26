using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthWindDeneme.Models.EntityFramework;
namespace NorthWindDeneme.Controllers
{
    public class HomeController : Controller
    {

        readonly NorthWindModel db = new NorthWindModel();


        public ActionResult Index()
        {
            var model = db.Categories.ToList();
            return View(model);

           
        }

        [HttpGet]

        public ActionResult Kayıt()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Kayıt(Categories categories)
        {
            if (categories.CategoryID == 0)
            {
                db.Categories.Add(categories);
            }
            else
            {
                var updateCategories = db.Categories.Find(categories.CategoryID);
                if (updateCategories == null)
                {
                    return HttpNotFound();
                }
                updateCategories.CategoryName = categories.CategoryName;
                updateCategories.Description = categories.Description;
            }
            db.SaveChanges();
            return RedirectToAction("index", "Home");

        }
        public ActionResult Guncelle(int id)
        {
            var model = db.Categories.Find(id);
            if (model == null)
                return HttpNotFound();

            return View("Kayıt", model);
        }
        public ActionResult Sil(int id)
        {
            var deleteCategory = db.Categories.Find(id);
            if (deleteCategory == null)
                return HttpNotFound();
            db.Categories.Remove(deleteCategory);
            db.SaveChanges();
            return RedirectToAction("index");

        }
        [HttpPost]
        public ActionResult Ara(string ara)
        {
            List<Categories> bul = db.Categories.Where(x => x.CategoryName.Contains(ara) || x.Description.Contains(ara)).ToList();
            if (bul == null)
                return HttpNotFound();

            return View("index",bul);
        }
    }
}