using System.Diagnostics;
using frontend.Models;
using frontend.MyModels;
using Microsoft.AspNetCore.Mvc;

namespace frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.DsHinh = XulyHinh.getDSHinh();
            List<SanPham> dsSP = XulySanPham.getDSSanpham().Take(8).ToList();
            return View(dsSP);
        }

        public IActionResult GioiThieu()
        {
            return View();
        }

        public IActionResult DanhGia()
        {
            List<DanhGia> dg = XulyDanhgia.getDSDanhgia();
            List<NguoiDung> nd = XulyNguoidung.getDSNguoidung();
            ViewBag.DSKH = nd;
            return View(dg);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
