using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace CarShowroom.Models
{
    [Table("Xe")]
    public class Xe
    {
        [Key]
        [DisplayName("Mã Xe")]
        public int MaXe { get; set; }
        [DisplayName("Tên Xe")]
        [StringLength(50)]
        [Required(ErrorMessage = "Tên xe không được quá 50 ký tự")]
        public string TenXe { get; set; }
        [DisplayName("Số Chỗ")]
        [DefaultValue(999)]
        public int SoCho { get; set; }
        [DisplayName("Phiên Bản")]
        [Required(ErrorMessage = "Phiên Bản Không Được Trống")]
        public string PhienBan { get; set; }
        [DisplayName("Giá Nhập")]
        public decimal GiaNhap { get; set; }
        [DisplayName("Giá Cho Thuê")]
        public decimal GiaChoThue { get; set; }
        [DisplayName("Số Lượng Xe")]
        public int SoLuongXe { get; set; }
        public string ChiTietXe { get; set; }
        [DisplayName("Số Lượng Xe Đã Thuê")]
        public int? SoLuongXeDaThue { get; set; }
        [DisplayName("Hình Ảnh")]
        public string HinhAnh { get; set; }
        [DisplayName("Hãng Xe")]
        public int MaHangXe { get; set; }
        [ForeignKey("MaHangXe")]
        public virtual HangXe HangXe { get; set; }
        public virtual ICollection<BienSo> BienSo { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

    }
}