using CarShowroom;
using CarShowroom.Areas.Admin.Controllers;
using CarShowroom.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CarShowroom.Areas.Admin.Controllers
{
   
    public class BaiVietController : BaseController
    {
        CarShowroomDbContext db = new CarShowroomDbContext();

        // GET: Bdmin/Post
        public ActionResult Index()
        {
            var list = db.Posts.Where(m => m.status > 0).OrderByDescending(m=>m.ID).ToList();
            return View(list);
        }

        // GET: Bdmin/Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet BaiViet = db.Posts.Find(id);
            if (BaiViet == null)
            {
                return HttpNotFound();
            }
            return View(BaiViet);
        }

        // GET: Bdmin/Post/Create
        public ActionResult Create()
        {
            ViewBag.listTopic = db.Topics.Where(m => m.status == 1 ).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(BaiViet BaiViet)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file;
                var namecateDb = db.Topics.Where(m => m.ID == BaiViet.topid).First();
                string slug = Mystring.ToSlug(BaiViet.title.ToString());
                string namecate = Mystring.ToStringNospace(namecateDb.name);
                file = Request.Files["img"];
                string filename = file.FileName.ToString();
                string ExtensionFile = Mystring.GetFileExtension(filename);
                string namefilenew = namecate + "/" + slug + "." + ExtensionFile;
                var path = Path.Combine(Server.MapPath("~/img/Post/"), namefilenew);
                var folder = Server.MapPath("~/img/Post/" + namecate);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                file.SaveAs(path);
                BaiViet.img = namefilenew;
                BaiViet.slug = slug;
                db.Posts.Add(BaiViet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.listTopic = db.Topics.Where(m => m.status != 0).ToList();
            return View(BaiViet);
        }

        // GET: Bdmin/Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet BaiViet = db.Posts.Find(id);
            if (BaiViet == null)
            {
                return HttpNotFound();
            }
            ViewBag.listTopic = db.Topics.Where(m => m.status != 0).ToList();
            return View(BaiViet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit( BaiViet BaiViet)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file;
                string slug = Mystring.ToSlug(BaiViet.title.ToString());
                file = Request.Files["img"];
                string filename = file.FileName.ToString();
                if (filename.Equals("") == false)
                {
                    var namecateDb = db.Topics.Where(m => m.ID == BaiViet.topid).First();
                    string namecate = Mystring.ToStringNospace(namecateDb.name);
                    string ExtensionFile = Mystring.GetFileExtension(filename);
                    string namefilenew = namecate + "/" + slug + "." + ExtensionFile;
                    var path = Path.Combine(Server.MapPath("~/img/post"), namefilenew);
                    var folder = Server.MapPath("~/img/post/" + namecate);
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    file.SaveAs(path);
                    BaiViet.img = namefilenew;
                }
                BaiViet.slug = slug;
                db.Entry(BaiViet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.listTopic = db.Topics.Where(m => m.status != 0).ToList();
            return View(BaiViet);
        }

      
        public ActionResult delete(int id)
        {
            BaiViet BaiViet = db.Posts.Find(id);
            var res = db.Posts.FirstOrDefault(m => m.ID == id);
            if (res != null)
            {
                db.Posts.Remove(res);
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }
}
