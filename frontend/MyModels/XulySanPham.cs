namespace frontend.Models
{
    public class XulySanPham
    {
        private static string apiUrl = "http://musicstore08.somee.com/api/sanpham"; 
        public static List<SanPham> getDSSanpham()
        {
            try
            {
                HttpClient hc = new HttpClient();
                var kq = hc.GetFromJsonAsync<List<SanPham>>(apiUrl);
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
