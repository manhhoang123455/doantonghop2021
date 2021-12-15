using CarShowroom.Models.Enum;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace CarShowroom.Models
{
    internal class KhoiTaoDuLieu : CreateDatabaseIfNotExists<CarShowroomDbContext>
    {
        protected override void Seed(CarShowroomDbContext context)
        {
            TaiKhoan admin2 = new TaiKhoan()
            {
                TenTaiKhoan = "Admin2",
                MatKhau = "123456",
                QuyenHan = QuyenHan.Admin,
                KhachHang = new KhachHang()
                {
                    TenKhachHang = "Admin2",
                    NamSinh = new DateTime(1997, 01, 01),
                    DiaChi = "Hà Nội",
                    CMND = "12356731",
                    NganHang = "TpBank",
                    SoTaiKhoan = "12331311",SoDienThoai = "1234267"
                }
            };
            context.TaiKhoan.Add(admin2);
            List<HangXe> listHangXe = new List<HangXe>()
            {
                new HangXe()
                {
                    MaHangXe = 1,
                    TenHangXe = "Lamborghini",
                    Quocgia = "Italy",
                    Xe = new List<Xe>()
                    {
                        new Xe()
                        {
                            TenXe = "Aventador S",
                            BienSo = new List<BienSo>()
                            {
                                new BienSo(){ BienSoXe = "30-F1 99999" },
                                new BienSo(){ BienSoXe = "30-F1 12345" },
                                new BienSo(){ BienSoXe = "30-F1 54321" },
                                new BienSo(){ BienSoXe = "30-F1 98765" },
                                new BienSo(){ BienSoXe = "30-F1 56789" },
                                new BienSo(){ BienSoXe = "30-F1 78542" },
                                new BienSo(){ BienSoXe = "30-F1 32141" },
                                new BienSo(){ BienSoXe = "30-F1 56784" },
                                new BienSo(){ BienSoXe = "30-F1 21354" },
                                new BienSo(){ BienSoXe = "30-F1 11234" }

                            },
                            PhienBan = "2019",
                            GiaNhap = 10000000000,
                            GiaChoThue = 1000000,
                            SoLuongXe = 10,
                            SoCho = 2,
                            HinhAnh = @"\img\AventadorS.jpg",
                            SoLuongXeDaThue = 0
                        },
                         new Xe()
                        {
                            TenXe = "Huracan LP610-4",
                            BienSo = new List<BienSo>()
                            {
                                new BienSo(){BienSoXe = "30-T1 12345"},
                                new BienSo(){BienSoXe = "30-T1 54632"},
                                new BienSo(){BienSoXe = "30-T1 11111"},
                                new BienSo(){BienSoXe = "30-T1 22222"},
                                new BienSo(){BienSoXe = "30-T1 33333"}
                            },
                            PhienBan = "2019",
                            GiaNhap = 10000000000,
                            GiaChoThue = 1000000,
                            SoLuongXe = 5,
                            SoCho = 2,
                            HinhAnh = @"\img\Huracan LP610-4.jpg",
                            SoLuongXeDaThue = 0
                        },
                         new Xe()
                        {
                            TenXe = "Aventador LP750-SV",
                            BienSo = new List<BienSo>()
                            {
                                new BienSo(){BienSoXe = "30-H1 99321"},
                                new BienSo(){BienSoXe = "30-H1 85463"}
                            },
                            PhienBan = "2017",
                            GiaNhap = 50000000000,
                            GiaChoThue = 5000000,
                            SoLuongXe = 2,
                            SoCho = 2,
                            HinhAnh = @"\img\Aventador LP750-SV.jpg",
                            SoLuongXeDaThue = 0
                        },
                    }

                },
                 new HangXe()
                {
                    MaHangXe = 2,
                    TenHangXe = "Toyota",
                    Quocgia = "Japan",
                    Xe = new List<Xe>()
                    {
                        new Xe()
                        {
                            TenXe = "Lexus ES 250",
                             BienSo = new List<BienSo>()
                            {
                                new BienSo(){ BienSoXe = "29-F1 99999" },
                                new BienSo(){ BienSoXe = "29-F1 12345" },
                                new BienSo(){ BienSoXe = "29-F1 54321" },
                                new BienSo(){ BienSoXe = "29-F1 98765" },
                                new BienSo(){ BienSoXe = "29-F1 56789" },
                                new BienSo(){ BienSoXe = "29-F1 78542" },
                                new BienSo(){ BienSoXe = "29-F1 32141" },
                                new BienSo(){ BienSoXe = "29-F1 56784" },
                                new BienSo(){ BienSoXe = "29-F1 21354" },
                                new BienSo(){ BienSoXe = "29-F1 11234" }

                            },
                            PhienBan = "2019",
                            GiaNhap = 16500000000,
                            GiaChoThue = 1600000,
                            SoLuongXe = 10,
                            SoCho = 4,
                            HinhAnh = @"\img\Lexus ES 250.jpg",
                            SoLuongXeDaThue = 0
                        },
                         new Xe()
                        {
                            TenXe = "CAMRY 2.0G",
                            BienSo = new List<BienSo>()
                            {
                                new BienSo(){BienSoXe = "29-T1 12345"},
                                new BienSo(){BienSoXe = "29-T1 54632"},
                                new BienSo(){BienSoXe = "29-T1 11111"},
                                new BienSo(){BienSoXe = "29-T1 22222"},
                                new BienSo(){BienSoXe = "29-T1 33333"}
                            },
                            PhienBan = "2019",
                            GiaNhap = 1029000000,
                            GiaChoThue = 1000000,
                            SoLuongXe = 5,
                            SoCho = 4,
                            HinhAnh = @"\img\CAMRY 20G.png",
                            SoLuongXeDaThue = 0
                        },
                         new Xe()
                        {
                            TenXe = "LAND CRUISER",
                               BienSo = new List<BienSo>()
                            {
                                new BienSo(){BienSoXe = "29-H1 99321"},
                                new BienSo(){BienSoXe = "29-H1 85463"}
                            },
                            PhienBan = "2017",
                            GiaNhap = 4067900000,
                            GiaChoThue = 3000000,
                            SoLuongXe = 5,
                            SoCho = 7,
                            HinhAnh = @"\img\LAND CRUISER.jpg",
                            SoLuongXeDaThue = 0
                        },
                    }

                },
                  new HangXe()
                {
                    MaHangXe = 2,
                    TenHangXe = "Vin Fast",
                    Quocgia = "VietNam",
                    Xe = new List<Xe>()
                    {
                        new Xe()
                        {
                            TenXe = "FADIL",
                             BienSo = new List<BienSo>()
                            {
                                new BienSo(){ BienSoXe = "99-F1 99999" },
                                new BienSo(){ BienSoXe = "99-F1 12345" },
                                new BienSo(){ BienSoXe = "99-F1 54321" },
                                new BienSo(){ BienSoXe = "99-F1 98765" },
                                new BienSo(){ BienSoXe = "99-F1 56789" },
                                new BienSo(){ BienSoXe = "99-F1 78542" },
                                new BienSo(){ BienSoXe = "99-F1 32141" },
                                new BienSo(){ BienSoXe = "99-F1 56784" },
                                new BienSo(){ BienSoXe = "99-F1 21354" },
                                new BienSo(){ BienSoXe = "99-F1 11234" }

                            },
                            PhienBan = "2019",
                            GiaNhap = 10000000000,
                            GiaChoThue = 1000000,
                            SoLuongXe = 10,
                            SoCho = 7,
                            HinhAnh = @"\img\FADIL.jpg",
                            SoLuongXeDaThue = 0
                        },
                         new Xe()
                        {
                            TenXe = "LUX A2.0",
                              BienSo = new List<BienSo>()
                            {
                                new BienSo(){BienSoXe = "99-T1 12345"},
                                new BienSo(){BienSoXe = "99-T1 54632"},
                                new BienSo(){BienSoXe = "99-T1 11111"},
                                new BienSo(){BienSoXe = "99-T1 22222"},
                                new BienSo(){BienSoXe = "99-T1 33333"}
                            },
                            PhienBan = "2019",
                            GiaNhap = 8000000000,
                            GiaChoThue = 800000,
                            SoLuongXe = 5,
                            SoCho = 4,
                            HinhAnh = @"\img\luxa2.0.jpg",
                            SoLuongXeDaThue = 0
                        },
                         new Xe()
                        {
                            TenXe = "LUX SA2.0",
                               BienSo = new List<BienSo>()
                            {
                                new BienSo(){BienSoXe = "99-H1 99321"},
                                new BienSo(){BienSoXe = "99-H1 85463"}
                            },
                            PhienBan = "2017",
                            GiaNhap = 1500000000,
                            GiaChoThue = 1000000,
                            SoLuongXe = 5,
                            SoCho = 4,
                            HinhAnh = @"\img\LUX SA2.0.jpg",
                            SoLuongXeDaThue = 0
                        },
                    }

                }
            };
            context.HangXe.AddRange(listHangXe);
            context.SaveChanges();
        }
    }
}