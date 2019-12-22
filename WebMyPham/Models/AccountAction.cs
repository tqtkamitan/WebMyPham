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
    }
}