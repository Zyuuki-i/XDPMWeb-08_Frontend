using frontend.Areas.Admin.MyModels;

namespace frontend.Models
{
    public class XulySanPham
    {
        private static string apiUrl = "http://musicstore08.somee.com/api/sanpham"; 
        //private static string apiUrl = "https://localhost:7073/api/SanPham";

        private static readonly HttpClient hc = new HttpClient();
        public static List<SanPham> getDSSanpham()
        {
            try
            {
                var kq = hc.GetFromJsonAsync<List<SanPham>>(apiUrl);
                kq.Wait();
                if (kq.IsCompletedSuccessfully == false)
                    return new List<SanPham>();
                return kq.Result;
            }
            catch (Exception)
            {
                return new List<SanPham>();
            }

        }

        public static SanPham getSanpham(string masp)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<SanPham>(apiUrl+@"/"+masp);
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

        public static List<SanPham> locSanPham(string? keyword, string? maloai, string? mansx)
        {
            try
            {
                string url = $"{apiUrl}/loc?" +
                            $"keyword={Uri.EscapeDataString(keyword ?? "")}&" +
                            $"maloai={Uri.EscapeDataString(maloai ?? "")}&" +
                            $"mansx={Uri.EscapeDataString(mansx ?? "")}";

                var kq = hc.GetFromJsonAsync<List<SanPham>>(url);
                kq.Wait();
                if (kq.IsCompletedSuccessfully == false)
                    return new List<SanPham>();
                return kq.Result;
            }
            catch (Exception)
            {
                return new List<SanPham>();
            }
        }

        public static bool themSanPham(SanPham sp)
        {
            try
            {
                var kq = hc.PostAsJsonAsync(apiUrl, sp);
                kq.Wait();

                return kq.Result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public static bool suaSanPham(string id, SanPham sp)
        {
            try
            {
                var kq = hc.PutAsJsonAsync(apiUrl + "/" + id, sp);
                kq.Wait();

                return kq.Result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public static bool xoaSanPham(string id)
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
