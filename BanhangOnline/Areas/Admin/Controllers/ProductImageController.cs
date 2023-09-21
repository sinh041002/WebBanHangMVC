using Banhangonline.Models;

using BanhangOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;
using System.Web.Mvc;

namespace BanhangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductImageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ProductImage
        public ActionResult Index(int id)
        {
            ViewBag.ProductId=id;
            var item = db.ProductImages.Where(x=>x.ProductId== id).ToList();
            return View(item);
        }
        [HttpPost]
        public ActionResult AddImage(int productId, string imageUrl)
        {
            db.ProductImages.Add(new ProductImage
            {
                ProductId= productId,
                Image= imageUrl,
                IsDefault=false,
            });
            db.SaveChanges();
            return Json(new { success = true });
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.ProductImages.Find(id);
            if (item != null)
            {
                db.ProductImages.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

    }
}