using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMyPham.Models;

namespace WebMyPham.Controllers
{
    public class CartController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Cart
        public ActionResult Index()
        {
            ViewBag.Account = AccountAction.GetAll();
            List<PayCheck> giohang = Session["giohang"] as List<PayCheck>;
            return View(giohang);
        }

        public RedirectToRouteResult ThemVaoGio(int idPay)
        {
            if (Session["giohang"] == null) // Nếu giỏ hàng chưa được khởi tạo
            {
                Session["giohang"] = new List<PayCheck>();  // Khởi tạo Session["giohang"] là 1 List<CartItem>
            }

            List<PayCheck> giohang = Session["giohang"] as List<PayCheck>;  // Gán qua biến giohang dễ code

            // Kiểm tra xem sản phẩm khách đang chọn đã có trong giỏ hàng chưa

            if (giohang.FirstOrDefault(m => m.idPay == idPay) == null) // ko co sp nay trong gio hang
            {
                Product sp = db.Products.Find(idPay);  // tim sp theo sanPhamID
                string user = "";
                if (Session["user"] != null)
                {
                    user = Session["user"].ToString();
                }
                //
                PayCheck newItem = null;
                newItem = new PayCheck()
                {
                        idPay = idPay,
                        account = user,
                        product = sp.productName,
                        amount = 1,
                        price = sp.price
                };  // Tạo ra 1 CartItem mới

                giohang.Add(newItem);  // Thêm CartItem vào giỏ 
            }
            else
            {
                // Nếu sản phẩm khách chọn đã có trong giỏ hàng thì không thêm vào giỏ nữa mà tăng số lượng lên.
                PayCheck cardItem = giohang.FirstOrDefault(m => m.idPay == idPay);
                cardItem.amount++;
            }

            // Action này sẽ chuyển hướng về trang chi tiết sp khi khách hàng đặt vào giỏ thành công. Bạn có thể chuyển về chính trang khách hàng vừa đứng bằng lệnh return Redirect(Request.UrlReferrer.ToString()); nếu muốn.
            return RedirectToAction("Index", "Cart", new { id = idPay });
        }

        public RedirectToRouteResult SuaSoLuong(int idPay, int soluongmoi)
        {
            // tìm carditem muon sua
            List<PayCheck> giohang = Session["giohang"] as List<PayCheck>;
            PayCheck itemSua = giohang.FirstOrDefault(m => m.idPay == idPay);
            if (itemSua != null)
            {
                itemSua.amount = soluongmoi;
            }
            return RedirectToAction("Index");

        }

        public RedirectToRouteResult XoaKhoiGio(int idPay)
        {
            List<PayCheck> giohang = Session["giohang"] as List<PayCheck>;
            PayCheck itemXoa = giohang.FirstOrDefault(m => m.idPay == idPay);
            if (itemXoa != null)
            {
                giohang.Remove(itemXoa);
            }
            return RedirectToAction("Index");
        }

        public ActionResult ThanhToan()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            string user = Session["user"].ToString();
            var products = Session["giohang"];
            List<PayCheck> giohang = Session["giohang"] as List<PayCheck>;
            PayCheck[] objects = giohang.ConvertAll<PayCheck>(item => (PayCheck)item).ToArray();
            DateTime now = DateTime.Now;
            for (int i = 0; i < objects.Length; i++)
            {
                Account account = db.Accounts.Find(objects[i].account);
                PayCheck paycheck = new PayCheck
                {
                    account = account.email,
                    customerName = account.name,
                    email = account.email,
                    phoneNumber = account.phoneNumber,
                    address = account.address,
                    product = objects[i].product,
                    amount = objects[i].amount,
                    price = objects[i].price,
                    hadDelivered = false,
                    DateAdded = now,
                };
                db.PayChecks.Add(paycheck);
            }
            Session["giohang"] = null;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult HoaDonKhachHang()
        {
            ViewBag.Account = AccountAction.GetAll();
            if (Session["user"] == null) return RedirectToAction("Login", "User");
            ViewBag.HoaDon = PayCheck.GetAll();
            return View();
        }

        public ActionResult DaGiaoHang(int id)
        {
            ViewBag.Account = AccountAction.GetAll();
            if (Session["user"] == null) return RedirectToAction("Login", "User");
            PayCheck hoaDon = db.PayChecks.Find(id);
            hoaDon.hadDelivered = true;
            db.Entry(hoaDon).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
            return Redirect("~/Cart/DonDatHang");
        }

        public ActionResult DonDatHang()
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