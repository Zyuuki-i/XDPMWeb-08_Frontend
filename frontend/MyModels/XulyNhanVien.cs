using frontend.Models;

namespace frontend.MyModels
{
    public class XulyNhanVien
    {
        private static string apiUrl = "http://musicstore08.somee.com/api/nhanvien";
        //private static string apiUrl = "https://localhost:7073/api/NhanVien";

        private static readonly HttpClient hc = new HttpClient();
        public static List<NhanVien> getDSNhanvien()
        {
            try
            {
                var kq = hc.GetFromJsonAsync<List<NhanVien>>(apiUrl);
                kq.Wait();
                if (kq.IsCompletedSuccessfully == false)
                    return new List<NhanVien>();
                return kq.Result;
            }
            catch (Exception)
            {
                return new List<NhanVien>();
            }

        }

        public static NhanVien getNhanvienByManv(string manv)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<NhanVien>(apiUrl + @"/" + manv);
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

        public static NhanVien DangNhap(string email, string password)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<NhanVien>(apiUrl + @"/DangNhap?email=" + email + "&password=" + password);
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

        public static NhanVien getNhanvienByEmail(string email)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<NhanVien>(apiUrl + @"/Email?email=" + email);
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

        public static bool themNhanvien(NhanVien nv)
        {
            try
            {
                var kq = hc.PostAsJsonAsync(apiUrl, nv);
                kq.Wait();

                return kq.IsCompletedSuccessfully && kq.Result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool suaNhanvien(string id, NhanVien nv)
        {
            try
            {
                var kq = hc.PutAsJsonAsync(apiUrl + "/" + id, nv);
                kq.Wait();

                return kq.IsCompletedSuccessfully && kq.Result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool xoaNhanvien(string id)
        {
            try
            {
                var kq = hc.DeleteAsync(apiUrl + "/" + id);
                kq.Wait();

                return kq.IsCompletedSuccessfully && kq.Result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
