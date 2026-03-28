using Microsoft.AspNetCore.Mvc;
using frontend.Areas.Admin.MyModels;
using frontend.Models;
using frontend.MyModels;

namespace frontend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GiaoHangController : Controller
    {
        public IActionResult Index(string manv = "", int trang = 1)
        {
            List<CDonDatHang> ds = XulyDonDatHang.getDondathangByTT("Đang xử lý").Select(t=>CDonDatHang.chuyenDoi(t)).ToList();
            ViewBag.NV = XulyNhanVien.getDSNhanvienByVT("Carrier");
            ViewBag.MaNV = manv;

            int soSP = 8;
            int tongSP = ds.Count;
            int soTrang = (int)Math.Ceiling((double)tongSP / soSP);

            List<CDonDatHang> dsDon = ds.Skip((trang - 1) * soSP).Take(soSP).ToList();
            ViewBag.trangHienTai = trang;
            ViewBag.tongTrang = soTrang;
            return View(dsDon);
        }

        public IActionResult Shipper(string id)
        {
            NhanVien? nv = XulyNhanVien.getNhanvienByManv(id);
            return PartialView(nv);
        }

        public IActionResult banGiaoDDH(IFormCollection fc)
        {
            string manv = fc["Manv"].ToString();
            bool flag = false;
            NhanVien? nv = XulyNhanVien.getNhanvienByManv(manv);
            if (nv == null)
            {
                TempData["MessageError_GiaoHang"] = "Có lỗi khi chọn nhân viên hoặc nhân viên không tồn tại!";
            }
            else
            {
                string email = HttpContext.Session.GetString("UserEmail") ?? "";
                NhanVien? admin = XulyNhanVien.getNhanvienByEmail(email);
                List<DonDatHang> dsDDH = XulyDonDatHang.getDSDondathang();
                foreach (DonDatHang ddh in dsDDH)
                {
                    if (fc.ContainsKey(ddh.MaDdh.ToString()))
                    {
                        ddh.Trangthai = "Đã xử lý";
                        ddh.MaNv = admin?.MaNv;
                        if (XulyDonDatHang.suaDonDatHang(ddh.MaDdh, DonDatHangDTO.chuyenDoi(ddh)))
                        {
                            GiaoHang gh = new GiaoHang();
                            gh.MaDdh = ddh.MaDdh;
                            gh.MaNv = nv.MaNv;
                            gh.Ngaybd = DateTime.Now;
                            gh.Ngaykt = DateTime.Now.AddDays(7);
                            gh.Tongthu = ddh.Phuongthuc == "COD" ? ddh.Tongtien : 0;
                            gh.Trangthai = "Chờ giao hàng";
                            if (XulyGiaoHang.them(gh) == false) flag = false; else flag = true;
                        }else flag = false;
                    }
                }
                if (flag)
                {
                    TempData["MessageSuccess_GiaoHang"] = "Bàn giao thành công tới nhân viên " + nv.Tennv;
                }
                else
                {
                    TempData["MessageError_GiaoHang"] = "Vui lòng chọn đơn cần bàn giao!";
                }
            }
            int tranght = 1;
            Int32.TryParse(fc["trangHT"], out tranght);
            return RedirectToAction("Index", new { manv = manv, trang = tranght });
        }

        public IActionResult trangThaiGH(int trang = 1)
        {
            List<GiaoHang> ds = XulyGiaoHang.getDSGiaohang();

            foreach (GiaoHang gh in ds)
            {
                gh.MaNvNavigation = XulyNhanVien.getNhanvienByManv(gh.MaNv);
            }

            int soSP = 6;
            int tongSP = ds.Count;
            int soTrang = (int)Math.Ceiling((double)tongSP / soSP);

            ds = ds.Skip((trang - 1) * soSP).Take(soSP).ToList();

            ViewBag.trangHienTai = trang;
            ViewBag.tongTrang = soTrang;
            return View(ds);
        }
    }
}
