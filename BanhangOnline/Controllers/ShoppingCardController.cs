using Banhangonline.Models;

using BanhangOnline.Models;
using BanhangOnline.Models.EF;
using System;

using System.Configuration;

using System.Linq;

using System.Web.Mvc;

namespace BanhangOnline.Controllers
{
    public class ShoppingCardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ShoppingCard
        public ActionResult Index()
        {
           
            return View();
        }
        public ActionResult Checkout()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            { 
                ViewBag.Checkcart=cart;
            }
                return View();
        }
        public ActionResult CheckOutSuccess()
        {
            return View();
        }
        public ActionResult Partial_CheckOut()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout( Order order)
        {
            var code = new { Success = false, code = -1 };

            if (ModelState.IsValid==false)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart != null && cart.Items.Any())
                {
                    Order order1=new Order();
                    order1.CustomerName=order.CustomerName;
                    order1.Phone=order.Phone;   
                    order1.Address=order.Address;
                    cart.Items.ForEach(x => order1.OrderDetails.Add(new OrderDetail
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        Price = x.Price
                    }));
                    order1.TotalAmount=cart.Items.Sum(x=>(x.Price*x.Quantity));
                    order1.TypePayment=order.TypePayment;
                    order1.CreatedBy=order.Phone;
                    order1.CreatedDate=DateTime.Now;
                    order1.ModifiedrDate=DateTime.Now;
                    Random rd= new Random();
                    order1.Code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                
                    db.Orders.Add(order1);
                    
                    db.SaveChanges();
                    //send mail cho khach 
                    var strSanPham = "";
                    var ThanhTien = decimal.Zero;
                    var TongTien=decimal.Zero;
                    foreach(var item in cart.Items)
                    {
                        strSanPham += "<tr>";
                        strSanPham += "<td>"+item.ProductName+"</td>";
                        strSanPham += "<td>" + item.Quantity + "</td>";
                        strSanPham += "<td>" + item.Price + "</td>";
                        strSanPham += "</tr>";
                        ThanhTien += item.Price * item.Quantity;

                    }
                    TongTien = ThanhTien;
                    string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/send2.html"));
                    contentCustomer = contentCustomer.Replace("{{MaDon}}", order1.Code);
                    contentCustomer = contentCustomer.Replace("{{SanPham}}", strSanPham);
                    contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", order1.CustomerName);
                    contentCustomer = contentCustomer.Replace("{{NgayDatHang}}", DateTime.Now.ToString());
                    contentCustomer = contentCustomer.Replace("{{Phone}}", order1.Phone);
                    contentCustomer = contentCustomer.Replace("{{Email}}", order1.Email);
                    contentCustomer = contentCustomer.Replace("{{DiaChiGiaoHang}}", order1.Address);
                    contentCustomer = contentCustomer.Replace("{{ThanhTien}}",Banhangonline.Common.Common.FormatNumber( ThanhTien,0));
                    contentCustomer = contentCustomer.Replace("{{TongTien}}", Banhangonline.Common.Common.FormatNumber(TongTien, 0));
                    Banhangonline.Common.Common.SendMail("ShopOnline","Đơn Hàng #"+order.Code,contentCustomer,order.Email);

                    string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/send1.html"));
                    contentAdmin = contentAdmin.Replace("{{MaDon}}", order1.Code);
                    contentAdmin = contentAdmin.Replace("{{SanPham}}", strSanPham);
                    contentAdmin = contentAdmin.Replace("{{TenKhachHang}}", order1.CustomerName);
                    contentAdmin = contentAdmin.Replace("{{NgayDatHang}}", DateTime.Now.ToString());
                    contentAdmin = contentAdmin.Replace("{{Phone}}", order1.Phone);
                    contentAdmin = contentAdmin.Replace("{{Email}}", order1.Email);
                    contentAdmin = contentAdmin.Replace("{{DiaChiGiaoHang}}", order1.Address);
                    contentAdmin = contentAdmin.Replace("{{ThanhTien}}", Banhangonline.Common.Common.FormatNumber(ThanhTien, 0));
                    contentAdmin = contentAdmin.Replace("{{TongTien}}", Banhangonline.Common.Common.FormatNumber(TongTien, 0));
                    Banhangonline.Common.Common.SendMail("ShopOnline", "Đơn Hàng #" + order.Code, contentAdmin, ConfigurationManager.AppSettings["EmailAdmin"]);
                    cart.ClearCard();
                    //order1.Email = order.Email;
                     code = new { Success = true, code = 1 };
                }
            }

            return View();
        }
        public ActionResult Partial_Item_Cart()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }
        public ActionResult Partial_Item_ThanhToan()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }
        public ActionResult ShowCount()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return Json(new { Count = cart.Items.Count }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Count = 0 }, JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            var db = new ApplicationDbContext();
            var checkProduct = db.Products.FirstOrDefault(x => x.Id == id);
            if (checkProduct != null)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart == null)
                {
                    cart = new ShoppingCart();
                }
                ShoppingCartItems item = new ShoppingCartItems
                {
                    ProductId = checkProduct.Id,
                    ProductName = checkProduct.Title ,
                    CategoryName = checkProduct.ProductCategory.Title,
                    Alias = checkProduct.Alias,
                    Quantity = quantity
                };
                if (checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault) != null)
                {
                    item.ProductImg = checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault).Image;
                }
                item.Price = checkProduct.Price;
                if (checkProduct.PriceSale > 0)
                {
                    item.Price = (decimal)checkProduct.PriceSale;
                }
                item.TotalPrice = item.Quantity * item.Price;
                cart.AddToCart(item, quantity);
                Session["Cart"] = cart;
                code = new { Success = true, msg = "Thêm sản phẩm vào giở hàng thành công!", code = 1, Count = cart.Items.Count };
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };

            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {   var checkProduct = cart.Items.FirstOrDefault(x=>x.ProductId == id);
                if (checkProduct != null)
                {
                    cart.Items.Remove(checkProduct);
                    return Json(new { Success = true, msg = "", code = -1, Count = 0 });
                }
               
            }
            return Json(code);
        }
        [HttpPost]
        public ActionResult Update(int id, int quantity)
        {
         

            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {   
                cart.UpdateQuantity(id,quantity);
                return Json(new { Success = true });


            }
            return Json(new { Success = false });
        }
        [HttpPost]
        public ActionResult DeleteAll()
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };

            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {      
                    cart.Items.Clear();
                    return Json(new { Success = true, msg = "", code = -1, Count = 0 });
                

            }
            return Json(code);
        }
    }
}