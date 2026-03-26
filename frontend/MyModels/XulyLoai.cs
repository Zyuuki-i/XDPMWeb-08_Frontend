namespace frontend.Models
{
    public class XulyLoai
    {
        private static string apiUrl = "http://musicstore08.somee.com/api/loai";
        //private static string apiUrl = "https://localhost:7073/api/Loai";

        private static readonly HttpClient hc = new HttpClient();
        public static List<LoaiSanPham> getDSLoai()
        {
            try
            {
                var kq = hc.GetFromJsonAsync<List<LoaiSanPham>>(apiUrl);
                kq.Wait();
                if (kq.IsCompletedSuccessfully == false)
                    return null;
                return kq.Result;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
