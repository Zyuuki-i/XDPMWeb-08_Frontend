using frontend.Models;
using System;
using System.Collections.Generic;

namespace frontend.MyModels
{
    public partial class MDonDatHang
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
        public List<ChiTietDonDatHangDTO>? chiTietDonDatHangs { get; set; }

        public static MDonDatHang chuyenDoi(DonDatHang ddh)
        {
            if (ddh == null)
            {
                return null;
            }
            return new MDonDatHang
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
