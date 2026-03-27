using System;
using System.Collections.Generic;

namespace frontend.Models
{
    public partial class NhanVien
    {
        public string MaNv { get; set; } = null!;
        public string Tennv { get; set; } = null!;
        public string Matkhau { get; set; } = null!;
        public bool Phai { get; set; }
        public string? Sdt { get; set; }
        public string Email { get; set; } = null!;
        public string Cccd { get; set; } = null!;
        public string? Diachi { get; set; }
        public string? Hinh { get; set; }
        public string MaVt { get; set; } = null!;
        public string Tenvt { get; set; } = null!;
        public bool? Trangthai { get; set; }
    }
}
