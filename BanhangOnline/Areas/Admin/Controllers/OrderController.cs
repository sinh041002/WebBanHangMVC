using Banhangonline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace BanhangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Order
        public ActionResult Index(int? page)
        {
            var item=db.Orders.OrderByDescending(x=>x.CreatedDate).ToList();
            if (page == null)
            {
                page = 1;
            }
            var pageNumber = page ?? 1;
            var pageSize = 8;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(item.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult Orderdetail(int id)
        {
            var item = db.Orders.Find(id);
            return View(item);
        }
       

        [HttpPost]
        public ActionResult UpdateTrangthai(int id,int trangthai)
        {
            var item = db.Orders.Find(id);
            if (item != null)
            {
                db.Orders.Attach(item);
                item.TypePayment= trangthai;
                db.Entry(item).Property(x=>x.TypePayment).IsModified=true;
                db.SaveChanges();
                return Json(new {message= "success", success = true});
            }
            return Json(new { message = "Unsuccess", success = false });
        }
    }
}