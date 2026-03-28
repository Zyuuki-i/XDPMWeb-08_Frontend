using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using frontend.Areas.Admin.MyModels;
using frontend.Models;
using frontend.MyModels;

namespace frontend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhanVienController : Controller
    {
        public IActionResult Index()
        {
            List<NhanVien> dsNV = XulyNhanVien.getDSNhanvienByTT(true);
            return View(dsNV);
        }

        public IActionResult loadNVVoHieu()
        {
            List<NhanVien> dsNV = XulyNhanVien.getDSNhanvienByTT(false);
            return PartialView(dsNV);
        }

        public IActionResult formThem()
        {
            ViewBag.dsVT = new SelectList(XulyVaiTro.getDSVaiTro(), "MaVt", "Tenvt");
            return View();
        }

        public IActionResult them(NhanVien x)
        {
            ViewBag.dsVT = new SelectList(XulyVaiTro.getDSVaiTro(), "MaVt", "Tenvt");

            if (string.IsNullOrWhiteSpace(x.Cccd) || x.Cccd.Length != 12)
            {
                ModelState.AddModelError("Cccd", "CCCD phải đúng 12 số.");
                return View("formThem", x);
            }
            var checkCCCD = XulyNhanVien.getNhanvienByCCCD(x.Cccd);
            if (checkCCCD != null)
            {
                ModelState.AddModelError("Cccd", "CCCD đã tồn tại trong hệ thống.");
                return View("formThem", x);
            }

            x.Trangthai = true;
            x.Tenvt = "None";
            x.Hinh = "default.png";

            if(XulyNhanVien.them(x) == false)
            {
                ModelState.AddModelError("", "Lỗi khi lưu dữ liệu.");
                return View("formThem", x);
            }
            return RedirectToAction("Index");
        }

        public IActionResult dongTaiKhoan(string id)
        {
            var nv = XulyNhanVien.getNhanvienByManv(id);
            if (nv == null)
            {
                return NotFound();
            }
            nv.Trangthai = false;
            XulyNhanVien.sua(id, nv);
            return RedirectToAction("Index");
        }

        public IActionResult moTaiKhoan(string id)
        {
            var nv = XulyNhanVien.getNhanvienByManv(id);
            if (nv == null)
            {
                return NotFound();
            }
            nv.Trangthai = true;
            XulyNhanVien.sua(id, nv);
            return RedirectToAction("Index");
        }
    }
}
