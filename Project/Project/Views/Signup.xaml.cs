using Newtonsoft.Json;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Project.Views;

namespace Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Signup : ContentPage
    {
        public Signup()
        {
            InitializeComponent();
        }


        async void Button_Clicked(object sender, EventArgs e)
        {

            try
            {
                Register.IsEnabled = false;
                User senduser = new User();
                senduser.Email = lblUserEmail.Text;
                senduser.Name = lblUserName.Text;
                senduser.Surname = lblUserSurname.Text;
                senduser.Password = lblUserPassword.Text;
                senduser.Mobile = lblMobile.Text;
                senduser.Address = lblAdress.Text;

                string url = "http://192.168.1.40:80/api/User/SaveUser";
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(senduser);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();
                Response responseData = JsonConvert.DeserializeObject<Response>(result);

                if (responseData.Status == 1)
                {
                    await Navigation.PushAsync(new Signin());
                    Register.IsEnabled = true;
                }
                else
                {
                    await DisplayAlert("Message", responseData.Message, "Tamam");
                    Register.IsEnabled = true;
                    return;
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", ex.Message, "Tamam");
                Register.IsEnabled = true;
                return;
            }
                

        }

    }
}