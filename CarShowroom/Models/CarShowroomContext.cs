using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CarShowroom.Models
{
    public class CarShowroomDbContext:DbContext
    {
        public CarShowroomDbContext(): base("SqlConnection")
        {

        }
        static CarShowroomDbContext()
        {
            Database.SetInitializer<CarShowroomDbContext>(new KhoiTaoDuLieu());
        }
        public DbSet<TaiKhoan> TaiKhoan { get; set; }
        public DbSet<HangXe> HangXe { get; set; }
        public DbSet<Xe> Xe { get; set; }
        public DbSet<HoaDon> HoaDon { get; set; }
        public DbSet<ChuDeBaiViet> Topics { get; set; }
        public DbSet<BaiViet> Posts { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDon { get; set; }
        public DbSet<KhachHang> KhachHang { get; set; }
    }
}