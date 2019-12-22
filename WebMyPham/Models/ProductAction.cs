using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Globalization;
using WebMyPham.Models;
using System.IO;

namespace WebMyPham.Models
{
    public class ProductAction
    {
        public static void Create(string productName, int price, string description, string img, string producer, string[] caterogies, DateTime dateAdded, string status, HttpPostedFileBase[] imgs)
        {
            using (var db = new DataContext())
            {
                Product product = new Product
                {
                    productName = productName,
                    price = price,
                    description = description,
                    producer = producer,
                    img = img,
                    dateAdded = dateAdded.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    status = "Mới nhập",
                };
                db.Products.Add(product);
                db.SaveChanges();

                foreach (var idCaterogy in caterogies)
                {
                    var caterogy = db.Caterogies.Find(Int32.Parse(idCaterogy));
                    db.Product_Caterogies.Add(new Product_Caterogy
                    {
                        idProduct = product.idProduct,
                        product = productName,
                        idCaterogy = Int32.Parse(idCaterogy),
                        caterogy = caterogy.caterory,
                    });
                }
                db.SaveChanges();
                for (int i = 0; i < imgs.Length; i++)
                {
                    string _path = "";
                    string _FileName = Path.GetFileName(imgs[i].FileName);
                    _path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("/UploadedFiles"), _FileName);
                    imgs[i].SaveAs(_path);
                    productImg.addImgChapter(product.idProduct, "/UploadedFiles/" + _FileName);
                }
                db.Dispose();
            }
        }

        public static void EditProduct(int id, string productName, int price, string description, string img, string producer, string[] caterogies, DateTime dateAdded, string status)
        {
            using (var db = new DataContext())
            {
                var product = db.Products.Find(id);
                product.productName = productName;
                product.price = price;
                product.description = description;
                product.img = img;
                product.producer = producer;
                product.dateAdded = dateAdded.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                product.status = "Mới nhập";
                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                bool hadUpdated = false;
                foreach (var idCaterogy in caterogies)
                {
                    var caterogy = db.Caterogies.Find(Int32.Parse(idCaterogy));
                    if (hadUpdated == false)
                    {
                        db.Product_Caterogies.RemoveRange(db.Product_Caterogies.Where(x => x.idProduct == id));
                        db.SaveChanges();
                    }
                    db.Product_Caterogies.Add(new Product_Caterogy
                    {
                        idProduct = product.idProduct,
                        product = productName,
                        idCaterogy = Int32.Parse(idCaterogy),
                        caterogy = caterogy.caterory,
                    });
                    db.SaveChanges();
                    hadUpdated = true;
                }

                db.Dispose();
            }
        }

        public static List<Product> GetAll()
        {
            List<Product> list_product = null;
            using (var db = new DataContext())
            {
                list_product = db.Products.ToList();
                db.Dispose();
            }
            return list_product;
        }
    }
}