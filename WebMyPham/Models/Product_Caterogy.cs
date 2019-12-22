using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMyPham.Models
{
    public class Product_Caterogy
    {
        [Key, Column(Order = 0)]
        public int idProduct { get; set; }
        public string product { get; set; }
        [Key, Column(Order = 1)]
        public int idCaterogy { get; set; }
        public string caterogy { get; set; }
    }
}