using frontend.Areas.Admin.MyModels;

namespace frontend.Models
{
    public class DonDatHangVM
    {
        public int MaDdh { get; set; }
        public int MaNd { get; set; }
        public string? MaNv { get; set; }
        public string? Nguoinhan { get; set; }
        public string? Sdt { get; set; }
        public string Diachi { get; set; } = null!;
        public string? Phuongxa { get; set; }
        public string? Tinhthanh { get; set; }
        public DateTime? Ngaydat { get; set; }
        public decimal? Tongtien { get; set; }
        public string Trangthai { get; set; } = null!;
        public string? TtThanhtoan { get; set; }
        public string? Phuongthuc { get; set; }
        public static DonDatHangVM chuyenDoi(DonDatHang ddh)
        {
            if (ddh == null)
            {
                return null;
            }
            return new DonDatHangVM
            {
                MaDdh = ddh.MaDdh,
                MaNd = ddh.MaNd,
                MaNv = ddh.MaNv,
                Nguoinhan = ddh.Nguoinhan,
                Sdt = ddh.Sdt,
                Diachi = ddh.Diachi,
                Phuongxa = ddh.Phuongxa,
                Tinhthanh = ddh.Tinhthanh,
                Ngaydat = ddh.Ngaydat,
                Tongtien = ddh.Tongtien,
                Trangthai = ddh.Trangthai,
                TtThanhtoan = ddh.TtThanhtoan,
                Phuongthuc = ddh.Phuongthuc,
            };
        }
    }
}
