using Microsoft.AspNetCore.Mvc;
using frontend.Models;


namespace frontend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoaiController : Controller
    {
        public IActionResult Index()
        {
            return View(XulyLoai.getDSLoai());
        }

        public IActionResult formXoaLoai(string id)
        {
            LoaiSanPham? l = XulyLoai.getLoai(id);
            if (l == null)
            {
                return RedirectToAction("Index");
            }
            return View(l);
        }

        public IActionResult xoaLoai(string id)
        {
            LoaiSanPham? l = XulyLoai.getLoai(id);
            if (l != null)
            {
                XulyLoai.xoa(l.MaLoai);
            }
            return RedirectToAction("Index");
        }

        public IActionResult formThemLoai()
        {
            return View();
        }

        public IActionResult themLoai(LoaiSanPham l)
        {
            XulyLoai.them(l);
            return RedirectToAction("Index");
        }

        public IActionResult formSuaLoai(string id)
        {
            LoaiSanPham? l = XulyLoai.getLoai(id);
            return View(l);
        }

        public IActionResult suaLoai(LoaiSanPham l)
        {
            LoaiSanPham? x = XulyLoai.getLoai(l.MaLoai);
            if (x != null)
            {
                XulyLoai.sua(l.MaLoai, l);
            }
            return RedirectToAction("Index");
        }


    }
}
