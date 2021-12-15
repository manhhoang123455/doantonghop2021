using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using CarShowroom.Models;
using PagedList;

namespace CarShowroom.Areas.Admin.Controllers
{
    public class XeController : BaseController
    {
        // GET: Admin/Xe
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

            var xe = from s in context.Xe
                     select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                xe = xe.Where(s => s.TenXe.Contains(searchString));
            }

            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View(xe.OrderBy(x => x.MaXe).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            CarShowroomDbContext context = new CarShowroomDbContext();
            var dsHangXe = context.HangXe.ToList();

            TempData["dsHangXe"] = dsHangXe;


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Xe lx)
        {

            if (ModelState.IsValid)
            {
                
                CarShowroomDbContext context = new CarShowroomDbContext();
                string fileName = Path.GetFileName(lx.ImageFile.FileName);
                string extension = Path.GetExtension(fileName);
                if (extension != ".jpg" && extension != ".png" && extension != ".jpeg")
                {
                    
                    var dsHangXe = context.HangXe.ToList();
                    ViewBag.ImageError = "Chỉ Chấp Nhận Định Dạng Ảnh (jpp,png,jpeg)";
                    TempData["dsHangXe"] = dsHangXe;
                    return View(lx);
                }
               
                string path = Path.Combine(Server.MapPath("~/App_Themes/admin/img/product"), fileName);

                WebImage img = new WebImage(lx.ImageFile.InputStream);
                if (img.Width > 300)
                    img.Resize(300, 273);
                img.Save(path);
                
               
                Xe xe = new Xe();
                xe.ChiTietXe = lx.ChiTietXe;
                xe.BienSo = lx.BienSo;
                xe.GiaChoThue = lx.GiaChoThue;
                xe.GiaNhap = lx.GiaNhap;
                xe.HinhAnh = "/App_Themes/admin/img/product/"+ fileName;
                xe.HangXe = lx.HangXe;
                xe.MaXe = lx.MaXe;
                xe.MaHangXe = lx.MaHangXe;
                xe.SoLuongXe = lx.SoLuongXe;
                xe.TenXe = lx.TenXe;
                xe.SoCho = lx.SoCho;
                xe.PhienBan = lx.PhienBan;
                context.Xe.Add(xe);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                CarShowroomDbContext context = new CarShowroomDbContext();
                var dsHangXe = context.HangXe.ToList();

                TempData["dsHangXe"] = dsHangXe;
                return View(lx);
            }



        }
        public ActionResult Edit(int? id)
        {
            CarShowroomDbContext context = new CarShowroomDbContext();
            var dsHangXe = context.HangXe.ToList();

            TempData["dsHangXe"] = dsHangXe;
            var res = context.Xe.FirstOrDefault(m => m.MaXe == id);
            return View(res);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Xe lx)
        {
            CarShowroomDbContext context = new CarShowroomDbContext();
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileName(lx.ImageFile.FileName);
                string extension = Path.GetExtension(fileName);
                if (extension != ".jpg" && extension != ".png" && extension != ".jpeg")
                {

                    var dsHangXe = context.HangXe.ToList();
                    ViewBag.ImageError = "Chỉ Chấp Nhận Định Dạng Ảnh (jpp,png,jpeg)";
                    TempData["dsHangXe"] = dsHangXe;
                    return View(lx);
                }
                string path = Path.Combine(Server.MapPath("~/App_Themes/admin/img/product"), fileName);
                lx.ImageFile.SaveAs(path);
                var res = context.Xe.FirstOrDefault(m => m.MaXe == lx.MaXe);
                if (res != null)
                {
                    res.ChiTietXe = lx.ChiTietXe;
                    res.BienSo = lx.BienSo;
                    res.GiaChoThue = lx.GiaChoThue;
                    res.GiaNhap = lx.GiaNhap;
                    res.HinhAnh = "/App_Themes/admin/img/product/" + fileName;
                    res.HangXe = lx.HangXe;
                    res.MaXe = lx.MaXe;
                    res.SoLuongXe = lx.SoLuongXe;
                    res.MaHangXe = lx.MaHangXe;
                    res.TenXe = lx.TenXe;
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                var dsHangXe = context.HangXe.ToList();

                TempData["dsHangXe"] = dsHangXe;
                return View(lx);
            }

        }
        public ActionResult Details(int? id)
        {
            CarShowroomDbContext context = new CarShowroomDbContext();
            var res = context.Xe.FirstOrDefault(m => m.MaXe == id);
            return View(res);

        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            CarShowroomDbContext context = new CarShowroomDbContext();
            var res = context.Xe.FirstOrDefault(m => m.MaXe == id);
            if (res != null)
            {
                context.Xe.Remove(res);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}