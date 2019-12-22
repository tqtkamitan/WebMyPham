using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMyPham.Models
{
    public class Product_CaterogyAction
    {
        public static List<Product_Caterogy> GetAll()
        {
            List<Product_Caterogy> list_product = null;
            using (var db = new DataContext())
            {
                list_product = db.Product_Caterogies.ToList();
                db.Dispose();
            }
            return list_product;
        }
    }
}