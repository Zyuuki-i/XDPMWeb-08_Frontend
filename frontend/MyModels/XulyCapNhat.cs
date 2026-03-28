using frontend.Models;

namespace frontend.MyModels
{
    public class XulyCapNhat
    {
        private static string apiUrl = "http://musicstore08.somee.com/api/capnhat";
        //private static string apiUrl = "https://localhost:7073/api/CapNhat";

        private static readonly HttpClient hc = new HttpClient();
        public static List<CapNhat> getDSCapNhat()
        {
            try
            {
                var kq = hc.GetFromJsonAsync<List<CapNhat>>(apiUrl);
                kq.Wait();
                if (kq.IsCompletedSuccessfully == false)
                    return new List<CapNhat>();
                return kq.Result;
            }
            catch (Exception)
            {
                return new List<CapNhat>();
            }
        }

        public static CapNhat getCapNhat(int id)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<CapNhat>(apiUrl + @"/" + id);
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

        public static CapNhat getCapNhatbyMasp(string id)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<CapNhat>(apiUrl + @"/SanPham/" + id);
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

        public static bool them(CapNhat x)
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

        public static bool sua(int id, CapNhat x)
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

        public static bool xoa(int id)
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

        public static bool xoaAll(string masp)
        {
            try
            {
                var kq = hc.DeleteAsync(apiUrl + "/SanPham/" + masp);
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
