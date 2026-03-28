namespace frontend.Models
{
    public class ChiTietGiamGiaDTO
    {
        public int MaNd { get; set; }
        public string MaGg { get; set; } = null!;
        public int? Soluong { get; set; }

        public static ChiTietGiamGiaDTO chuyenDoi(ChiTietGiamGia ct)
        {
            if (ct == null) return null;
            return new ChiTietGiamGiaDTO
            {
                MaNd = ct.MaNd,
                MaGg = ct.MaGg,
                Soluong = ct.Soluong
            };
        }
    }
}
