using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using System.Net;
using WebMyPham.Models;

namespace WebMyPham.Controllers
{
    public class AdminController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Admin
        public ActionResult AdminSite()
        {
            if (Session["user"] == null) return RedirectToAction("Login", "User");
            Account account = db.Accounts.Find(Session["user"].ToString());
            if (account.role != "Nhân viên")
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Account = AccountAction.GetAll();
            return View();
        }
        public ActionResult ProductList()
        {
            if (Session["user"] == null) return RedirectToAction("Login", "User");
            Account account = db.Accounts.Find(Session["user"].ToString());
            if (account.role != "Nhân viên")
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Account = AccountAction.GetAll();
            ViewBag.Product = ProductAction.GetAll();
            ViewBag.ProductCaterogy = Product_CaterogyAction.GetAll();
            ViewBag.Caterogy = CateroryAction.GetAll();
            ViewBag.Producer = ProducerAction.GetAll();
            return View();
        }

        [HttpGet]
        public ActionResult AddProduct() {
            if (Session["user"] == null) return RedirectToAction("Login", "User");
            Account account = db.Accounts.Find(Session["user"].ToString());
            if (account.role != "Nhân viên")
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Account = AccountAction.GetAll();
            ViewBag.Caterory = CateroryAction.GetAll();
            ViewBag.Producer = ProducerAction.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(string productName, int price, string description, string producer, string[] caterogy, DateTime dateAdded, string status, HttpPostedFileBase img, HttpPostedFileBase[] imgs) {
            string _path = "";
            if (productName == null)
            {
                ViewBag.Noti = "<h3 class='text-danger'>Xin hãy viết tên sản phẩm</h3>";
                return View();
            }
            else if (price <= 0)
            {
                ViewBag.Noti = "<h3 class='text-danger'>Xin hãy viết giá sản phẩm</h3>";
                return View();
            }
            else if (producer == null)
            {
                ViewBag.Noti = "<h3 class='text-danger'>Xin hãy chọn hãng sản xuất sản phẩm</h3>";
                return View();
            }
            else if (caterogy.Length == 0)
            {
                ViewBag.Noti = "<h3 class='text-danger'>Xin hãy chọn loại sản phẩm</h3>";
                return View();
            }
            else if (img == null)
            {
                ViewBag.Noti = "<h3 class='text-danger'>Xin hãy chọn hình</h3>";
                return View();
            }
            if (img.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(img.FileName);
                _path = Path.Combine(Server.MapPath("/UploadedFiles"), _FileName);
                img.SaveAs(_path);
                ProductAction.Create(productName, price, description, "/UploadedFiles/" + _FileName, producer, caterogy, dateAdded, status, imgs);
            }
            return Redirect("~/Admin/ProductList");
        }

        [HttpPost]
        public ActionResult EditProduct(int id, string productName, int price, string description, string producer, string[] caterogy, DateTime dateAdded, string status, HttpPostedFileBase img)
        {
            if (productName == null)
            {
                ViewBag.Noti = "<h3 class='text-danger'>Xin hãy viết tên sản phẩm</h3>";
                return View();
            }
            else if (price <= 0)
            {
                ViewBag.Noti = "<h3 class='text-danger'>Xin hãy viết giá sản phẩm</h3>";
                return View();
            }
            else if (producer == null)
            {
                ViewBag.Noti = "<h3 class='text-danger'>Xin hãy chọn hãng sản xuất sản phẩm</h3>";
                return View();
            }
            else if (caterogy.Length == 0)
            {
                ViewBag.Noti = "<h3 class='text-danger'>Xin hãy chọn loại sản phẩm</h3>";
                return View();
            }
            else if (img == null)
            {
                ViewBag.Noti = "<h3 class='text-danger'>Xin hãy chọn hình</h3>";
                return View();
            }
            string _path = "";
            if (img == null)
            {
                var book = db.Products.Find(id);
                string _FileName = book.img;
                ProductAction.EditProduct(id, productName, price, description, _FileName, producer, caterogy, dateAdded, status);
            }
            else
            {
                string _FileName = Path.GetFileName(img.FileName);
                _path = Path.Combine(Server.MapPath("/UploadedFiles"), _FileName);
                img.SaveAs(_path);
                ProductAction.EditProduct(id, productName, price, description, "/UploadedFiles/" + _FileName, producer, caterogy, dateAdded, status);
            }
            
            return Redirect("~/Admin/ProductList");
        }

        public ActionResult ProducerList() {
            if (Session["user"] == null) return RedirectToAction("Login", "User");
            Account account = db.Accounts.Find(Session["user"].ToString());
            if (account.role != "Nhân viên")
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Account = AccountAction.GetAll();
            ViewBag.Producer = ProducerAction.GetAll();
            return View();
        }

        [HttpGet]
        public ActionResult AddProducer()
        {
            Account account = db.Accounts.Find(Session["user"].ToString());
            if (account.role != "Nhân viên")
            {
                return RedirectToAction("Index", "Home");
            }
            if (Session["user"] == null) return RedirectToAction("Login", "User");
            ViewBag.Account = AccountAction.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult AddProducer(string producer)
        {
            if (producer == null)
            {
                ViewBag.Noti = "<h3 class='text-danger'>Xin viết tên hãng sản xuất</h3>";
                return View();
            }
            ProducerAction.Create(producer);
            return Redirect("~/Admin/ProducerList");
        }

        [HttpGet]
        public ActionResult CateroryList()
        {
            if (Session["user"] == null) return RedirectToAction("Login", "User");
            Account account = db.Accounts.Find(Session["user"].ToString());
            if (account.role != "Nhân viên")
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Account = AccountAction.GetAll();
            ViewBag.Caterory = CateroryAction.GetAll();
            return View();
        }

        [HttpGet]
        public ActionResult AddCaterory()
        {
            if (Session["user"] == null) return RedirectToAction("Login", "User");
            Account account = db.Accounts.Find(Session["user"].ToString());
            if (account.role != "Nhân viên")
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Account = AccountAction.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult AddCaterory(string caterory)
        {
            if (caterory == null)
            {
                ViewBag.Noti = "<h3 class='text-danger'>Xin viết loại sản phẩm</h3>";
                return View();
            }
            CateroryAction.Create(caterory);
            return Redirect("~/Admin/CateroryList");
        }

        public ActionResult DoanhThu()
        {
            ViewBag.Account = AccountAction.GetAll();
            if (Session["user"] == null) return RedirectToAction("Login", "User");
            Account account = db.Accounts.Find(Session["user"].ToString());
            if (account.role != "Nhân viên")
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.HoaDon = PayCheck.GetAll();
            return View();
        }
    }
}