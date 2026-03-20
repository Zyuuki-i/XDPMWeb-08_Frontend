using System;
using System.Collections.Generic;

namespace frontend.Models
{
    public partial class LoaiSanPham
    {
        public string MaLoai { get; set; } = null!;
        public string Tenloai { get; set; } = null!;
        public string? Mota { get; set; }
    }
}
