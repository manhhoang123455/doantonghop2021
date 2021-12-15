using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarShowroom.Models
{
    [Table("HangXe")]
    public class HangXe
    {
        [Key]
        public int MaHangXe { get; set; }
        [DisplayName("Tên Hãng Xe")]
        [Required(ErrorMessage = "Tên Hãng Xe không được trống")]
        public string TenHangXe { get; set; }
        [DisplayName("Quốc gia")]
        [Required(ErrorMessage = "Tên Quốc gia không được trống")]
        public string Quocgia { get; set; }
        public virtual ICollection<Xe> Xe { get; set; }
    }
}