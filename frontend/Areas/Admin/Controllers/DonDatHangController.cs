using Microsoft.AspNetCore.Mvc;
using frontend.Models;
using frontend.Areas.Admin.MyModels;
using frontend.MyModels;

namespace frontend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DonDatHangController : Controller
    {
        public IActionResult Index(int thang = 0)
        {
            ViewBag.thang = thang;
            return View();
        }

        public IActionResult timKiem(string maddh)
        {
            List<CDonDatHang> ketQua = new List<CDonDatHang>();

            if (string.IsNullOrEmpty(maddh))
            {
                return PartialView("IndexAjax", ketQua);
            }

            DonDatHang? ddh = XulyDonDatHang.getDondathang(Int32.Parse(maddh));

            if (ddh != null)
            {
                ketQua.Add(CDonDatHang.chuyenDoi(ddh));
            }
            return PartialView("IndexAjax", ketQua);
        }

        public IActionResult IndexAjax(string trangthai = "", int thang = 0, int trang = 1)
        {
            List<CDonDatHang> ds = new List<CDonDatHang>();
            if (string.IsNullOrEmpty(trangthai) || trangthai == "")
            {
                ds = XulyDonDatHang.getDSDondathang().Select(t => CDonDatHang.chuyenDoi(t))
                        .ToList();
            }
            else
            {
                ds = XulyDonDatHang.getDondathangByTT(trangthai)
                        .Select(t => CDonDatHang.chuyenDoi(t))
                        .ToList();
            }
            if (thang != 0)
            {
                ds = ds
                    .Where(t => t.Ngaydat != null && t.Ngaydat.Value.Month == thang)
                    .ToList();
            }

            int soSP = 5;
            int tongSP = ds.Count;
            int soTrang = (int)Math.Ceiling((double)tongSP / soSP);

            List<CDonDatHang> dsDon = ds.Skip((trang - 1) * soSP).Take(soSP).ToList();

            ViewBag.trangHienTai = trang;
            ViewBag.tongTrang = soTrang;
            return PartialView(dsDon);
        }

        public IActionResult HuyDDH(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail") ?? "";
            NhanVien? admin = XulyNhanVien.getNhanvienByEmail(email);
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var ddh = XulyDonDatHang.getDondathang(id);
            if (ddh == null)
            {
                return RedirectToAction("Index");
            }
            if (ddh.Trangthai == "Đang xử lý")
            {
                ddh.Trangthai = "Đã hủy";
                ddh.MaNv = admin?.MaNv;
                XulyDonDatHang.suaDonDatHang(ddh.MaDdh, DonDatHangDTO.chuyenDoi(ddh));
            }
            return RedirectToAction("Index");
        }

        public IActionResult chiTietDDH(int id)
        {
            string? email = HttpContext.Session.GetString("UserEmail");
            if (email == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            NhanVien? nv = XulyNhanVien.getNhanvienByEmail(email);
            DonDatHang? ddh = XulyDonDatHang.getDondathang(id);
            if (ddh == null)
            {
                return RedirectToAction("Index");
            }
            List<ChiTietDonDatHang> dsCTDDH = XulyChiTietDonDatHang.getDSChitietdondathangByMaddh(ddh.MaDdh);
            foreach (ChiTietDonDatHang ct in dsCTDDH)
            {
                ct.MaSpNavigation = XulySanPham.getSanpham(ct.MaSp);
                ddh.ChiTietDonDatHangs.Add(ct);
            }
            ddh.MaNvNavigation = XulyNhanVien.getNhanvienByManv(ddh.MaNv??"");
            ddh.MaNdNavigation = XulyNguoidung.getNguoidungByMand(ddh.MaNd);
            ViewBag.NV = nv;
            return View(ddh);
        }

        public IActionResult xacNhanDDH(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail") ?? "";
            NhanVien? admin = XulyNhanVien.getNhanvienByEmail(email);
            DonDatHang? ddh = XulyDonDatHang.getDondathang(id);
            if (ddh != null)
            {
                ddh.Trangthai = "Hoàn thành";
                ddh.TtThanhtoan = "Đã thanh toán";
                ddh.MaNv = admin?.MaNv;
                XulyDonDatHang.suaDonDatHang(ddh.MaDdh,DonDatHangDTO.chuyenDoi(ddh));
            }
            return RedirectToAction("Index");
        }
    }
}

