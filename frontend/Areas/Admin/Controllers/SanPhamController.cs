using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using frontend.Areas.Admin.MyModels;
using frontend.Models;
using frontend.MyModels;

namespace frontend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanPhamController : Controller
    {
        public IActionResult Index(string MaLoai = "", string MaNsx = "", string MaSp = "", int trang = 1)
        {
            List<CSanPham> ds = XulySanPham.getDSSanpham().Select(t => CSanPham.chuyenDoi(t)).ToList();
            ViewBag.flag = false;
            if (!string.IsNullOrEmpty(MaLoai) && !string.IsNullOrEmpty(MaNsx))
            {
                if (MaLoai != "all" && MaNsx != "all")
                    ds = ds.Where(sp => sp.MaLoai == MaLoai && sp.MaNsx == MaNsx).ToList();
                else if (MaLoai != "" && MaNsx == "all")
                    ds = ds.Where(sp => sp.MaLoai == MaLoai).ToList();
                else if (MaLoai == "all" && MaNsx != "")
                    ds = ds.Where(sp => sp.MaNsx == MaNsx).ToList();
            }
            else if (!string.IsNullOrEmpty(MaSp))
            {
                CSanPham? sp = CSanPham.chuyenDoi(XulySanPham.getSanpham(MaSp));
                if (sp != null && sp.MaSp != null)
                {
                    int vt = ds.FindIndex(item => item.MaSp == sp.MaSp);
                    if (vt > 1 && vt < ds.Count - 1)
                    {
                        ds.RemoveAt(vt);
                        ds.Insert(0, sp);
                        ViewBag.FindMaSp = sp.MaSp;
                    }
                    else
                    {
                        ds.Clear();
                    }
                }
                else
                {
                    ds.Clear();
                    List<SanPham> dsSP = XulySanPham.locSanPham(MaSp, null, null);
                    foreach (SanPham s in dsSP)
                    {
                        CSanPham csp = CSanPham.chuyenDoi(s);
                        ds.Add(csp);
                    }
                }
            }
            ViewBag.DSNsx = new SelectList(XulyNhaSanXuat.getDSNhasanxuat(), "MaNsx", "Tennsx");
            ViewBag.DSLoai = new SelectList(XulyLoai.getDSLoai(), "MaLoai", "Tenloai");

            int soSP = 4;
            int tongSP = ds.Count;
            int soTrang = (int)Math.Ceiling((double)tongSP / soSP);

            List<CSanPham> sanPhams = ds.Skip((trang - 1) * soSP).Take(soSP).ToList();

            ViewBag.trangHienTai = trang;
            ViewBag.tongTrang = soTrang;

            var maSpList = sanPhams.Select(sp => sp.MaSp).ToList();
            List<Hinh> dsh = XulyHinh.getDS1Hinh();
            List<Hinh> dshinh = dsh
                                    .Where(g => maSpList.Contains(g.MaSp))
                                    .GroupBy(t => t.MaSp)
                                    .Select(g => g.First())
                                    .ToList();
            ViewBag.DsHinh = dshinh;
            TempData["maloai"] = MaLoai;
            TempData["mansx"] = MaNsx;
            TempData["masp"] = MaSp;
            return View(sanPhams);
        }

        public IActionResult timKiem(string MaSp)
        {
            return RedirectToAction("Index", new { MaLoai = (string)null, MaNsx = (string)null, MaSp = MaSp });
        }

        public IActionResult locSanPham(string MaLoai, string MaNsx)
        {
            if (MaLoai == "all" && MaNsx == "all")
                return RedirectToAction("Index", new { MaLoai = (string)null, MaNsx = (string)null, MaSp = (string)null });
            return RedirectToAction("Index", new { MaLoai = MaLoai, MaNsx = MaNsx, MaSp = (string)null });
        }

        public IActionResult formThemSP()
        {
            ViewBag.DSNsx = new SelectList(XulyNhaSanXuat.getDSNhasanxuat(), "MaNsx", "Tennsx");
            ViewBag.DSLoai = new SelectList(XulyLoai.getDSLoai(), "MaLoai", "Tenloai");
            return View();
        }

        public IActionResult themSanPham(CSanPham x, List<IFormFile> filehinh)
        {
            ViewBag.DSNsx = new SelectList(XulyNhaSanXuat.getDSNhasanxuat(), "MaNsx", "Tennsx");
            ViewBag.DSLoai = new SelectList(XulyLoai.getDSLoai(), "MaLoai", "Tenloai");
            if (ModelState.IsValid)
            {
                if (XulySanPham.getSanpham(x.MaSp)?.MaSp != null)
                {
                    TempData["MessageError_ThemSP"] = "Sản phẩm đã tồn tại!!!";
                    return RedirectToAction("formThemSP");
                }
                try
                {
                    SanPham sp = CSanPham.chuyenDoi(x);
                    if (XulySanPham.themSanPham(sp) == false)
                    {
                        TempData["MessageError_ThemSP"] = "Có lỗi khi thêm sản phẩm!!!";
                        return RedirectToAction("formThemSP");
                    }

                    CapNhat cn = new CapNhat();
                    string email = HttpContext.Session.GetString("UserEmail") ?? "";
                    List<NhanVien> dsNV = XulyNhanVien.getDSNhanvien();
                    foreach (NhanVien n in dsNV)
                    {
                        if (n.Email.Trim() == email.Trim())
                        {
                            cn.MaNv = n.MaNv;
                            break;
                        }
                    }
                    if (cn.MaNv == null)
                    {
                        TempData["MessageError_ThemSP"] = "Có lỗi khi thêm sản phẩm (không tìm thấy nhân viên)!!!";
                        return RedirectToAction("formThemSP");
                    }
                    cn.MaSp = sp.MaSp;
                    cn.Ngaycapnhat = DateTime.Now;  
                    if (XulyCapNhat.them(cn) == false)
                    {
                        TempData["MessageError_ThemSP"] = "Có lỗi khi thêm cập nhật!!!";
                        XulySanPham.xoaSanPham(sp.MaSp);
                        return RedirectToAction("formThemSP");
                    }
                    if (filehinh != null && filehinh.Count > 0)
                    {
                        if (filehinh.Count > 10)
                        {
                            TempData["MessageError_ThemSP"] = "Chỉ được tải lên tối đa 10 hình ảnh cho mỗi sản phẩm!!!";
                            return RedirectToAction("formThemSP");
                        }
                        if (filehinh.Any(f => f.Length > 2 * 1024 * 1024))
                        {
                            TempData["MessageError_ThemSP"] = "Kích thước mỗi hình ảnh không được vượt quá 2MB!!!";
                            return RedirectToAction("formThemSP");
                        }
                        string thuMucAnh = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "anhsp", sp.MaSp.Trim());
                        if (!Directory.Exists(thuMucAnh))
                        {
                            Directory.CreateDirectory(thuMucAnh);
                        }
                        foreach (IFormFile file in filehinh)
                        {
                            string chuoiRandom = Guid.NewGuid().ToString();
                            string tenfile = sp.MaSp.Trim() + "_" + chuoiRandom + Path.GetExtension(file.FileName);
                            string duongdan = Path.Combine(thuMucAnh, tenfile);
                            using (FileStream f = new FileStream(duongdan, FileMode.Create))
                            {
                                file.CopyTo(f);
                            }
                            Hinh hinh = new Hinh();
                            hinh.MaSp = sp.MaSp;
                            hinh.Tenhinh = tenfile;
                            if(XulyHinh.them(hinh) == false) TempData["MessageError_ThemSP"] = "Thêm thành công, nhưng có lỗi khi thêm hình: "+hinh.Tenhinh;
                        }
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["MessageError_ThemSP"] = "Có lỗi khi thêm sản phẩm!!!";
                    return RedirectToAction("formThemSP");
                }
            }
            return View("formThemSP");
        }

        public IActionResult formXoaSP(string id)
        {
            SanPham? s = XulySanPham.getSanpham(id);
            if (s == null)
            {
                return RedirectToAction("Index");
            }
            CSanPham sp = CSanPham.chuyenDoi(s);
            return View(sp);
        }

        public IActionResult xoaSanPham(string id)
        {
            SanPham? sp = XulySanPham.getSanpham(id);
            if (sp == null || sp.MaSp == null)
            {
                TempData["MessageError_XoaSP"] = "Sản phẩm không tồn tại!!!";
                return RedirectToAction("formXoaSP", new { id = id });
            }
            if (XulyChiTietDonDatHang.getDSChitietdondathangByMasp(id).Count > 0)
            {
                TempData["MessageError_XoaSP"] = "Không thể xóa sản phẩm này!!!";
                return RedirectToAction("formXoaSP", new { id = id });
            }
            try
            {
                XulyCapNhat.xoaAll(sp.MaSp);
                if (XulySanPham.xoaSanPham(sp.MaSp) == false)
                {
                    TempData["MessageError_XoaSP"] = "Có lỗi khi xóa sản phẩm!!!";
                    return RedirectToAction("formXoaSP", new { id = id });
                }
                string thuMucAnh = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "anhsp", sp.MaSp.Trim());
                if (Directory.Exists(thuMucAnh))
                {
                    Directory.Delete(thuMucAnh, true);
                }
                XulyHinh.xoaAll(sp.MaSp);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["MessageError_XoaSP"] = "Có lỗi khi xóa sản phẩm!!!";
                return RedirectToAction("formXoaSP", new { id = id });
            }
        }

        public IActionResult formSuaSP(string id)
        {
            SanPham? sp = XulySanPham.getSanpham(id);
            if (sp == null)
            {
                return RedirectToAction("Index");
            }
            CSanPham csp = CSanPham.chuyenDoi(sp);
            ViewBag.DSNsx = new SelectList(XulyNhaSanXuat.getDSNhasanxuat(), "MaNsx", "Tennsx");
            ViewBag.DSLoai = new SelectList(XulyLoai.getDSLoai(), "MaLoai", "Tenloai");
            return View(csp);
        }

        public IActionResult suaSanPham(CSanPham x)
        {
            ViewBag.DSNsx = new SelectList(XulyNhaSanXuat.getDSNhasanxuat(), "MaNsx", "Tennsx");
            ViewBag.DSLoai = new SelectList(XulyLoai.getDSLoai(), "MaLoai", "Tenloai");
            SanPham sp = CSanPham.chuyenDoi(x);
            if(XulySanPham.suaSanPham(sp.MaSp,sp) == false)
            {
                TempData["MessageError_SuaSP"] = "Có lỗi khi sửa sản phẩm!!!";
                return RedirectToAction("formSuaSP", new { id = x.MaSp });
            }
            return RedirectToAction("Index"); 
        }

        public IActionResult chiTietSP(string id)
        {
            SanPham? s = XulySanPham.getSanpham(id);
            if (s == null) return RedirectToAction("Index");

            CSanPham sp = CSanPham.chuyenDoi(s);
            sp.MaLoaiNavigation = XulyLoai.getLoai(sp.MaLoai??"");
            sp.MaNsxNavigation = XulyNhaSanXuat.getNhasanxuat(sp.MaNsx??"");

            CapNhat? cn = XulyCapNhat.getCapNhatbyMasp(id);
            DateTime? ngayCapNhat = null;
            if (cn != null)
            {
                ngayCapNhat = cn.Ngaycapnhat;
            }
            ViewBag.NgayCapNhat = ngayCapNhat;

            List<Hinh> dsHinh = XulyHinh.getDSHinhByMasp(sp.MaSp);
            ViewBag.DsHinh = dsHinh;
            if (dsHinh != null && dsHinh.Count > 0)
                ViewBag.HinhDaiDien = sp.MaSp.Trim() + "/" + dsHinh[0].Tenhinh;
            else
                ViewBag.HinhDaiDien = "default.png";

            return View(sp);
        }

        public IActionResult chinhSuaKhoHang(string masp, int soluong)
        {
            SanPham? sp = XulySanPham.getSanpham(masp);
            if (sp == null)
            {
                TempData["MessageError_ChiTiet"] = "Có lỗi khi truy vấn sản phẩm!!!";
                return RedirectToAction("chiTietSP", new { id = masp });
            }
            try
            {
                CapNhat cn = new CapNhat();
                string email = HttpContext.Session.GetString("UserEmail") ?? "";
                List<NhanVien> dsNV = XulyNhanVien.getDSNhanvien();
                foreach (NhanVien n in dsNV)
                {
                    if (n.Email.Trim() == email.Trim())
                    {
                        cn.MaNv = n.MaNv;
                        break;
                    }
                }
                if (cn.MaNv == null)
                {
                    TempData["MessageError_ChiTiet"] = "Có lỗi khi cập nhật kho (không tìm thấy nhân viên)!!!";
                    return RedirectToAction("chiTietSP", new { id = masp });
                }
                cn.MaSp = sp.MaSp;
                cn.Ngaycapnhat = DateTime.Now;
                XulyCapNhat.them(cn);

                if (soluong < 0)
                    soluong = 0;
                sp.Soluongton += soluong;
                XulySanPham.suaSanPham(sp.MaSp,sp);
                return RedirectToAction("chiTietSP", new { id = masp });
            }
            catch (Exception)
            {
                TempData["MessageError_ChiTiet"] = "Có lỗi khi cập nhật kho hàng!!!";
                return RedirectToAction("chiTietSP", new { id = masp });
            }
        }

        public IActionResult chinhSuaAnh(string id)
        {
            SanPham? sp = XulySanPham.getSanpham(id);
            if (sp == null)
                return RedirectToAction("chiTietSP", new { id });
            ViewBag.SP = sp;
            List<CHinh> dsHinh = XulyHinh.getDSHinhByMasp(sp.MaSp).Select(x=>CHinh.chuyenDoi(x)).ToList();
            return View(dsHinh);
        }

        public IActionResult xoaAnh(int id)
        {
            Hinh? hinh = XulyHinh.getHinh(id);
            if (hinh == null)
            {
                TempData["MessageError_Anh"] = "Hình ảnh không tồn tại!!!";
                return RedirectToAction("chinhSuaAnh", new { id = id });
            }

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "anhsp", hinh.MaSp.Trim());
            string filePath = Path.Combine(folderPath, hinh.Tenhinh);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                XulyHinh.xoa(id);
            }
            return RedirectToAction("chiTietSP", new { id = hinh.MaSp });
        }

        public IActionResult themAnh(string maSp, List<IFormFile> filehinh)
        {
            SanPham? sp = XulySanPham.getSanpham(maSp);
            if (sp == null) return RedirectToAction("Index", new { MaLoai = (string)null, MaNsx = (string)null, MaSp = (string)null });

            string thuMucAnh = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "anhsp", sp.MaSp.Trim());
            if (!Directory.Exists(thuMucAnh))
            {
                Directory.CreateDirectory(thuMucAnh);
            }
            if (filehinh == null || filehinh.Count == 0)
            {
                TempData["MessageError_Anh"] = "Vui lòng chọn hình ảnh để tải lên!!!";
                return RedirectToAction("chinhSuaAnh", new { id = maSp });
            }
            if (filehinh.Count + XulyHinh.getDSHinhByMasp(sp.MaSp).Count > 10)
            {
                TempData["MessageError_Anh"] = "Chỉ được tải lên tối đa 10 hình ảnh cho mỗi sản phẩm!!!";
                return RedirectToAction("chinhSuaAnh", new { id = maSp });
            }
            if (filehinh.Any(f => f.Length > 2 * 1024 * 1024))
            {
                TempData["MessageError_Anh"] = "Kích thước mỗi hình ảnh không được vượt quá 2MB!!!";
                return RedirectToAction("chinhSuaAnh", new { id = maSp });
            }
            foreach (IFormFile file in filehinh)
            {
                string chuoiRandom = Guid.NewGuid().ToString();
                string tenfile = sp.MaSp + "_" + chuoiRandom + Path.GetExtension(file.FileName);
                string duongdan = Path.Combine(thuMucAnh, tenfile);
                using (FileStream f = new FileStream(duongdan, FileMode.Create))
                {
                    file.CopyTo(f);
                }
                Hinh hinh = new Hinh();
                hinh.MaSp = sp.MaSp;
                hinh.Tenhinh = tenfile;
                if(XulyHinh.them(hinh) == false)
                {
                    TempData["MessageError_Anh"] = "Có lỗi khi tải ảnh!!!";
                    return RedirectToAction("chinhSuaAnh", new { id = maSp });
                }
            }
            return RedirectToAction("chiTietSP", new { id = maSp });
        }
    }
}
