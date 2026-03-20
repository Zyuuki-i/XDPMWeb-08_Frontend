namespace frontend.Models
{
    public class XulyLoai
    {
        private static string apiUrl = "http://musicstore08.somee.com/api/loai";
        public static List<LoaiSanPham> getDSLoai()
        {
            try
            {
                HttpClient hc = new HttpClient();
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
