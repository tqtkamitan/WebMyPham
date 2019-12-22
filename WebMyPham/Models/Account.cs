using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebMyPham.Models
{
    public class Account
    {
        
        [Required]
        public string name { get; set; }
        [Required]
        [Key]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public string img { get; set; }
        public int VIP_Point { get; set; }
        public string status { get; set; }
        public string role { get; set; }
    }
}