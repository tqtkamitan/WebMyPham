using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using WebMyPham.Models;

namespace WebMyPham.Controllers
{
    public class HomeController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult Index()
        {
            ViewBag.Account = AccountAction.GetAll();
            var randomProduct = db.Products.OrderBy(x => Guid.NewGuid()).Take(6);
            var randomCaterogy = db.Caterogies.OrderBy(x => Guid.NewGuid()).Take(6);
            var randomProducer = db.Producers.OrderBy(x => Guid.NewGuid()).Take(6);
            ViewBag.Product = randomProduct;
            ViewBag.randomCaterogy = randomCaterogy;
            ViewBag.randomProducer = randomProducer;
            ViewBag.NewProduct = ProductAction.GetAll();
            List<PayCheck> giohang = Session["giohang"] as List<PayCheck>;
            return View(giohang);
        }

        public ActionResult Search(string searchString)
        {
            ViewBag.Account = AccountAction.GetAll();
            db.SaveChanges();
            if (searchString != "")
            {
                var links = from l in db.Products // lấy toàn bộ liên kết
                            where l.productName.Contains(searchString) || l.producer.Contains(searchString)
                            select l;
                List<Product> chapter_list = links.ToList();
                ViewBag.Product = chapter_list;
                return View();
            }
            else
            {
                var link = from l in db.Products
                           select l;
                List<Product> chapter_list = link.ToList();
                ViewBag.Product = chapter_list;
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Account = AccountAction.GetAll();
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Account = AccountAction.GetAll();
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ViewProduct(int id) {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Account = AccountAction.GetAll();
            ViewBag.Product = ProductAction.GetAll();
            ViewBag.ProductCaterogy = Product_CaterogyAction.GetAll();
            ViewBag.Caterogy = CateroryAction.GetAll();
            ViewBag.ProductImg = productImg.getAllProductImg(id);
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult DanhSachSanPham()
        {
            ViewBag.Account = AccountAction.GetAll();
            ViewBag.Product = ProductAction.GetAll();
            return View();
        }

        public ActionResult DanhSachTheLoai()
        {
            ViewBag.Caterogy = CateroryAction.GetAll();
            return View();
        }

        public ActionResult ViewTheLoai(int id)
        {
            var links = from l in db.Product_Caterogies // lấy toàn bộ liên kết
                        where l.idCaterogy.Equals(id)
                        select l;
            ViewBag.Account = AccountAction.GetAll();
            ViewBag.Product = ProductAction.GetAll();
            ViewBag.ProductCaterogy = Product_CaterogyAction.GetAll();
            ViewBag.Caterogy = CateroryAction.GetAll();
            ViewBag.caterogyId = links;
            return View();
        }
    }
}