namespace frontend.Models
{
    public class XulyLoai
    {
        private static string apiUrl = "http://musicstore08.somee.com/api/loai";
        //private static string apiUrl = "https://localhost:7073/api/Loai";

        private static readonly HttpClient hc = new HttpClient();
        public static List<LoaiSanPham> getDSLoai()
        {
            try
            {
                var kq = hc.GetFromJsonAsync<List<LoaiSanPham>>(apiUrl);
                kq.Wait();
                if (kq.IsCompletedSuccessfully == false)
                    return new List<LoaiSanPham>();
                return kq.Result;
            }
            catch (Exception)
            {
                return new List<LoaiSanPham>();
            }
        }

        public static LoaiSanPham getLoai(string id)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<LoaiSanPham>(apiUrl+@"/"+id);
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

        public static bool them(LoaiSanPham x)
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

        public static bool sua(string id, LoaiSanPham x)
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
