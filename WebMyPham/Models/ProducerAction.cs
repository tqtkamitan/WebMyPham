using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebMyPham.Models
{
    public class ProducerAction
    {
        public static void Create(string producer)
        {
            using (var db = new DataContext())
            {
                db.Producers.Add(new Producer { producer = producer });
                db.SaveChanges();
                db.Dispose();
            }
        }

        public static List<Producer> GetAll()
        {
            List<Producer> list_producer = null;
            using (var db = new DataContext())
            {
                list_producer = db.Producers.ToList();
                db.Dispose();
            }
            return list_producer;
        }
    }
}