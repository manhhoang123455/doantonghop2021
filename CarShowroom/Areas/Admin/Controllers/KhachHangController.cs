using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarShowroom.Models;
using PagedList;

namespace CarShowroom.Areas.Admin.Controllers
{
    public class KhachHangController : BaseController
    {
        // GET: Admin/KhachHang
        // GET: Admin/HangXe
        [HttpGet]
        public ViewResult Index(string currentFilter, string searchString, int? page)
        {

            CarShowroomDbContext context = new CarShowroomDbContext();
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            ViewBag.CurrentFilter = searchString;

            var khachHang = from s in context.KhachHang
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                khachHang = khachHang.Where(s => s.SoDienThoai.Contains(searchString));
            }

            int pageSize = 2;
            int pageNumber = (page ?? 1);
            //
            return View(khachHang.OrderBy(x => x.MaKhachHang).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Edit(int? id)
        {
            CarShowroomDbContext context = new CarShowroomDbContext();
            var res = context.KhachHang.FirstOrDefault(m => m.MaKhachHang == id);
            List<string> listNH = new List<string>()
            {
                "TPBank",
                "VietcomBank",
                "ViettinBank",
                "SeABank",
                "VPBank",
                "SacomBank",
                "TechcomBank",
                "BIDV",
                "DongABank"
            };
            SelectList selectList = new SelectList(listNH);
            ViewBag.listNH = selectList;
            return View(res);

        }
        [HttpPost]
        public ActionResult Edit(KhachHang kh)
        {
            CarShowroomDbContext context = new CarShowroomDbContext();
            if (ModelState.IsValid)
            {
                var res = context.KhachHang.FirstOrDefault(m => m.MaKhachHang == kh.MaKhachHang);
                if (res != null)
                {
                    res.SoDienThoai = kh.SoDienThoai;
                    res.CMND = kh.CMND;
                    res.DiaChi = kh.DiaChi;
                    res.NganHang = kh.NganHang;
                    res.TenKhachHang = kh.TenKhachHang;
                    res.SoTaiKhoan = kh.SoTaiKhoan;
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(kh);
            }

        }
        public ActionResult Details(int? id)
        {
            CarShowroomDbContext context = new CarShowroomDbContext();
            var res = context.KhachHang.FirstOrDefault(m => m.MaKhachHang == id);
            return View(res);

        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            CarShowroomDbContext context = new CarShowroomDbContext();
            var res = context.KhachHang.FirstOrDefault(m => m.MaKhachHang == id);
            if (res != null)
            {
                context.KhachHang.Remove(res);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}