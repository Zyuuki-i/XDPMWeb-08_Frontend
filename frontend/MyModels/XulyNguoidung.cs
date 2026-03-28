using frontend.Models;

namespace frontend.MyModels
{
    public class XulyNguoidung
    {
        private static string apiUrl = "http://musicstore08.somee.com/api/nguoidung";
        //private static string apiUrl = "https://localhost:7073/api/NguoiDung";

        private static readonly HttpClient hc = new HttpClient();
        public static List<NguoiDung> getDSNguoidung()
        {
            try
            {
                var kq = hc.GetFromJsonAsync<List<NguoiDung>>(apiUrl);
                kq.Wait();
                if (kq.IsCompletedSuccessfully == false)
                    return new List<NguoiDung>();
                return kq.Result;
            }
            catch (Exception)
            {
                return new List<NguoiDung>();
            }
        }

        public static List<NguoiDung> getDSNguoidungByTT(bool tt)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<List<NguoiDung>>(apiUrl + @"/TrangThai?trangthai=" + tt);
                kq.Wait();
                if (kq.IsCompletedSuccessfully == false)
                    return new List<NguoiDung>();
                return kq.Result;
            }
            catch (Exception)
            {
                return new List<NguoiDung>();
            }
        }

        public static NguoiDung getNguoidungByMand(int mand)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<NguoiDung>(apiUrl + @"/DanhGia/" + mand);
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

        public static NguoiDung DangNhap(string email, string password)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<NguoiDung>(apiUrl + @"/DangNhap?email="+email+"&password="+password);
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

        public static NguoiDung getNguoidungByEmail(string email)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<NguoiDung>(apiUrl + @"/Email?email=" + email);
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

        public static bool them(NguoiDung x)
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

        public static bool sua(int id, NguoiDung x)
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
