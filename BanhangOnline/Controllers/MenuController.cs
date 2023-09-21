using Banhangonline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanhangOnline.Controllers
{
    public class MenuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuTop()
        {
            var item=db.Categories.OrderBy(x=>x.Position).ToList();
            return PartialView("_MenuTop",item);
        }
        public ActionResult MenuProductCategory()
        {
            var item = db.ProductCategories.ToList();
            return PartialView("_MenuProductCategory", item);
        }
        public ActionResult MenuProductLeft(int? id)
        {
            if(id!=null)
            {

                ViewBag.CateId=id;
            }
           
            var item = db.ProductCategories.ToList();
            return PartialView("_MenuProductLeft", item);
        }
        public ActionResult MenuArrivals()
        {
            var item = db.ProductCategories.ToList();
            return PartialView("_MenuArrivals", item);
        }
    }
}