using Newtonsoft.Json;
using Project.Model;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWork : PopupPage
    {
        public AddWork()
        {
            InitializeComponent();


        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
           //await App.Current.MainPage.Navigation.PopPopupAsync(true);

            try
            {
                indicator.IsRunning = true;
                btnAdvertise.IsEnabled = false;
                Work work = new Work();
                work.Id = Convert.ToInt32(App.UserID);
                work.Tittle = lblTittle.Text;
                work.Category = lblCategory.Text;
                work.Hıw = lblHıw.Text;
                work.EducationStatus = lblEducation.Text;
                work.Experience = lblExperience.Text;
                work.CompanyName = lblCompanyName.Text;
                work.Address = lblAddress.Text;
                work.CompanyPhone = lblCompanyPhone.Text;
                work.Explanation = lblExplanation.Text;
                work.InfoEmployer = "velicanevli";

                var locations = await Geocoding.GetLocationsAsync(work.Address); // girilen adresi kordinata çevirme 
                var location = locations?.FirstOrDefault();

                work.AddressLongitude =  location.Longitude;
                work.AddressLatitude = location.Latitude;

                App.getWork = work;

                string url = "http://192.168.1.40:80/api/Work/savework";
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(work);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();
                Response responseData = JsonConvert.DeserializeObject<Response>(result);

                if (responseData.Status == 1)
                {
                    indicator.IsRunning = false;
                    await DisplayAlert("Message", responseData.Message, "Tamam");
                    //await Navigation.PushAsync(new Signin());
                    btnAdvertise.IsEnabled = true;
                }
                else
                {
                    await DisplayAlert("Message", responseData.Message, "Tamam");
                    btnAdvertise.IsEnabled = true;
                    return;
                }

                await Navigation.PushAsync(new Tabbed());
                await App.Current.MainPage.Navigation.PopPopupAsync(true);
   

            }
            catch(Exception ex)
            {
                await DisplayAlert("Message", ex.Message, "Tamam");
                btnAdvertise.IsEnabled = true;
                return;
            }

          
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopPopupAsync(true);
        }
    }
}