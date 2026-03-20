using System;
using System.Collections.Generic;

namespace frontend.Models
{
    public partial class DanhGia
    {
        public int MaNd { get; set; }
        public string MaSp { get; set; } = null!;
        public string? Noidung { get; set; }
        public int? Sosao { get; set; }

    }
}
