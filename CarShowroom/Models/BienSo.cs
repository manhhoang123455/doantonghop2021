using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarShowroom.Models
{
    [Table("BienSo")]
    public class BienSo
    {
        [DisplayName("Biển Số")]
        [Key]
        public string BienSoXe { get; set; }
        public int MaXe { get; set; }
        [ForeignKey("MaXe")]
        public virtual Xe Xe { get; set; }
    }
}