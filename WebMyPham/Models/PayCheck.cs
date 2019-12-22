using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMyPham.Models
{
    public class PayCheck
    {
        [Key]
        public int idPay { get; set; }
        public string account { get; set; }
        public string customerName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public string product { get; set; }
        public int amount { get; set; }
        public int price { get; set; }
        public int totalMoney { get {
                return amount * price;
            }
        }
        public bool hadDelivered { get; set; }
        public DateTime DateAdded { get; set; }

        public static List<PayCheck> GetAll()
        {
            List<PayCheck> list_pay = null;
            using (var db = new DataContext())
            {
                list_pay = db.PayChecks.ToList();
                db.Dispose();
            }
            list_pay.Reverse();
            return list_pay;
        }
    }
}