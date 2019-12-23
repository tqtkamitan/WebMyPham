using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebMyPham.Models
{
    public class AccountAction
    {
        public static void Create(string name, string phoneNumber, string address, string email, string password, string img) {
            using (var db = new DataContext())
            {
                db.Accounts.Add(new Account
                {
                    name = name,
                    phoneNumber = phoneNumber,
                    address = address,
                    email = email,
                    password = password,
                    img = img,
                    status = "Active",
                    role = "Staff",
                });
                db.SaveChanges();
                db.Dispose();
            }
        }

        public static List<Account> GetAll()
        {
            List<Account> list_acc = null;
            using (var db = new DataContext())
            {
                list_acc = db.Accounts.ToList();
                db.Dispose();
            }
            return list_acc;
        }

        public static List<Account> GetAllEmployee()
        {
            List<Account> list_acc = null;
            using (var db = new DataContext())
            {
                var links = from l in db.Accounts // lấy toàn bộ liên kết
                            where l.role.Contains("Nhân viên")
                            select l;
                list_acc = links.ToList();
                db.Dispose();
            }
            return list_acc;
        }

        public static void EditAccount(string email, string name, string phoneNumber, string address, string img, string password)
        {
            using (var db = new DataContext())
            {
                var product = db.Accounts.Find(email);
                product.name = name;
                product.phoneNumber = phoneNumber;
                product.address = address;
                product.img = img;
                product.password = password;
                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }
    }
}