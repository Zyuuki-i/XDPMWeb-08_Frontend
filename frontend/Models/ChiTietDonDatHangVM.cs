using frontend.Areas.Admin.MyModels;

namespace frontend.Models
{
    public class ChiTietDonDatHangVM
    {
        public int MaDdh { get; set; }
        public string MaSp { get; set; } = null!;
        public int Soluong { get; set; }
        public decimal Gia { get; set; }
        public decimal? Thanhtien { get; set; }

        public static ChiTietDonDatHangVM chuyenDoi(ChiTietDonDatHang ctddh)
        {
            if (ctddh == null)
            {
                return null;
            }
            return new ChiTietDonDatHangVM
            {
                MaDdh = ctddh.MaDdh,
                MaSp = ctddh.MaSp,
                Soluong = ctddh.Soluong,
                Gia = ctddh.Gia,
                Thanhtien = ctddh.Thanhtien,
            };
        }
    }
}
