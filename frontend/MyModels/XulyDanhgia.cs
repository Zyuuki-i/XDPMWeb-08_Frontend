using frontend.Models;

namespace frontend.MyModels
{
    public class XulyDanhgia
    {
        private static string apiUrl = "http://musicstore08.somee.com/api/danhgia";
        public static List<DanhGia> getDSDanhgia()
        {
            try
            {
                HttpClient hc = new HttpClient();
                var kq = hc.GetFromJsonAsync<List<DanhGia>>(apiUrl);
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
