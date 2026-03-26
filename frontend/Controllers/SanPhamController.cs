using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using frontend.Models;
using frontend.MyModels;
using NuGet.Protocol.Plugins;

namespace frontend.Controllers
{
    public class SanPhamController : Controller
    {

        public IActionResult Index(string keyword = "", string maloai="", string mansx="", int trang = 1)
        {
            List<SanPham> dsSP = XulySanPham.locSanPham(keyword, maloai, mansx);
            int soSP = 6;

            int tongSP = dsSP.Count;
            int soTrang = (int)Math.Ceiling((double)tongSP / soSP);

            List<SanPham> sanPhams = dsSP.Skip((trang - 1) * soSP).Take(soSP).ToList();
            ViewBag.trangHienTai = trang;
            ViewBag.tongTrang = soTrang;
            ViewBag.DsHinh = XulyHinh.getDS1Hinh();
            ViewBag.DsLoai = XulyLoai.getDSLoai();
            ViewBag.DsNSX = XulyNhaSanXuat.getDSNhasanxuat();
            TempData["maloai"] = maloai;
            TempData["mansx"] = mansx;
            TempData["keyword"] = keyword;
            return View(sanPhams);
        }

        public IActionResult locLoai(string maloai)
        {
            return RedirectToAction("Index", new { maloai = maloai });
        }

        public IActionResult locNSX(string mansx)
        {
            return RedirectToAction("Index", new { mansx = mansx });
        }

        public IActionResult timKiem(string keyword)
        {
            return RedirectToAction("Index", new { keyword = keyword });
        }

        public IActionResult chiTiet(string id)
        {
            SanPham? sp = XulySanPham.getSanpham(id);
            if (sp == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.HinhChinh = XulyHinh.getDSHinhByMasp(id); ;
            ViewBag.DsLoai = XulyLoai.getDSLoai();
            ViewBag.DsNSX = XulyNhaSanXuat.getDSNhasanxuat();

            List<DanhGia> dg = XulyDanhgia.getDSDanhgiaByMasp(id);
            ViewBag.DanhGia = dg;
            List<NguoiDung> dsKHDG = new List<NguoiDung>();
            foreach(DanhGia d in dg)
            {
                NguoiDung? ng = XulyNguoidung.getNguoidungByMand(d.MaNd);
                if(ng != null) dsKHDG.Add(ng);
            }
            ViewBag.KhachHangDanhGia = dsKHDG;

            List<SanPham> dsSP = XulySanPham.locSanPham(null,sp.MaLoai,sp.MaNsx)
                                        .Take(4)
                                        .ToList();

            int vt = dsSP.FindIndex(x => x.MaSp == sp.MaSp);
            if (vt >= 0 && vt < dsSP.Count)
                dsSP.RemoveAt(vt);
            dsSP.Insert(0, sp);

            List<Hinh> dsHinh = new List<Hinh>();
            foreach (SanPham sanPham in dsSP)
            {
                Hinh? hinh = XulyHinh.getDSHinhByMasp(sanPham.MaSp)?.FirstOrDefault();
                if (hinh != null)
                {
                    dsHinh.Add(hinh);
                }
            }
            ViewBag.DsHinh = dsHinh
                                    .GroupBy(t => t.MaSp)
                                    .Select(g => g.First())
                                    .ToList();
            return View(dsSP);
        }




    }
}
