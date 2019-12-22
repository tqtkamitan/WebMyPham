using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMyPham.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idProduct { get; set; }
        [Required]
        public string productName { get; set; }
        [Required]
        public int price { get; set; }
        public string description { get; set; }
        public string producer { get; set; }
        public string img { get; set; }
        public string dateAdded { get; set; }
        public string status { get; set; }
    }
}