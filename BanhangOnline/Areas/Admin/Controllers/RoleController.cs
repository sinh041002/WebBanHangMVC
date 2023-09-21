using Banhangonline.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanhangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Role
        public ActionResult Index()
        {
            var items=db.Roles.ToList();    
            return View(items);
        }
        public ActionResult Add()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(IdentityRole model)
        {
            if (ModelState.IsValid)
            {

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                roleManager.Create(model);
                

                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Sua(int id)
        {
            var items = db.Roles.Find(id);
            return View(items);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sua(IdentityRole model)
        {
            if (ModelState.IsValid)
            {

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                roleManager.Update(model);


                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}