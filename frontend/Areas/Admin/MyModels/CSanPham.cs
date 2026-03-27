using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using frontend.Models;

namespace frontend.Areas.Admin.MyModels
{
    public class CSanPham
    {
        [Display(Name = "Mã sản phẩm")]
        [Required(ErrorMessage = "Mã sản phẩm không được để trống!")]
        public string MaSp { get; set; } = null!;
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống!")]
        public string Tensp { get; set; } = null!;
        [Display(Name = "Mã nhà sản xuất")]
        [Required(ErrorMessage = "Chọn nhà sản xuất phù hợp!")]
        public string? MaNsx { get; set; }
        [Display(Name = "Mã loại")]
        [Required(ErrorMessage = "Chọn loại cho sản phẩm!")]
        public string? MaLoai { get; set; }
        [Display(Name = "Giá")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá sản phẩm không hợp lệ!")]
        public decimal Giasp { get; set; }
        [Display(Name = "Số lượng tồn")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn không hợp lệ!")]
        [Required(ErrorMessage = "Số lượng tồn không được để trống!")]
        public int? Soluongton { get; set; }
        [Display(Name = "Mô tả")]
        public string? Mota { get; set; }

        public string Tenloai { get; set; } = null!;
        public string Tennsx { get; set; } = null!;

        public static CSanPham chuyenDoi(SanPham sp)
        {
            if(sp == null) return null;
            return new CSanPham
            {
                MaSp = sp.MaSp,
                Tensp = sp.Tensp,
                MaNsx = sp.MaNsx,
                MaLoai = sp.MaLoai,
                Giasp = sp.Giasp,
                Soluongton = sp.Soluongton,
                Mota = sp.Mota,
                Tenloai = sp.Tenloai,
                Tennsx = sp.Tennsx,
            };
        }

        public static SanPham chuyenDoi(CSanPham sp)
        {
            if (sp == null) return null;
            return new SanPham
            {
                MaSp = sp.MaSp,
                Tensp = sp.Tensp,
                MaNsx = sp.MaNsx,
                MaLoai = sp.MaLoai,
                Giasp = sp.Giasp,
                Soluongton = sp.Soluongton,
                Mota = sp.Mota,
                Tenloai = sp.Tenloai,
                Tennsx = sp.Tennsx
            };
        }
    }
}
