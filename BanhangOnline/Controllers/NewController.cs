using Banhangonline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanhangOnline.Controllers
{
    public class NewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: New
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Part_News_Home()
        {
            var item=db.News.Take(3).ToList();
            return View(item);
        }
    }
}