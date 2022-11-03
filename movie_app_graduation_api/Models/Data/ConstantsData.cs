using Newtonsoft.Json;

namespace movie_app_graduation_api.Models.Data
{
    public class ConstantsData
    {
        public ConstantsData()
        {
            /* var data = File.ReadAllText("MyConfig.json");
             var json = JsonConvert.DeserializeObject<DataJsonOject>(data);*/

            String? appUrl = GetJson("MyConfig.json")!.AppUrl!;
        }
        private const string mainAuthApiEndpoint = "api/Auth";
        private const string MoviesEndpoint = "api/Movies";
        private const string GenresEndpoint = "api/Genres";

        public const string weatherForCastEndPoint = "WeatherForecast";
        public const string Login = $"{mainAuthApiEndpoint}/login";
        public const string Register = $"{mainAuthApiEndpoint}/register";
        public const string Movies = $"{MoviesEndpoint}/";



        public DataJsonOject? appUrl;

        public DataJsonOject? GetJson(String path)
        {
            try
            {

                var data = File.ReadAllText(path);
                var json = JsonConvert.DeserializeObject<DataJsonOject>(data);
                return json;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }
    }

    public class DataJsonOject
    {
        public String? AppUrl { get; set; }
        public String? Name { get; set; }

    }
}
