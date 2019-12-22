using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using System.Net;
using System.Net.Mail;
using WebMyPham.Models;

namespace WebMyPham.Controllers
{
    public class UserController : Controller
    {
        private DataContext db = new DataContext();

        // GET: User
        public ActionResult Login([Bind(Include = "email, password")]Account systemUser)
        {
            ViewBag.Account = AccountAction.GetAll();
            if (Session["user"] != null) return RedirectToAction("Index", "Home");
            string email = systemUser.email;
            string password = systemUser.password;

            using (var db = new DataContext())
            {
                Account user = db.Accounts.Find(email);
                if (user != null)
                {
                    string taikhoan = user.email;
                    string matkhau = user.password;
                    //string AccStyle = user.status;
                    if (email.Equals(taikhoan) && password.Equals(matkhau))
                    {
                        Session.Add("user", user.email);
                        if (Session["giohang"] != null)
                        {
                            List<PayCheck> giohang = Session["giohang"] as List<PayCheck>;
                            for (int i = 0; i < giohang.Count; i++)
                            {
                                PayCheck itemSua = giohang.FirstOrDefault(m => m.idPay == i);
                                if (itemSua != null)
                                {
                                    itemSua.account = Session["user"].ToString();
                                }
                            }
                        }
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                    else {
                        ViewBag.Noti = "<h3 class='text-danger'>Sai thông tin đăng nhập</h3>";
                        return View();
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.Account = AccountAction.GetAll();
            if (Session["user"] != null) return RedirectToAction("Index", "Home");
            ViewBag.Accounts = AccountAction.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult Register(string name, string phoneNumber, string address, string email, string password, string password2)
        {
            if (Session["user"] != null) return RedirectToAction("Index", "Home");
            Session.Clear();
            using (var db = new DataContext())
            {
                Account user = db.Accounts.Find(email);
                if (user != null)
                {
                    ViewBag.Noti = "<h3 class='text-danger'>Email này đã được đăng kí</h3>";
                    return Redirect("~/User/Register");
                }
                if (password != password2)
                {
                    ViewBag.Noti = "<h3 class='text-danger'>Password nhập lại phải trùng Password nhập ban đầu</h3>";
                    return Redirect("~/User/Register");
                }
            }
            string img = "/UploadedFiles/anonymous-profile.jpg";
            AccountAction.Create(name, phoneNumber, address, email, password, img);
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("quangtan.tr@gmail.com");
            mailMessage.To.Add(email);
            mailMessage.Subject = "Asene,do not reply!";
            mailMessage.Body = "Cảm ơn đã sữ dụng dịch vụ của trang web chúng tôi";
            mailMessage.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Credentials = new NetworkCredential("quangtan.tr@gmail.com", "kirito1998");
            Session.Add("user", email);
            try
            {
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult EditUserInfo(int id)
        {
            return View();
        }
        public ActionResult AccountList()
        {
            return View();
        }

        public ActionResult EmployeeList()
        {
            return View();
        }
    }
}