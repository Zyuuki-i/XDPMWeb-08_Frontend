using Microsoft.AspNetCore.Mvc;
using frontend.Models;
using frontend.Areas.Admin.MyModels;
using frontend.MyModels;

namespace frontend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GiamGiaController : Controller
    {
        public IActionResult Index()
        {
            return View(XulyGiamGia.geDSGiamGia());
        }

        public IActionResult formThemMa()
        {
            return View();
        }

        public IActionResult themMa(GiamGia gg)
        {
            if (gg.Ngaybd > gg.Ngaykt)
            {
                TempData["MessageError_TGiamGia"] = "Ngày bắt đầu phải trước ngày kết thúc!";
                return RedirectToAction("formThemMa");
            }
            if (gg.Ngaybd < DateTime.Now)
            {
                TempData["MessageError_TGiamGia"] = "Ngày bắt đầu phải sau ngày hiện tại!";
                return RedirectToAction("formThemMa");
            }
            if (gg.Phantramgiam < 0 || gg.Phantramgiam > 100)
            {
                TempData["MessageError_TGiamGia"] = "Phần trăm giảm phải từ 0 đến 100!";
                return RedirectToAction("formThemMa");
            }
            if(XulyGiamGia.them(gg) == false)
            {
                TempData["MessageError_TGiamGia"] = "Lỗi khi thêm mã giảm giá!";
                return RedirectToAction("formThemMa");
            }
            return RedirectToAction("Index");
        }

        public IActionResult formApDung(string id)
        {
            GiamGia? gg =XulyGiamGia.getGiamGia(id);
            List<NguoiDung> nd = XulyNguoidung.getDSNguoidung();
            ViewBag.DSNguoiDung = nd;
            return View(gg);
        }

        public IActionResult apDungMa(IFormCollection fc)
        {
            string magg = fc["MaGg"];
            List<NguoiDung> nd = XulyNguoidung.getDSNguoidung();
            foreach (NguoiDung item in nd)
            {
                List<ChiTietGiamGia> ctggs = XulyChiTietGiamGia.getChiTietGiamGiaByNguoiDung(item.MaNd);
                ChiTietGiamGia? ctgg = ctggs.FirstOrDefault(x => x.MaGg == magg);
                if (ctgg != null)
                    ctgg.Soluong += 1;
                else
                {
                    ctgg = new ChiTietGiamGia
                    {
                        MaGg = magg,
                        MaNd = item.MaNd,
                        Soluong = 1
                    };
                    XulyChiTietGiamGia.themChiTietGiamGia(ChiTietGiamGiaDTO.chuyenDoi(ctgg));
                }
            }
            return RedirectToAction("Index");
        }
    }
}
