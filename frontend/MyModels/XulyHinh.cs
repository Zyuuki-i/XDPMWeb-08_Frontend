namespace frontend.Models
{
    public class XulyHinh
    {
        private static string apiUrl = "http://musicstore08.somee.com/api/hinh";
        public static List<Hinh> getDSHinh()
        {
            try
            {
                HttpClient hc = new HttpClient();
                var kq = hc.GetFromJsonAsync<List<Hinh>>(apiUrl);
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
