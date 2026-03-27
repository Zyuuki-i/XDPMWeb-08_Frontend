namespace frontend.Models
{
    public class ChiTietGiamGiaVM
    {
        public int MaNd { get; set; }
        public string MaGg { get; set; } = null!;
        public int? Soluong { get; set; }

        public static ChiTietGiamGiaVM chuyenDoi(ChiTietGiamGia ct)
        {
            if (ct == null) return null;
            return new ChiTietGiamGiaVM
            {
                MaNd = ct.MaNd,
                MaGg = ct.MaGg,
                Soluong = ct.Soluong
            };
        }
    }
}
