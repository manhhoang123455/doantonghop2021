using CarShowroom.Models;
using CarShowroom.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarShowroom.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        private CarShowroomDbContext Dbcontext = new CarShowroomDbContext();
        public ActionResult login()
        {          
            return View("login");
        }
        [HttpPost]
        public ActionResult login(string tenTaiKhoan, string matKhau)
        {
            if (ModelState.IsValid)
            {
                var result = Dbcontext.TaiKhoan.Where(m => m.TenTaiKhoan == tenTaiKhoan && m.MatKhau == matKhau)
                    .Where(m => m.QuyenHan == 0)
                    .FirstOrDefault();
                if (result != null)
                {
                    Session.Add(UserSession.USER_ADMIN, result);
                    return RedirectToAction("Index", "Xe");
                }
            }
            TempData["ThongBaoLoi"] = "Tài khoản hoặc mật khẩu không đúng. Mời thử lại!";
            return View("login");
        }
        public ActionResult logout()
        {
            Session[UserSession.USER_ADMIN] = "";
            return RedirectToAction("login", "auth");
        }

    }
}