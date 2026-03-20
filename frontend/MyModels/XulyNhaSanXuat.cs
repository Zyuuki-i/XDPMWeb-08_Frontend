namespace frontend.Models
{
    public class XulyNhaSanXuat
    {
        private static string apiUrl = "http://musicstore08.somee.com/api/nhasanxuat";
        public static List<NhaSanXuat> getDSNhasanxuat()
        {
            try
            {
                HttpClient hc = new HttpClient();
                var kq = hc.GetFromJsonAsync<List<NhaSanXuat>>(apiUrl);
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
