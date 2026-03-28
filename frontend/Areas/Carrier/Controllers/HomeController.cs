using Microsoft.AspNetCore.Mvc;
using frontend.Areas.Carrier.MyModels;
using frontend.Models;
using frontend.MyModels;

namespace frontend.Areas.Carier.Controllers
{
    [Area("Carrier")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string? email = HttpContext.Session.GetString("UserEmail");
            if (email == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            NhanVien? nv = XulyNhanVien.getNhanvienByEmail(email);
            List<GiaoHang> ds = new List<GiaoHang>();
            if (nv != null)
            {
                List<GiaoHang> dsGH = XulyGiaoHang.getDSGiaohang();
                foreach (GiaoHang gh in dsGH)
                {
                    if (gh.MaNv == nv.MaNv && gh.Trangthai != "Đã giao hàng" && gh.Trangthai != "Giao thất bại")
                    {
                        gh.MaNvNavigation = nv;
                        gh.MaDdhNavigation = XulyDonDatHang.getDondathang(gh.MaDdh);
                        if (gh.MaDdhNavigation != null)
                        {
                            gh.MaDdhNavigation.MaNdNavigation = XulyNguoidung.getNguoidungByMand(gh.MaDdhNavigation.MaNd);
                        }
                        ds.Add(gh);
                    }
                }
            }
            ViewBag.donChoGiao = ds.Where(t => t.Trangthai == "Chờ giao hàng").ToList();
            ViewBag.donDangGiao = ds.Where(t => t.Trangthai == "Đang giao hàng").ToList();
            return View();
        }

        public IActionResult donHoanThanh()
        {
            string? email = HttpContext.Session.GetString("UserEmail");
            if (email == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            NhanVien? nv = XulyNhanVien.getNhanvienByEmail(email);
            List<GiaoHang> ds = new List<GiaoHang>();
            if (nv != null)
            {
                List<GiaoHang> dsGH = XulyGiaoHang.getDSGiaohang();
                foreach (GiaoHang gh in dsGH)
                {
                    if (gh.MaNv == nv.MaNv && gh.Trangthai == "Đã giao hàng")
                    {
                        gh.MaNvNavigation = nv;
                        ds.Add(gh);
                    }
                }
            }
            return View(ds);
        }

        public IActionResult donThatBai()
        {
            string? email = HttpContext.Session.GetString("UserEmail");
            if (email == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            NhanVien? nv = XulyNhanVien.getNhanvienByEmail(email);
            List<GiaoHang> ds = new List<GiaoHang>();
            if (nv != null)
            {
                List<GiaoHang> dsGH = XulyGiaoHang.getDSGiaohang();
                foreach (GiaoHang gh in dsGH)
                {
                    if (gh.MaNv == nv.MaNv && gh.Trangthai == "Giao thất bại")
                    {
                        gh.MaNvNavigation = nv;
                        ds.Add(gh);
                    }
                }
            }
            return View(ds);
        }

        public IActionResult chiTietDH(int id)
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
            ViewBag.Shipper = nv;
            return View(ddh);
        }

        public IActionResult xacNhan(int id)
        {
            List<GiaoHang> dsGH = XulyGiaoHang.getDSGiaohang();
            GiaoHang? gh = dsGH.FirstOrDefault(g => g.MaGh == id && g.Trangthai == "Chờ giao hàng");
            if (gh != null)
            {
                gh.Trangthai = "Đang giao hàng";
                XulyGiaoHang.sua(gh.MaGh, gh);
            }
            return RedirectToAction("Index");
        }

        public IActionResult giaoThanhCong(int id)
        {
            GiaoHang? gh = XulyGiaoHang.getGiaohang(id);
            if (gh != null)
            {
                DonDatHang? ddh = XulyDonDatHang.getDondathang(gh.MaDdh);
                if (ddh != null)
                {
                    gh.Trangthai = "Đã giao hàng";
                    ddh.Trangthai = "Chờ xác nhận";
                    XulyGiaoHang.sua(gh.MaGh,gh);
                    XulyDonDatHang.suaDonDatHang(ddh.MaDdh, DonDatHangDTO.chuyenDoi(ddh));
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult giaoThatBai(int id)
        {
            GiaoHang? gh = XulyGiaoHang.getGiaohang(id);
            if (gh != null)
            {
                DonDatHang? ddh = XulyDonDatHang.getDondathang(gh.MaDdh);
                if (ddh != null)
                {
                    gh.Trangthai = "Giao thất bại";
                    ddh.Trangthai = "Đang xử lý";
                    XulyGiaoHang.sua(gh.MaGh, gh);
                    XulyDonDatHang.suaDonDatHang(ddh.MaDdh, DonDatHangDTO.chuyenDoi(ddh));
                }
            }
            return RedirectToAction("Index");
        }

    }
}
