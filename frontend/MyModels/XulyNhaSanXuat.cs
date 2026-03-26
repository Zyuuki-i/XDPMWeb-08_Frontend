namespace frontend.Models
{
    public class XulyNhaSanXuat
    {
        private static string apiUrl = "http://musicstore08.somee.com/api/nhasanxuat";
        //private static string apiUrl = "https://localhost:7073/api/NhaSanXuat";

        private static readonly HttpClient hc = new HttpClient();
        public static List<NhaSanXuat> getDSNhasanxuat()
        {
            try
            {
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
