using System;
using System.Collections.Generic;

namespace frontend.Models
{
    public partial class ChiTietGiamGia
    {
        public int MaNd { get; set; }
        public string MaGg { get; set; } = null!;
        public int? Soluong { get; set; }

        public virtual GiamGia MaGgNavigation { get; set; } = null!;
        public virtual NguoiDung MaNdNavigation { get; set; } = null!;
    }
}
