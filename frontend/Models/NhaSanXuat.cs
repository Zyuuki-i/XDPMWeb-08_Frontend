using System;
using System.Collections.Generic;

namespace frontend.Models
{
    public partial class NhaSanXuat
    {

        public string MaNsx { get; set; } = null!;
        public string Tennsx { get; set; } = null!;
        public string? Diachi { get; set; }
        public string? Sdt { get; set; }
        public string? Email { get; set; }

    }
}
