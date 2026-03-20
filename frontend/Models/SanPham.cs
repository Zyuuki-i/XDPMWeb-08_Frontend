using System;
using System.Collections.Generic;

namespace frontend.Models
{
    public partial class SanPham
    {
        public string MaSp { get; set; } = null!;
        public string Tensp { get; set; } = null!;
        public string Tenloai { get; set; } = null!;
        public string Tennsx { get; set; } = null!;
        public string? MaNsx { get; set; }
        public string? MaLoai { get; set; }
        public decimal Giasp { get; set; }
        public int? Soluongton { get; set; }
        public string? Mota { get; set; }

    }
}
