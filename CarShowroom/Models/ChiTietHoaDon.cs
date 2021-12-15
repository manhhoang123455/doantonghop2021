using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarShowroom.Models
{
    [Table("ChiTietHoaDon")]
    public class ChiTietHoaDon
    {
        [Key,Column(Order =0)]
        public int MaXe { get; set; }
        [Key,Column(Order =1)]
        public int MaHoaDon { get; set; }
        public int SoLuong { get; set; }
        public string GhiChu { get; set; }
        public decimal ThanhTien { get; set; }
        [ForeignKey("MaXe")]
        public Xe Xe { get; set; }

        [ForeignKey("MaHoaDon")]

        public virtual HoaDon HoaDon { get; set; }
    }
}