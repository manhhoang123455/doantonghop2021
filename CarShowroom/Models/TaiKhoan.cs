using CarShowroom.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarShowroom.Models
{
    [Table("TaiKhoan")]
    public class TaiKhoan
    {
        [Key]
        public int MaTaiKhoan { get; set; }
        [StringLength(20, ErrorMessage = "Tên tài khoản không được quá 20 ký tự")]
        [Required(ErrorMessage = "Tên tài khoản không được để trống")]
        public string TenTaiKhoan { get; set; }
        [StringLength(20, ErrorMessage = "Mật khẩu không được quá 20 ký tự")]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string MatKhau { get; set; }
        public QuyenHan QuyenHan { get; set; }
        public virtual KhachHang KhachHang { get; set; }
    }
}