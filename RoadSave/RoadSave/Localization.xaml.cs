using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RoadSave
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Localization : ContentPage
	{
        List<Pinezki> pin = new List<Pinezki>();

		public Localization ()
		{
			InitializeComponent ();
            btnGetLocation.Clicked += BtnGetLocation_Clicked;
            btnClearSavePins.Clicked += btnClearSavePins_Clicked;
        }

        private async void btnClearSavePins_Clicked(object sender, EventArgs e)
        {
            await ResetLocations();
        }

        private async Task ResetLocations()
        {
            pin.Clear();
            txtPins.Text = "Pins :";
        }

        private async void BtnGetLocation_Clicked(object sender, EventArgs e)
        {
            await RetreiveLocation();
        }

        private async Task RetreiveLocation()
        {
            
            var locator = CrossGeolocator.Current;

            locator.DesiredAccuracy = 200;

            var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000000);
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromKilometers(2)));

            txtLat.Text = "Latitude: " + position.Latitude.ToString();
            txtLong.Text = "Longitude: " + position.Longitude.ToString();

            pin.Add(new Pinezki { lat = position.Latitude, lon = position.Longitude });

            txtPins.Text = "";

            int i = 0;
            foreach (var item in pin)
            {
                i++;
                txtPins.Text += "ID:"+i+ ",Lat:" + item.lat + ",Long:" + item.lon + "\n";
            }
        }
        
        
    }
    public class Pinezki
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }
}
