using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace widgetSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void postButton_Clicked(object sender, EventArgs e)
        {
            DateTime aDateTime = new DateTime(2021, 12, 23); 
            var euroNok = postingToInterface.getEuroToNokPrice(aDateTime);
            loggingRelated.writeALineToLog("Clicked app: " + euroNok);

        }

        private async void openLogButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new loggingPage());
        }
    }
}
