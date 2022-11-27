using ClientApiConfig;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace ClientApp.Pages
{
    public class IndexPage: ComponentBase
    {
        protected List<WeatherForecast> WeatherData { get; set; } = new();
        protected string ErrorMsg { get; set; }
        protected string Info { get; set; }
        [Inject]
        public IHttpClientFactory HttpClientFactory { get; set; }

        protected async Task GetWeatherClickHandler(MouseEventArgs args)
        {
            ErrorMsg = "";
            var client = HttpClientFactory.CreateClient("api");
            try
            {
                var response = await client.GetAsync("weatherforecast");
                if (!response.IsSuccessStatusCode)
                    ErrorMsg = $"Nie można było pobrać danych, błąd: {response.StatusCode}";
                else
                {
                    var data = await response.Content.ReadAsStringAsync();
                    WeatherData = new(JsonSerializer.Deserialize<WeatherForecast[]>(data, JsonConfig.DefaultSerializerOptions));
                }
            }catch(HttpRequestException ex)
            {
                ErrorMsg = $"Nie można było pobrać danych, błąd: {ex.Message}";
            }
        }

        protected async Task PostHandler(MouseEventArgs args)
        {
            ErrorMsg = "";
            try
            {
                var data = new WeatherForecast
                {
                    Date = DateTime.Now,
                    Summary = "Cold",
                    TemperatureC = 5
                };

                var client = HttpClientFactory.CreateClient("api");
                //client.DefaultRequestHeaders.Add("X-API-KEY", "abc");
                var response = await client.PostAsJsonAsync("weatherforecast", data);
                Info = $"Kod: {response.StatusCode}";
            }catch(HttpRequestException ex)
            {
                ErrorMsg = $"Nie można wysłać POST: {ex.Message}";
            }
        }

        protected async Task DeleteHandler(MouseEventArgs args)
        {
            ErrorMsg = "";
            try
            {
                var client = HttpClientFactory.CreateClient("api");
                var response = await client.DeleteAsync("weatherforecast");
                Info = $"Kod: {response.StatusCode}";
            }
            catch (HttpRequestException ex)
            {
                ErrorMsg = $"Nie można wysłać DELETE: {ex.Message}";
            }
        }
    }
}
