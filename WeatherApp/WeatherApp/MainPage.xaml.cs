using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        RestService _restService;

        public MainPage()
        {
            InitializeComponent();
            _restService = new RestService();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cityEntry.Text))
            {
                WeatherData weatherData = await _restService.GetWeatherDataAsync(GenerateRequestUri(Constants.OpenWeatherMapEndpoint));
                DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, 0);

                DateTime DTsunrise = time.AddSeconds((double)weatherData.System.Sunrise + weatherData.Timezone);
                DateTime DTsunset = time.AddSeconds((double)weatherData.System.Sunset + weatherData.Timezone);

                sunrise.Text = DTsunrise.ToString();
                sunset.Text = DTsunset.ToString();

                BindingContext = weatherData;
            }
        }

        string GenerateRequestUri(string x)
        {
            string r = x;
            if (x != null && x != "")
            {

                r += $"?zip={cityEntry.Text}";
                r += "&units=imperial";
                r += $"&APPID={Constants.OpenWeatherMapAPIKey}&lang=pt_br";

            }
            return r;
        }
    }
}