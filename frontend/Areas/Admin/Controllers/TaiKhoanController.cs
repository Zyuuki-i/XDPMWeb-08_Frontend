using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using frontend.Areas.Admin.MyModels;
using frontend.Models;
using frontend.MyModels;

namespace frontend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TaiKhoanController : Controller
    {
        public IActionResult Index()
        {
            List<NguoiDung> taiKhoans = new List<NguoiDung>();
            List<NguoiDung> dsND = XulyNguoidung.getDSNguoidung();
            foreach (NguoiDung tk in dsND)
            {
                if (tk.Trangthai == true)
                    taiKhoans.Add(tk);
            }
            List<CNguoiDung> ds = taiKhoans.Select(t => CNguoiDung.chuyendoi(t)).ToList();
            return View(ds);
        }

        public IActionResult loadTKVoHieu()
        {
            List<NguoiDung> dsTK = XulyNguoidung.getDSNguoidungByTT(false);
            List<CNguoiDung> ds = dsTK.Select(t => CNguoiDung.chuyendoi(t)).ToList();
            return PartialView(ds);
        }

        public IActionResult dongTaiKhoan(int id)
        {
            var tk = XulyNguoidung.getNguoidungByMand(id);
            if (tk == null)
            {
                return NotFound();
            }
            tk.Trangthai = false;
            XulyNguoidung.sua(tk.MaNd,tk);
            return RedirectToAction("Index");
        }

        public IActionResult moTaiKhoan(int id)
        {
            var tk = XulyNguoidung.getNguoidungByMand(id);
            if (tk == null)
            {
                return NotFound();
            }
            tk.Trangthai = true;
            XulyNguoidung.sua(tk.MaNd, tk);
            return RedirectToAction("Index");
        }

    }
}
