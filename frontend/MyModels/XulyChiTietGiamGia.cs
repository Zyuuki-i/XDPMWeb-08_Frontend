using frontend.Models;

namespace frontend.MyModels
{
    public class XulyChiTietGiamGia
    {
        private static string apiUrl = "http://musicstore08.somee.com/api/chitietgiamgia";
        //private static string apiUrl = "https://localhost:7073/api/ChiTietGiamGia";
        private static readonly HttpClient hc = new HttpClient();

        public static List<ChiTietGiamGia> getChiTietGiamGiaByNguoiDung(int mand)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<List<ChiTietGiamGia>>(apiUrl + @"/NguoiDung/" + mand);
                kq.Wait();

                if (!kq.IsCompletedSuccessfully || kq.Result == null)
                    return new List<ChiTietGiamGia>();

                return kq.Result;
            }
            catch (Exception)
            {
                return new List<ChiTietGiamGia>();
            }
        }

        public static List<ChiTietGiamGia> getChiTietGiamGia(string magg)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<List<ChiTietGiamGia>>(apiUrl +@"/"+ magg);
                kq.Wait();

                if (!kq.IsCompletedSuccessfully || kq.Result == null)
                    return new List<ChiTietGiamGia>();

                return kq.Result;
            }
            catch (Exception)
            {
                return new List<ChiTietGiamGia>();
            }
        }

        public static List<ChiTietGiamGia> getDSChiTietGiamGia()
        {
            try
            {
                var kq = hc.GetFromJsonAsync<List<ChiTietGiamGia>>(apiUrl);
                kq.Wait();

                if (!kq.IsCompletedSuccessfully || kq.Result == null)
                    return new List<ChiTietGiamGia>();

                return kq.Result;
            }
            catch (Exception)
            {
                return new List<ChiTietGiamGia>();
            }
        }

        public static bool themChiTietGiamGia(ChiTietGiamGiaVM dto)
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

        public static bool suaChiTietGiamGia(ChiTietGiamGiaVM dto)
        {
            try
            {

                var kq = hc.PutAsJsonAsync(apiUrl, dto);
                kq.Wait();

                return kq.IsCompletedSuccessfully && kq.Result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public static bool xoaChiTietGiamGia(ChiTietGiamGiaVM dto)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(apiUrl),
                    Content = JsonContent.Create(dto)
                };

                var kq = hc.SendAsync(request);
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
