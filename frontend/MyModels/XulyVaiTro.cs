using frontend.Models;

namespace frontend.MyModels
{
    public class XulyVaiTro
    {
        private static string apiUrl = "http://musicstore08.somee.com/api/vaitro";
        //private static string apiUrl = "https://localhost:7073/api/VaiTro";

        private static readonly HttpClient hc = new HttpClient();
        public static VaiTro getVaiTro(string mavt)
        {
            try
            {

                var kq = hc.GetFromJsonAsync<VaiTro>(apiUrl + @"/" + mavt);
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

        public static List<VaiTro> getDSVaiTro()
        {
            try
            {
                var kq = hc.GetFromJsonAsync<List<VaiTro>>(apiUrl);
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
