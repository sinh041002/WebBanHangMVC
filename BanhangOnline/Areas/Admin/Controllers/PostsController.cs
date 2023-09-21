using Banhangonline.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanhangOnline.Models.EF;

namespace Banhangonline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PostsController : Controller
    {

        // GET: Admin/Post
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Tintuc
        public ActionResult Index()
        {
            var items = db.Posts.ToList();
            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Posts model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.CategoryId = 23;
                model.ModifiedrDate = DateTime.Now;
                model.Alias = Banhangonline.Models.Common.Filter.FilterChar(model.Title);
                db.Posts.Add(model);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);

        }
        public ActionResult Edit(int id)
        {
            var items = db.Posts.Find(id);
            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Posts model)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Attach(model);

                model.ModifiedrDate = DateTime.Now;
                model.Alias = Banhangonline.Models.Common.Filter.FilterChar(model.Title);
                db.Entry(model).Property(x => x.Title).IsModified = true;
                db.Entry(model).Property(x => x.SeoTitle).IsModified = true;
                db.Entry(model).Property(x => x.SeoKeywords).IsModified = true;
                db.Entry(model).Property(x => x.SeoDescription).IsModified = true;
                db.Entry(model).Property(x => x.Detail).IsModified = true;
                db.Entry(model).Property(x => x.IsActive).IsModified = true;
                db.Entry(model).Property(x => x.ModifiedrBy).IsModified = true;
                db.Entry(model).Property(x => x.ModifiedrDate).IsModified = true;
                db.Entry(model).Property(x => x.Alias).IsModified = true;
                db.Entry(model).Property(x => x.Description).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);

        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Posts.Find(id);
            if (item != null)
            {
                db.Posts.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult isActive(int id)
        {
            var item = db.Posts.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.Entry(item).Property(x => x.IsActive).IsModified = true;
                db.SaveChanges();
                return Json(new { success = true, isAcive = item.IsActive });
            }
            return Json(new { success = false });
        }
    }
}