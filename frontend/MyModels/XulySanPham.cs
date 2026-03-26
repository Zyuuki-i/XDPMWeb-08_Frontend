namespace frontend.Models
{
    public class XulySanPham
    {
        private static string apiUrl = "http://musicstore08.somee.com/api/sanpham"; 
        //private static string apiUrl = "https://localhost:7073/api/SanPham";

        private static readonly HttpClient hc = new HttpClient();
        public static List<SanPham> getDSSanpham()
        {
            try
            {
                var kq = hc.GetFromJsonAsync<List<SanPham>>(apiUrl);
                kq.Wait();
                if (kq.IsCompletedSuccessfully == false)
                    return new List<SanPham>();
                return kq.Result;
            }
            catch (Exception)
            {
                return new List<SanPham>();
            }

        }

        public static SanPham getSanpham(string masp)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<SanPham>(apiUrl+@"/"+masp);
                kq.Wait();
                if (kq.IsCompletedSuccessfully == false)
                    return new SanPham();
                return kq.Result;
            }
            catch (Exception)
            {
                return new SanPham();
            }
        }

        public static List<SanPham> locSanPham(string? keyword, string? maloai, string? mansx)
        {
            try
            {
                string url = $"{apiUrl}/loc?" +
                            $"keyword={Uri.EscapeDataString(keyword ?? "")}&" +
                            $"maloai={Uri.EscapeDataString(maloai ?? "")}&" +
                            $"mansx={Uri.EscapeDataString(mansx ?? "")}";

                var kq = hc.GetFromJsonAsync<List<SanPham>>(url);
                kq.Wait();
                if (kq.IsCompletedSuccessfully == false)
                    return new List<SanPham>();
                return kq.Result;
            }
            catch (Exception)
            {
                return new List<SanPham>();
            }
        }

    }
}
