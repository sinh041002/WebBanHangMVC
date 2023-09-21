using Banhangonline.Models;

using BanhangOnline.Models.EF;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Banhangonline.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Tintuc
        public ActionResult Index(string Searchtext, int? page )
        {
            var pageSize = 6;
            if(page == null )
            {
                page = 1;
            }
            IEnumerable<News> items = db.News.OrderByDescending(x=>x.Id);
            if(!string.IsNullOrEmpty(Searchtext) )
            {
                items = items.Where(x=>x.Alias.Contains(Searchtext) || x.Title.Contains(Searchtext));

            }
            var pageIndex = page.HasValue?Convert.ToInt32(page):1;
            items=items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page=page;
      
            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(News model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.CategoryId = 23;
                model.ModifiedrDate = DateTime.Now;
                model.Alias = Banhangonline.Models.Common.Filter.FilterChar(model.Title);
                db.News.Add(model);
                //db.News.Attach(model);
                //db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var items = db.News.Find(id);
            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News model)
        {
            if (ModelState.IsValid)
            {
                db.News.Attach(model);

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
            var item = db.News.Find(id);
            if (item != null)
            {
                db.News.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult isActive(int id)
        {
            var item = db.News.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.Entry(item).Property(x => x.IsActive).IsModified = true;
                db.SaveChanges();
                return Json(new { success = true , isAcive = item.IsActive });
            }
            return Json(new { success = false });
        }
    }
}