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

        public static List<NhanVien> getDSNhanvienByTT(bool tt)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<List<NhanVien>>(apiUrl + @"/TrangThai?trangthai=" + tt);
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

        public static List<NhanVien> getDSNhanvienByVT(string vt)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<List<NhanVien>>(apiUrl + @"/VaiTro?vaitro=" + vt);
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

        public static NhanVien getNhanvienByCCCD(string cccd)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<NhanVien>(apiUrl + @"/CCCD?cccd=" + cccd);
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

        public static bool them(NhanVien x)
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

        public static bool sua(string id, NhanVien x)
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
    }
}
