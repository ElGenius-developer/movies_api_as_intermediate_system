using CompuTestApi.Models.Login;
using movie_app_graduation_api;
using movie_app_graduation_api.Models;
using movie_app_graduation_api.Models.Data;
using movie_app_graduation_api.Models.DTOs;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CalculatorApp
{
    internal static class ClientProgram
    {
        readonly static String? baseUrl = new ConstantsData().GetJson("MyConfig.json")!.AppUrl;


        static async Task Main(string[] args)
        {

            await ProcessRepositories();

            Console.ReadKey();
        }

        private static async Task ProcessRepositories()
        {
            // HttpClientHandler clientHandler = new HttpClientHandler();

            using (var client = new HttpClient())
            {


                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
                client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
                //get authorize data
                var authModel = await GetTokenAsync(client);
                if (authModel != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authModel.Token);

                    Console.WriteLine("Data of Weather is :");

                    await GetWeatherForecastAsync(client);
                    Console.WriteLine("************************");
                    Console.WriteLine("Data of Movies is :");
                    await GetMoviesDataAsync(client);



                    /* Console.Write("Enter first number: ");
                     int n1 = int.Parse(Console.ReadLine() ?? "0");
                     Console.Write("Enter second number: ");
                     int n2 = int.Parse(Console.ReadLine() ?? "0");
                     await AddTwoNumbers(client, n1, n2);*/

                }
                else
                {
                    Console.WriteLine("Not Authorized");
                }



            }
        }



        private static async Task<AuthModel?> GetTokenAsync(HttpClient client)
        {
            Uri endpoint = new Uri($"{baseUrl}{ConstantsData.Login}");


            var response = await client.PostAsJsonAsync
                (endpoint, new LoginModel()
                {
                    Email = "ahmed.abdalmola555@gmail.com",
                    Password = "Joker1997@"
                });
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AuthModel>();

        }
        private static async Task GetWeatherForecastAsync(HttpClient client)
        {
            var response = await client.GetAsync(baseUrl + ConstantsData.weatherForCastEndPoint);
            if (response.IsSuccessStatusCode)
            {
                var stringData = await response.Content.ReadAsStringAsync();
                var weatherForecastData = JsonConvert.DeserializeObject<List<WeatherForecast>>
                    (stringData);
                if (weatherForecastData != null)
                    foreach (var item in weatherForecastData)
                    {
                        Console.WriteLine(JsonConvert.SerializeObject(item));
                    }
                else
                    Console.WriteLine("No Data Founded");
            }
            else
            {
                Console.WriteLine($"Request Fail - code:{response.StatusCode}");
            }
        }
        private static async Task GetMoviesDataAsync(HttpClient client)
        {


            Console.WriteLine(baseUrl + ConstantsData.Movies);
            var response = await client.GetAsync(baseUrl + ConstantsData.Movies);
            var stringData = await response.Content.ReadAsStringAsync();
            //var movies = JsonConvert.DeserializeObject<List<Movie>> (stringData);
            Console.WriteLine(stringData);



        }

        private static async Task AddTwoNumbers(HttpClient client, int number1, int number2)
        {


            // Console.WriteLine(baseUrl + "api/Calculator/Add");
            var response = await client.PostAsJsonAsync<CalculatorDto>(baseUrl + "api/Calculator/Add", new CalculatorDto()
            {
                number1 = number1,
                number2 = number2,
            });
            var stringData = await response.Content.ReadAsStringAsync();
            Console.Write($"{number1} + {number2} = {stringData}");
        }

    }

}

