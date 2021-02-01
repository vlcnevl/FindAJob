using Newtonsoft.Json;
using Project.Model;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            CreateMap();

            getPin();
           
        }
        void CreateMap()
        {
            /*  Map currentMap = new Map
              {
                  HasScrollEnabled = true,
                  HasZoomEnabled = true,
                  MapType = MapType.Street

              };

              Pin pin = new Pin
              {
                  Type =PinType.Place,
                  Address = "Anıtkabir",
                  Label = "AnıtkabirLabel",
                  Position = new Position(39.925, 32.836944)

              };
              currentMap.Pins.Add(pin);


              currentMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(39.925, 32.836944), Distance.FromKilometers(10)));
              Content = currentMap;
            */
/*
            Pin pin = new Pin
            {
                Type = PinType.Place,
                Address = "Anıtkabir",
                Label = "AnıtkabirLabel",
                Position = new Position(39.925, 32.836944)

            };

            */
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(39.925, 32.836944), Distance.FromKilometers(10)));
        //    MyMap.Pins.Add(pin);

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var pop = new AddWork();
            App.Current.MainPage.Navigation.PushPopupAsync(pop,true);

        }

       public async void getPin()
        {
            string url = "http://192.168.1.40:80/api/Work/getworklist";
            //  HttpClient client = new HttpClient();




          
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(url);
               
                 List<Work> works   = JsonConvert.DeserializeObject<List<Work>>(json);

                // await  DisplayAlert("deneme",works[0].Tittle, "tamam");

                foreach(Work w in works)
                {

                    Pin pin = new Pin
                    {
                        Type = PinType.Place,
                        Address = w.Address,
                        Label = w.Tittle,
                        Position = new Position(w.AddressLatitude,w.AddressLongitude)

                    };
                    MyMap.Pins.Add(pin);
                }


            }



        }
      
    }
    
}