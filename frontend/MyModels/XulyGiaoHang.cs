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

        public static List<GiaoHang> getDSGiaohangByTt(string trangthai)
        {
            try
            {

                var kq = hc.GetFromJsonAsync<List<GiaoHang>>(apiUrl+ @"/TrangThai?trangthai="+trangthai);
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
        public static GiaoHang getGiaohang(int magh)
        {
            try
            {

                var kq = hc.GetFromJsonAsync<GiaoHang>(apiUrl + @"/" + magh);
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

        public static bool them(GiaoHang x)
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

        public static bool sua(int id, GiaoHang x)
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

        //end
    }
}
