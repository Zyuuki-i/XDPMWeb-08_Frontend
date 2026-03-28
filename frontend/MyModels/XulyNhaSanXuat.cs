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

        public static NhaSanXuat getNhasanxuat(string id)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<NhaSanXuat>(apiUrl+@"/"+id);
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

        public static bool them(NhaSanXuat x)
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

        public static bool sua(string id, NhaSanXuat x)
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
