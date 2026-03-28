using frontend.Models;

namespace frontend.MyModels
{
    public class XulyChiTietDonDatHang
    {
        private static string apiUrl = "http://musicstore08.somee.com/api/chitietdondathang";
        //private static string apiUrl = "https://localhost:7073/api/ChiTietDonDatHang";

        private static readonly HttpClient hc = new HttpClient();
        public static List<ChiTietDonDatHang> getDSChitietdondathang()
        {
            try
            {
                var kq = hc.GetFromJsonAsync<List<ChiTietDonDatHang>>(apiUrl);
                kq.Wait();
                if (kq.IsCompletedSuccessfully == false)
                    return new List<ChiTietDonDatHang>();
                return kq.Result;
            }
            catch (Exception)
            {
                return new List<ChiTietDonDatHang>();
            }
        }

        public static List<ChiTietDonDatHang> getDSChitietdondathangByMaddh(int maddh)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<List<ChiTietDonDatHang>>(apiUrl+@"/DonDatHang/"+maddh);
                kq.Wait();
                if (kq.IsCompletedSuccessfully == false)
                    return new List<ChiTietDonDatHang>();
                return kq.Result;
            }
            catch (Exception)
            {
                return new List<ChiTietDonDatHang>();
            }
        }

        public static List<ChiTietDonDatHang> getDSChitietdondathangByMasp(string masp)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<List<ChiTietDonDatHang>>(apiUrl + @"/SanPham/" + masp);
                kq.Wait();
                if (kq.IsCompletedSuccessfully == false)
                    return new List<ChiTietDonDatHang>();
                return kq.Result;
            }
            catch (Exception)
            {
                return new List<ChiTietDonDatHang>();
            }
        }

        public static bool themCTDDH(ChiTietDonDatHangDTO ct)
        {
            try
            {
                var kq = hc.PostAsJsonAsync(apiUrl, ct);
                kq.Wait();

                return kq.IsCompletedSuccessfully && kq.Result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public static bool xoaCTDDH(int maddh)
        {
            try
            {
                var kq = hc.DeleteAsync(apiUrl + "/" + maddh);
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
