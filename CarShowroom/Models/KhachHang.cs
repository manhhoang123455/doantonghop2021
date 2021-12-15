using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarShowroom.Models
{
    [Table("KhachHang")]
    public class KhachHang
    {
        [Key,ForeignKey("TaiKhoan")]
        [DisplayName("Mã Khách Hàng")]
        public int MaKhachHang { get; set; }
        [StringLength(50, ErrorMessage = "Tên khách hàng không được quá 50 ký tự")]
        [Required(ErrorMessage = "Tên người dùng không được để trống")]
        [DisplayName("Tên Người Dùng")]
        public string TenKhachHang { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Năm Sinh")]
        public DateTime NamSinh { get; set; }
        [StringLength(100, ErrorMessage = "Địa chỉ không được quá 100 ký tự")]
        [Required(ErrorMessage = "Địa chỉ dùng không được để trống")]
        [DisplayName("Địa Chỉ")]
        public string DiaChi { get; set; }
        [StringLength(11, ErrorMessage = "Số tài khoản không được quá 11 ký tự")]
        [Required()]
        [DisplayName("Số Tài Khoản")]
        public string SoTaiKhoan { get; set; }
        [DisplayName("Ngân Hàng")]
        [StringLength(20)]
        public string NganHang { get; set; }
        [StringLength(9, ErrorMessage = "Chứng minh nhân dân không được quá 9 ký tự")]
        [Required(ErrorMessage = "Chứng minh nhân dân không được để trống")]
        [DisplayName("Chứng Minh Nhân Dân")]
        public string CMND { get; set; }
        [StringLength(11,ErrorMessage = "Số điện thoại không được quá 11 ký tự")]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [DisplayName("Số Điện Thoại")]
        public string SoDienThoai { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}