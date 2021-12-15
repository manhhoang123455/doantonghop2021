using CarShowroom.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarShowroom.Controllers
{
    public class BaiVietCusController : Controller
    {
        CarShowroomDbContext db = new CarShowroomDbContext();
        // GET: BaiVietCus
        public ActionResult Index(int? page)
        {
            ViewBag.url = "bai-viet";
            if (page == null) page = 1;
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            var list = db.Posts.Where(m => m.status == 1).OrderByDescending(m => m.ID).ToList();
            return View("index", list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult postDetail(string slug)
        {
            ViewBag.post = db.Posts.Where(m => m.status == 1).OrderByDescending(m => m.ID).ToList();
            var siglePost = db.Posts.Where(m => m.status == 1 && m.slug == slug).FirstOrDefault();
            return View("postDetail", siglePost);
        }
    }
}