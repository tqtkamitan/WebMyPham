using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace WebMyPham.Models
{
    public class productImg
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idImg { get; set; }
        [Key, Column(Order = 1)]
        public int idProduct { get; set; }
        [Required]
        public string Imgs { get; set; }

        public static List<productImg> getAllProductImg(int id)
        {
            using (var db = new DataContext())
            {
                var links = from l in db.ProductImgs // lấy toàn bộ liên kết
                            where l.idProduct.Equals(id)
                            select l;
                List<productImg> chapter_list = links.ToList();
                return chapter_list;
            }
        }

        public static void addImgChapter(int id, string imgs)
        {
            using (var db = new DataContext())
            {
                var book = db.Products.Find(id);
                db.ProductImgs.Add(new productImg
                {
                    idProduct = id,
                    Imgs = imgs,
                });
                db.SaveChanges();
                db.Dispose();
            }
        }
    }
}