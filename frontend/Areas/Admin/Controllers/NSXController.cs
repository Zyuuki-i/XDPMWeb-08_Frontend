using Microsoft.AspNetCore.Mvc;
using frontend.Areas.Admin.MyModels;
using frontend.Models;

namespace frontend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NSXController : Controller
    {
        public IActionResult Index()
        {
            return View(XulyNhaSanXuat.getDSNhasanxuat());
        }

        public IActionResult formXoaNSX(string id)
        {
            NhaSanXuat? nsx = XulyNhaSanXuat.getNhasanxuat(id);
            if (nsx == null)
            {
                return RedirectToAction("Index");
            }
            return View(nsx);
        }

        public IActionResult xoaNSX(string id)
        {
            NhaSanXuat? nsx = XulyNhaSanXuat.getNhasanxuat(id);
            if (nsx != null)
            {
                XulyNhaSanXuat.xoa(nsx.MaNsx);
            }
            return RedirectToAction("Index");
        }

        public IActionResult formThemNSX()
        {
            return View();
        }

        public IActionResult themNSX(NhaSanXuat nsx)
        {
            XulyNhaSanXuat.them(nsx);
            return RedirectToAction("Index");
        }

        public IActionResult formSuaNSX(string id)
        {
            NhaSanXuat? nsx = XulyNhaSanXuat.getNhasanxuat(id);
            return View(nsx);
        }

        public IActionResult suaNSX(NhaSanXuat nsx)
        {
            XulyNhaSanXuat.sua(nsx.MaNsx, nsx);
            return RedirectToAction("Index");
        }
    }
}
