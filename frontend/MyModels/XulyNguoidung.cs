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

        public static NguoiDung getNguoidungByMand(int mand)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<NguoiDung>(apiUrl + @"/DanhGia/" + mand);
                kq.Wait();
                if (kq.IsCompletedSuccessfully == false)
                    return new NguoiDung();
                return kq.Result;
            }
            catch (Exception)
            {
                return new NguoiDung();
            }

        }
    }
}
