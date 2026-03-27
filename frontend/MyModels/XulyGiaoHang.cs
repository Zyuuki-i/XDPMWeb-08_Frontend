using frontend.Models;

namespace frontend.MyModels
{
    public class XulyGiaoHang
    {
        private static string apiUrl = "http://musicstore08.somee.com/api/giaohang";
        //private static string apiUrl = "https://localhost:7073/api/GiaoHang";

        private static readonly HttpClient hc = new HttpClient();
        public static List<GiaoHang> getDSGiaohang()
        {
            try
            {

                var kq = hc.GetFromJsonAsync<List<GiaoHang>>(apiUrl);
                kq.Wait();
                if (kq.IsCompletedSuccessfully == false)
                    return new List<GiaoHang>();
                return kq.Result;
            }
            catch (Exception)
            {
                return new List<GiaoHang>();
            }

        }
    }
}
