using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarShowroom.Models;

namespace CarShowroom.Areas.Admin.Controllers
{
    public class DonHangController : BaseController
    {
        // GET: DonHang
        public ActionResult Index()
        {
            CarShowroomDbContext DbContext = new CarShowroomDbContext();
            var result = from h in DbContext.HoaDon
                         join kh in DbContext.KhachHang on h.MaKhachHang equals kh.MaKhachHang
                         select new khachhang_hoadon()
                         {
                             Khachhang = kh,
                             HoaDon = h
                         };
            return View(result.ToList());
        }
        //  Details

        [HttpGet]
        public ActionResult Details(int id)
        {
            CarShowroomDbContext DbContext = new CarShowroomDbContext();
            var result = from h in DbContext.HoaDon
                         join kh in DbContext.KhachHang on h.MaKhachHang equals kh.MaKhachHang 
                         where h.MaHoaDon == id
                         select new khachhang_hoadon()
                         {
                             Khachhang = kh,
                             HoaDon = h
                         };
            return View(result.FirstOrDefault());
        }

        [HttpGet]
        public ActionResult getListProduct(int id)
        {
            CarShowroomDbContext DbContext = new CarShowroomDbContext();
            var result = from cthd in DbContext.ChiTietHoaDon
                         join x in DbContext.Xe on cthd.MaXe equals x.MaXe
                         where cthd.MaHoaDon == id
                         select new ChiTietHoaDon_Xe()
                         {
                             ChiTietHoaDon= cthd,
                             Xe = x
                         };
            return View("getListProduct", result);

        }
    }
}