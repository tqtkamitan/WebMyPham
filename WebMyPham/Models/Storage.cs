using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMyPham.Models
{
    public class Storage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idStore { get; set; }
        public string product { get; set; }
        public int numberStored { get; set; }
        public int importPrice { get; set; }
    }
}