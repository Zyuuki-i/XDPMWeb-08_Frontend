namespace frontend.Models
{
    public class XulyHinh
    {
        private static string apiUrl = "http://musicstore08.somee.com/api/hinh";
        //private static string apiUrl = "https://localhost:7073/api/Hinh";

        private static readonly HttpClient hc = new HttpClient();
        public static List<Hinh> getDSHinh()
        {
            try
            {
                var kq = hc.GetFromJsonAsync<List<Hinh>>(apiUrl);
                kq.Wait();
                if (kq.IsCompletedSuccessfully == false)
                    return new List<Hinh>();
                return kq.Result;
            }
            catch (Exception)
            {
                return new List<Hinh>();
            }
        }

        public static List<Hinh> getDS1Hinh()
        {
            try
            {
                var kq = hc.GetFromJsonAsync<List<Hinh>>(apiUrl+@"/only");
                kq.Wait();
                if (kq.IsCompletedSuccessfully == false)
                    return new List<Hinh>();
                return kq.Result;
            }
            catch (Exception)
            {
                return new List<Hinh>();
            }
        }


        public static List<Hinh> getDSHinhByMasp(string masp)
        {
            try
            {
                var kq = hc.GetFromJsonAsync<List<Hinh>>(apiUrl + @"/SanPham/" + masp);
                kq.Wait();
                if (kq.IsCompletedSuccessfully == false)
                    return new List<Hinh>();
                return kq.Result;
            }
            catch (Exception)
            {
                return new List<Hinh>();
            }
        }



        //end
    }
}
