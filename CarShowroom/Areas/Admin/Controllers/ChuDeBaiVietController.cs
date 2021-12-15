using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarShowroom.Areas.Admin.Controllers;
using CarShowroom.Models;

namespace CarShowroom.Areas.Admin.Controllers
{
    public class ChuDeBaiVietController : BaseController
    {
        CarShowroomDbContext db = new CarShowroomDbContext();

        // GET: Bdmin/Topic
        public ActionResult Index()
        {

            var list = db.Topics.Where(m => m.status != 0).OrderByDescending(m => m.ID).ToList();
            return View("index", list);
        }

        // GET: Bdmin/Topic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuDeBaiViet ChuDeBaiViet = db.Topics.Find(id);
            if (ChuDeBaiViet == null)
            {
                return HttpNotFound();
            }
            return View(ChuDeBaiViet);
        }

        // GET: Bdmin/Topic/Create
        public ActionResult Create()
        {
            ViewBag.listtopic = db.Topics.Where(m => m.status != 0).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChuDeBaiViet ChuDeBaiViet)
        {
            if (ModelState.IsValid)
            {
                //category
                string slug = Mystring.ToSlug(ChuDeBaiViet.name.ToString());
                ChuDeBaiViet.slug = slug;
                ChuDeBaiViet.parentid = 0;
                db.Topics.Add(ChuDeBaiViet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.listtopic = db.Topics.Where(m => m.status != 0).ToList();
            return View(ChuDeBaiViet);
        }

        // GET: Bdmin/Topic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuDeBaiViet ChuDeBaiViet = db.Topics.Find(id);
            if (ChuDeBaiViet == null)
            {
                return HttpNotFound();
            }
            ViewBag.listtopic = db.Topics.Where(m => m.status != 0).ToList();
            return View(ChuDeBaiViet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ChuDeBaiViet ChuDeBaiViet)
        {
            if (ModelState.IsValid)
            {
                string slug = Mystring.ToSlug(ChuDeBaiViet.name.ToString());
                ChuDeBaiViet.slug = slug;
                ChuDeBaiViet.parentid = 0;
                db.Entry(ChuDeBaiViet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.listtopic = db.Topics.Where(m => m.status != 0).ToList();
            return View(ChuDeBaiViet);
        }


        public ActionResult delete(int id)
        {
            var res = db.Topics.FirstOrDefault(m => m.ID == id);
            if (res != null)
            {
                db.Topics.Remove(res);
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }
    }
