using Microsoft.AspNetCore.Mvc;
using frontend.Models;
using frontend.Areas.Admin.MyModels;
using frontend.MyModels;

namespace frontend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThongKeController : Controller
    {
        public IActionResult Index(int year = 0)
        {
            if (year == 0) year = DateTime.Now.Year;
            List<CThongKes> ds = new List<CThongKes>();
            for (int i = 1; i <= 12; i++)
            {
                ds.Add(new CThongKes
                {
                    Thang = i,
                    SoLuongDon = 0,
                    TongTien = 0
                });
            }
            var donHang = XulyDonDatHang.getDSDondathang();

            foreach (var d in donHang)
            {
                if (d.Ngaydat != null && d.Trangthai == "Hoàn thành")
                {
                    int thang = d.Ngaydat.Value.Month;
                    if (d.Ngaydat.Value.Year != year) continue;
                    foreach (var item in ds)
                    {
                        if (item.Thang == thang)
                        {
                            item.SoLuongDon += 1;
                            item.TongTien += (d.Tongtien ?? 0);
                            break;
                        }
                    }
                }
            }
            ViewBag.Year = year;
            return View(ds);
        }
    }
}
