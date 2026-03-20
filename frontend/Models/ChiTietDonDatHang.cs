using System;
using System.Collections.Generic;

namespace frontend.Models
{
    public partial class ChiTietDonDatHang
    {
        public int MaDdh { get; set; }
        public string MaSp { get; set; } = null!;
        public int Soluong { get; set; }
        public decimal Gia { get; set; }
        public decimal? Thanhtien { get; set; }

        public virtual DonDatHang MaDdhNavigation { get; set; } = null!;
        public virtual SanPham MaSpNavigation { get; set; } = null!;
    }
}
