using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarShowroom.Models;
using PagedList;

namespace CarShowroom.Areas.Admin.Controllers
{
    public class HangXeController : BaseController
    {

        // GET: Admin/HangXe
        [HttpGet]
        public ViewResult Index(string currentFilter, string searchString, int? page)
        {
            
            CarShowroomDbContext context = new  CarShowroomDbContext();
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            ViewBag.CurrentFilter = searchString;

            var HangXe = from s in context.HangXe
                select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                HangXe = HangXe.Where(s => s.TenHangXe.Contains(searchString)
                                               || s.Quocgia.Contains(searchString));
            }
          
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            //
            return View(HangXe.OrderBy(x => x.MaHangXe).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(HangXe lx)
        {
            CarShowroomDbContext context = new CarShowroomDbContext();
            if (ModelState.IsValid)
            {
                HangXe HangXe = new HangXe();
                HangXe.Quocgia = lx.Quocgia;
                HangXe.TenHangXe = lx.TenHangXe;
                context.HangXe.Add(HangXe);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(lx);
            }

        }
        public ActionResult Edit(int? id)
        {
            CarShowroomDbContext context = new CarShowroomDbContext();
            var res = context.HangXe.FirstOrDefault(m => m.MaHangXe == id);
            return View(res);

        }
        [HttpPost]
        public ActionResult Edit(HangXe lx)
        {
            CarShowroomDbContext context = new CarShowroomDbContext();
            if (ModelState.IsValid)
            {
                var res = context.HangXe.FirstOrDefault(m => m.MaHangXe == lx.MaHangXe);
                if (res != null)
                {
                    res.Quocgia = lx.Quocgia;
                    res.TenHangXe = lx.TenHangXe;
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(lx);
            }

        }
        public ActionResult Details(int? id)
        {
            CarShowroomDbContext context = new CarShowroomDbContext();
            var res = context.HangXe.FirstOrDefault(m => m.MaHangXe == id);
            return View(res);

        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            CarShowroomDbContext context = new CarShowroomDbContext();
            var res = context.HangXe.FirstOrDefault(m => m.MaHangXe == id);
            if (res != null)
            {
                context.HangXe.Remove(res);
                context.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }
    }
}