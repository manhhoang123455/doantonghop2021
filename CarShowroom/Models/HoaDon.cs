using CarShowroom.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarShowroom.Models
{
    [Table("HoaDon")]
    public class HoaDon
    {
        [Key]
        [DisplayName("Mã hóa đơn")]
        public int MaHoaDon { get; set; }
        [DisplayName("Ngày nhận")]
        public DateTime NgayNhan { get; set; }
        [DisplayName("Ngày trả")]
        public DateTime NgayTra { get; set; }
        [DisplayName("Hình thức thanh toán")]
        public HinhThucThanhToan HinhThucThanhToan { get; set; }
        [DisplayName("Trạng thái thanh toán")]
        public int TrangThaiThanhToan { get; set; }
        [DisplayName("Tổng tiền")]
        public decimal TongThanhTien { get; set; }
        [DisplayName("Ghi chú")]
        public string GhiChu { get; set; }
        [DisplayName("Mã khách hàng")]
        public int MaKhachHang { get; set; }
        [ForeignKey("MaKhachHang")]
        public virtual KhachHang KhachHang { get; set; }
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDon { get; set; }

    }
}