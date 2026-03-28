using frontend.Models;

namespace frontend.MyModels
{
    public class XulyGiamGia
    {
        private static string apiUrl = "http://musicstore08.somee.com/api/giamgia";
        //private static string apiUrl = "https://localhost:7073/api/GiamGia";
        private static readonly HttpClient hc = new HttpClient();

        public static GiamGia getGiamGia(string magg)
        {
            try
            {
                var kq = hc.GetFromJsonAsync <GiamGia>(apiUrl + @"/" + magg);
                kq.Wait();

                if (!kq.IsCompletedSuccessfully || kq.Result == null)
                    return null;

                return kq.Result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<GiamGia> geDSGiamGia()
        {
            try
            {
                var kq = hc.GetFromJsonAsync<List<GiamGia>>(apiUrl);
                kq.Wait();

                if (!kq.IsCompletedSuccessfully || kq.Result == null)
                    return new List<GiamGia>();

                return kq.Result;
            }
            catch (Exception)
            {
                return new List<GiamGia>();
            }
        }

        public static bool them(GiamGia x)
        {
            try
            {
                var kq = hc.PostAsJsonAsync(apiUrl, x);
                kq.Wait();

                return kq.Result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public static bool sua(string id, GiamGia x)
        {
            try
            {
                var kq = hc.PutAsJsonAsync(apiUrl + "/" + id, x);
                kq.Wait();

                return kq.Result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public static bool xoa(string id)
        {
            try
            {
                var kq = hc.DeleteAsync(apiUrl + "/" + id);
                kq.Wait();

                return kq.Result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        //end
    }
}
