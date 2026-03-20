using frontend.Models;

namespace frontend.MyModels
{
    public class XulyNguoidung
    {
        private static string apiUrl = "http://musicstore08.somee.com/api/nguoidung";
        public static List<NguoiDung> getDSNguoidung()
        {
            try
            {
                HttpClient hc = new HttpClient();
                var kq = hc.GetFromJsonAsync<List<NguoiDung>>(apiUrl);
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
