using frontend.Models;

namespace frontend.MyModels
{
    public class XulyDanhgia
    {
        private static string apiUrl = "http://musicstore08.somee.com/api/danhgia";
        //private static string apiUrl = "https://localhost:7073/api/DanhGia";

        private static readonly HttpClient hc = new HttpClient();
        public static DanhGia getDanhgia(int mand)
        {
            try
            {

                var kq = hc.GetFromJsonAsync<DanhGia>(apiUrl + @"/" + mand);
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

        public static List<DanhGia> getDSDanhgia()
        {
            try
            {
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

        public static List<DanhGia> getDSDanhgiaByMasp(string masp)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<List<DanhGia>>(apiUrl + @"/SanPham/" + masp);
                kq.Wait();
                if (kq.IsCompletedSuccessfully == false)
                    return new List<DanhGia>();
                return kq.Result;
            }
            catch (Exception)
            {
                return new List<DanhGia>();
            }
        }

    }
}
