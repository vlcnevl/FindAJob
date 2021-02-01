using Newtonsoft.Json;
using Project.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Project.Views;

namespace Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Signin : ContentPage
    {
        public Signin()
        {
            InitializeComponent();
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Signup());
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgotPassword());
        }
        async void Signin_Clicked(object sender, EventArgs e)
        {

            try
            {
                Login login = new Login();
                login.Email = uMail.Text;
                login.Password = uPass.Text;
              // deserialize işlemlerinde sıkımtı bar bötle giridli mesaju alınıyor.
                string url = "http://192.168.1.40:80/api/user/uselogin";
                HttpClient client = new HttpClient();
                //string jsonData = JsonConvert.SerializeObject(login);
                //   StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                /*
                var result = await client.GetStringAsync(url);
                var resultContent = JsonConvert.DeserializeObject<User>(result);
                */
                /* json deserialize çalışıyor.
                  string json = "{\"Id\":\"1\",\"Name\":\"velican\",\"Surname\":\"evli\",\"Address\":\"Ankara\",\"Email\":\"e@gmail\",\"Password\":\"123\"}";

                
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
                {
                    // Deserialization from JSON  
                    DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(User));
                    User bsObj2 = (User)deserializer.ReadObject(ms);
                    await DisplayAlert("Girildi",bsObj2.Email , "Tamam");
                }

                */


                // string jsonData = JsonConvert.SerializeObject(login);
                //  StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                // HttpResponseMessage response = client.PostAsync(url, content);





                /* önemli
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
               
          
                string jsonVerisi = "";
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    //jsonVerisi adlı değişkene elde ettiği veriyi atıyoruz.
                    jsonVerisi = reader.ReadToEnd(); // id yi aldı
                }
                önemli son  */

                indicator.IsRunning = true;
                
                string jsonData = JsonConvert.SerializeObject(login);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();
                Response responseData = JsonConvert.DeserializeObject<Response>(result);



                string a = Convert.ToString(responseData.Id);

                App.UserID = Convert.ToString(responseData.Id);



                if (responseData.Status==1)
                {
                    indicator.IsRunning = false;
                  //  await DisplayAlert("Girildi", a, "Tamam");
                    // if ile respomse status değerine göre giriş yaptır.
                    await Navigation.PushAsync(new Tabbed());
                }
                else
                {
                    indicator.IsRunning = false;
                    await DisplayAlert("Hata", responseData.Message, "Tamam");
                }
            
            }

            catch (Exception ex)
            {
                await DisplayAlert("Girilmedi", ex.Message, "OK");
               
            }

        }
    }
}