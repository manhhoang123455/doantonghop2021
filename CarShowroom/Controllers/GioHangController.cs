using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using API_NganLuong;
using CarShowroom.Models;
using CarShowroom.Models.Enum;
using CarShowroom.Session;
using MoMo;
using Newtonsoft.Json.Linq;
using ShopThoiTrang.MomoAPI;
using ShopThoiTrang.nganluonAPI;

namespace CarShowroom.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        private const string CartSession = "CartSession";
        private readonly CarShowroomDbContext context = new CarShowroomDbContext();
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult Index()
        {
            ViewBag.LoiSoLuong = TempData["LoiSoLuong"] as string;
            ViewBag.ThongBaoTrong = TempData["ThongBaoTrong"] as string;
            var cart = Session[CartSession] as GioHang;
            GioHang listItem = new GioHang();
            if (cart != null && cart.DoTrongGioHangs.Count != 0)
            {

                listItem = cart;
            }
            else
            {
                listItem.DoTrongGioHangs = new List<DoTrongGioHang>();
                listItem.NgayThue = DateTime.Now;
                listItem.NgayTra = DateTime.Now;
                ViewBag.GioHangTrong = "Giỏ hàng hiện tại đang trống, mời quý khách lựa chọn sản phẩm cần thuê!!";
            }

            return View(listItem);
        }
        [HttpPost]
        public ActionResult Tang(int id, int currentQtt = 1, int quantity = 1)
        {

            var xe = context.Xe.FirstOrDefault(m => m.MaXe == id);
            var gioHang = Session[CartSession] as GioHang;
            foreach (var item in gioHang.DoTrongGioHangs)
            {
                if (item.Xe.MaXe == xe.MaXe)
                {
                    if (currentQtt < xe.SoLuongXe)
                        item.Quantity += quantity;
                    else
                    {
                        TempData["LoiSoLuong"] = "<script>alert('Trong kho đang tạm hết hàng, vui lòng chọn sản phẩm khác');</script>";
                        return RedirectToAction("Index");
                    }
                    //TODO
                }
            }
            Session[CartSession] = gioHang;
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Giam(int id, int quantity, int currentQtt)
        {

            var xe = context.Xe.FirstOrDefault(m => m.MaXe == id);
            var gioHang = Session[CartSession] as GioHang;
            foreach (var item in gioHang.DoTrongGioHangs)
            {
                if (item.Xe.MaXe == xe.MaXe)
                {
                    if (currentQtt > 1)
                        item.Quantity -= quantity;
                    else
                    {
                        TempData["LoiSoLuong"] = "<script>alert('Không thể đặt thuê với số lượng là 0');</script>";
                    }
                    //TODO
                }
            }
            Session[CartSession] = gioHang;
            return RedirectToAction("Index");


        }
        [HttpPost]
        public ActionResult Xoa(int id)
        {
            var gioHang = Session[CartSession] as GioHang;
            gioHang.DoTrongGioHangs.RemoveAll(x => x.Xe.MaXe == id);
            Session[CartSession] = gioHang;
            return RedirectToAction("Index");
        }
        public ActionResult Them(int id, int quantity)
        {

            var cart = Session[CartSession] as GioHang;
            var xe = context.Xe.FirstOrDefault(x => x.MaXe == id);
            if (cart != null)
            {
                var listItem = cart.DoTrongGioHangs as List<DoTrongGioHang>;
                if (listItem.Exists(m => m.Xe.MaXe == id))
                {
                    foreach (var item in listItem)
                    {
                        if (item.Xe.MaXe == xe.MaXe)
                        {
                            if (item.Quantity < xe.SoLuongXe)
                                item.Quantity += quantity;
                            else
                            {
                                TempData["LoiSoLuong"] = "<script>alert('Trong kho đang tạm hết hàng, vui lòng chọn sản phẩm khác');</script>";
                                return RedirectToAction("Index");
                            }
                            //TODO
                        }
                    }
                }
                else
                {
                    DoTrongGioHang cItem = new DoTrongGioHang { Xe = xe, Quantity = quantity };
                    listItem.Add(cItem);
                }
                cart.DoTrongGioHangs = listItem;
                Session[CartSession] = cart;
            }
            else
            {
                cart = new GioHang();
                List<DoTrongGioHang> listItem = new List<DoTrongGioHang>();
                DoTrongGioHang cItem = new DoTrongGioHang();
                cItem.Xe = xe;
                cItem.Quantity = quantity;
                listItem.Add(cItem);

                cart.DoTrongGioHangs = listItem;

                Session[CartSession] = cart;

            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult ThanhToan(DateTime ngayThue, DateTime ngayTra)
        {
            string payment_method = Request["option_payment"];
            decimal money = 0;
            ViewBag.ThongTinUser = Session[UserSession.USER_SESSION] as KhachHang;
            if (ViewBag.ThongTinUser == null)
            {
                TempData["ThongBaoDangNhap"] = "<script>alert('Mời Đăng Nhập Để thanh toán');</script>";
                return RedirectToAction("Index", "Login");
            }
            var cart = Session[CartSession] as GioHang;
            if (cart == null)
            {
                TempData["ThongBaoTrong"] = "<script>alert('Bạn Chưa Chọn Sản Phẩm Nên Không Thể Thanh Toán');</script>";
                return RedirectToAction("Index");
            }
            cart.NgayTra = ngayTra;
            cart.NgayThue = ngayThue;
            foreach (var item in cart.DoTrongGioHangs)
            {
                money += item.Xe.GiaChoThue * item.Quantity;
            }
            try
            {
                DateTime ngaymuon = Convert.ToDateTime(cart.NgayThue);
                DateTime ngaytra = Convert.ToDateTime(cart.NgayTra);
                TimeSpan time = ngaytra - ngaymuon;
                int tongSoNgay = time.Days + 1;

                ViewBag.Money = money * tongSoNgay;
            }
            catch { return RedirectToAction("Index"); }
            ViewBag.GioHang = cart;
            return View();
        }
        [HttpPost]
        public ActionResult DatHang()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            int numIterations = 0;
            numIterations = rand.Next(1, 100000);
            DateTime time = DateTime.Now;
            var user = (TaiKhoan)Session[UserSession.USER_SESSION1];
            var khachhang = context.KhachHang.Find(user.MaTaiKhoan);
            string orderCode = ToStringNospace(numIterations + "" + time);
            string sumOrder = Request["totalMoney"];
            string payment_method = Request["option_payment"];
            // Neu Ship COde
            if (payment_method.Equals("COD"))
            {
                // cap nhat thong tin sau khi dat hang thanh cong
                saveOrder();
                var hoadon = context.HoaDon.OrderByDescending(m => m.MaHoaDon).FirstOrDefault();
                ViewBag.khachhang = khachhang;
                return View("oderComplete", hoadon);
            }
            #region
            ////Neu Thanh toan MOMO
            //else if (payment_method.Equals("MOMO"))
            //{
            //    //request params need to request to MoMo system
            //    string endpoint = momoInfo.endpoint;
            //    string partnerCode = momoInfo.partnerCode;
            //    string accessKey = momoInfo.accessKey;
            //    string serectkey = momoInfo.serectkey;
            //    string orderInfo = momoInfo.orderInfo;
            //    string returnUrl = momoInfo.returnUrl;
            //    string notifyurl = momoInfo.notifyurl;

            //    string amount = sumOrder;
            //    string orderid = Guid.NewGuid().ToString();
            //    string requestId = Guid.NewGuid().ToString();
            //    string extraData = "";

            //    //Before sign HMAC SHA256 signature
            //    string rawHash = "partnerCode=" +
            //        partnerCode + "&accessKey=" +
            //        accessKey + "&requestId=" +
            //        requestId + "&amount=" +
            //        amount + "&orderId=" +
            //        orderid + "&orderInfo=" +
            //        orderInfo + "&returnUrl=" +
            //        returnUrl + "&notifyUrl=" +
            //        notifyurl + "&extraData=" +
            //        extraData;

            //    log.Debug("rawHash = " + rawHash);

            //    MoMoSecurity crypto = new MoMoSecurity();
            //    //sign signature SHA256
            //    string signature = crypto.signSHA256(rawHash, serectkey);
            //    log.Debug("Signature = " + signature);

            //    //build body json request
            //    JObject message = new JObject
            //{
            //    { "partnerCode", partnerCode },
            //    { "accessKey", accessKey },
            //    { "requestId", requestId },
            //    { "amount", amount},
            //    { "orderId", orderid },
            //    { "orderInfo", orderInfo },
            //    { "returnUrl", returnUrl },
            //    { "notifyUrl", notifyurl },
            //    { "extraData", extraData },
            //    { "requestType", "captureMoMoWallet" },
            //    { "signature", signature }
            //};
            //    log.Debug("Json request to MoMo: " + message.ToString());
            //    string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());
            //    JObject jmessage = JObject.Parse(responseFromMomo);

            //    saveOrder(HinhThucThanhToan.ThanhToanMomo);
            //    string payUrl = jmessage.GetValue("payUrl").ToString();
            //    return Redirect(payUrl);
            //}
            #endregion
            //Neu Thanh toan Ngan Luong
            else if (payment_method.Equals("NL"))
            {
                string str_bankcode = Request["bankcode"];
                RequestInfo info = new RequestInfo();
                info.Merchant_id = nganluongInfo.Merchant_id;
                info.Merchant_password = nganluongInfo.Merchant_password;
                info.Receiver_email = nganluongInfo.Receiver_email;
                info.cur_code = "vnd";
                info.bank_code = str_bankcode;
                info.Order_code = orderCode;
                info.Total_amount = sumOrder;
                info.fee_shipping = "0";
                info.Discount_amount = "0";
                info.order_description = "Thanh toán ngân lượng cho đơn hàng";
                info.return_url = nganluongInfo.return_url;
                info.cancel_url = nganluongInfo.cancel_url;
                info.Buyer_fullname = khachhang.TenKhachHang;
                info.Buyer_email = "vanhung3339@gmail.com";
                info.Buyer_mobile = khachhang.SoDienThoai;
                APICheckoutV3 objNLChecout = new APICheckoutV3();
                ResponseInfo result = objNLChecout.GetUrlCheckout(info, payment_method);
                // neu khong gap loi gi
                if (result.Error_code == "00")
                {
                    saveOrder(HinhThucThanhToan.ThanhToanNganLuong);
                    // chuyen sang trang ngan luong
                    return Redirect(result.Checkout_url);
                }
                else
                {
                    ViewBag.errorPaymentOnline = result.Description;
                    return View("payment");
                }

            }
            //Neu Thanh Toán ATM online
            else if (payment_method.Equals("ATM_ONLINE"))
            {
                string str_bankcode = Request["bankcode"];
                RequestInfo info = new RequestInfo();
                info.Merchant_id = nganluongInfo.Merchant_id;
                info.Merchant_password = nganluongInfo.Merchant_password;
                info.Receiver_email = nganluongInfo.Receiver_email;
                info.cur_code = "vnd";
                info.bank_code = str_bankcode;
                info.Order_code = orderCode;
                info.Total_amount = sumOrder;
                info.fee_shipping = "0";
                info.Discount_amount = "0";
                info.order_description = "Thanh toán ngân lượng cho đơn hàng";
                info.return_url = nganluongInfo.return_url;
                info.cancel_url = nganluongInfo.cancel_url;
                info.Buyer_fullname = khachhang.TenKhachHang;
                info.Buyer_email = "vanhung3339@gmail.com";
                info.Buyer_mobile = khachhang.SoDienThoai;
                APICheckoutV3 objNLChecout = new APICheckoutV3();
                ResponseInfo result = objNLChecout.GetUrlCheckout(info, payment_method);
                // neu khong gap loi gi
                if (result.Error_code == "00")
                {
                    saveOrder();
                    return Redirect(result.Checkout_url);
                }
                else
                {
                    ViewBag.errorPaymentOnline = result.Description;
                    return View("payment");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        void saveOrder(HinhThucThanhToan HTTT = HinhThucThanhToan.ThanhToanTrucTiep)
        {
            decimal totalMoney = 0;
            KhachHang khachHang = Session[UserSession.USER_SESSION] as KhachHang;
            GioHang gioHang = Session[CartSession] as GioHang;
            List<ChiTietHoaDon> listChiTietHoaDons = new List<ChiTietHoaDon>();
            foreach (var item in gioHang.DoTrongGioHangs)
            {
                ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon()
                {
                    SoLuong = item.Quantity,
                    
                    ThanhTien = item.Xe.GiaChoThue * item.Quantity,
                    MaXe = item.Xe.MaXe
                };
                listChiTietHoaDons.Add(chiTietHoaDon);
                totalMoney += chiTietHoaDon.ThanhTien;
            }
            HoaDon hoaDon = new HoaDon()
            {
                MaKhachHang = khachHang.MaKhachHang,
                NgayNhan = gioHang.NgayThue.Value,
                TrangThaiThanhToan = 2,
                NgayTra = gioHang.NgayTra.Value,
                HinhThucThanhToan = HTTT,
                TongThanhTien = totalMoney,
                ChiTietHoaDon = listChiTietHoaDons
            };

            context.HoaDon.Add(hoaDon);
            context.SaveChanges();
            Session[CartSession] = null;
        }
        String ToStringNospace(String s)
        {
            String[][] symbols = {
                                 new String[] { "[áàảãạăắằẳẵặâấầẩẫậ]", "a" },
                                 new String[] { "[đ]", "d" },
                                 new String[] { "[éèẻẽẹêếềểễệ]", "e" },
                                 new String[] { "[íìỉĩị]", "i" },
                                 new String[] { "[óòỏõọôốồổỗộơớờởỡợ]", "o" },
                                 new String[] { "[úùủũụưứừửữự]", "u" },
                                 new String[] { "[ýỳỷỹỵ]", "y" },
                                 new String[] { "[\\s'\";,]", "" }
                             };
            s = s.ToLower();
            foreach (var ss in symbols)
            {
                s = Regex.Replace(s, ss[0], ss[1]);
            }
            return s;
        }
        //Khi huy thanh toán Ngan Luong
        public ActionResult cancel_order()
        {

            return View("cancel_order");
        }
        //Khi thanh toán Ngan Luong XOng
        public ActionResult confirm_orderPaymentOnline()
        {

            String Token = Request["token"];
            RequestCheckOrder info = new RequestCheckOrder();
            info.Merchant_id = nganluongInfo.Merchant_id;
            info.Merchant_password = nganluongInfo.Merchant_password;
            info.Token = Token;
            APICheckoutV3 objNLChecout = new APICheckoutV3();
            ResponseCheckOrder result = objNLChecout.GetTransactionDetail(info);
            if (result.errorCode == "00")
            {
                var hoadon = context.HoaDon.OrderByDescending(m => m.MaHoaDon).FirstOrDefault();
                hoadon.TrangThaiThanhToan = 1;
                context.Entry(hoadon).State = EntityState.Modified;
                context.SaveChanges();
                var user = (TaiKhoan)Session[UserSession.USER_SESSION1];
                var khachhang = context.KhachHang.Find(user.MaTaiKhoan);
                ViewBag.khachhang = khachhang;
                ViewBag.status = 1;
                return View("oderComplete", hoadon);
            }
            else
            {
                ViewBag.status = 2;
            }

            return View("confirm_orderPaymentOnline");
        }
    }

}
