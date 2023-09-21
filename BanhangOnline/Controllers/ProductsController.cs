using Banhangonline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanhangOnline.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Products
        public ActionResult Index(int? id)
        {
            var item = db.Products.ToList();
            if (id!=null)
            {
                item=item.Where(x => x.ProductCategoryId==id).ToList();
            }
            var cate = db.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.Titlename = cate.Title;
            }
            ViewBag.CateId = id;
            return View(item);
        }
        public ActionResult Details(int? id) 
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                db.Products.Attach(item);
                item.ViewCount = item.ViewCount + 1;
                db.Entry(item).Property(x=>x.ViewCount).IsModified = true;
                db.SaveChanges();
            }
      
            return View(item);
        }
        public ActionResult Partial_ItemsById()
        {
            var item=db.Products.Where(x=>x.IsHome).Take(10).ToList();
            return PartialView(item);
        }
        public ActionResult Partial_ProductSales()
        {
            var item = db.Products.Where(x => x.IsSale).Take(10).ToList();
            return PartialView(item);
        }
    }
}