using frontend.Areas.Admin.MyModels;
using frontend.Models;

namespace frontend.MyModels
{
    public class XulyDonDatHang
    {
        private static string apiUrl = "http://musicstore08.somee.com/api/dondathang";
        //private static string apiUrl = "https://localhost:7073/api/DonDatHang";

        private static readonly HttpClient hc = new HttpClient();
        public static List<DonDatHang> getDSDondathang()
        {
            try
            {
                var kq = hc.GetFromJsonAsync<List<DonDatHang>>(apiUrl);
                kq.Wait();
                if (kq.IsCompletedSuccessfully == false)
                    return new List<DonDatHang>();
                return kq.Result;
            }
            catch (Exception)
            {
                return new List<DonDatHang>();
            }
        }

        public static DonDatHang getDondathang(int maddh)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<DonDatHang>(apiUrl +@"/"+maddh);
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

        public static bool themDonDatHang(DonDatHangVM dto)
        {
            try
            {
                var kq = hc.PostAsJsonAsync(apiUrl, dto);
                kq.Wait();

                return kq.IsCompletedSuccessfully && kq.Result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public static bool suaDonDatHang(int id, DonDatHangVM ddh)
        {
            try
            {
                var kq = hc.PutAsJsonAsync(apiUrl + "/" + id, ddh);
                kq.Wait();

                return kq.IsCompletedSuccessfully && kq.Result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public static bool xoaDonDatHang(int id)
        {
            try
            {
                var kq = hc.DeleteAsync(apiUrl + "/" + id);
                kq.Wait();

                return kq.IsCompletedSuccessfully && kq.Result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        //end
    }
}
