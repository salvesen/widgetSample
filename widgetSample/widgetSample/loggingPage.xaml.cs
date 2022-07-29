using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace widgetSample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class loggingPage : ContentPage
    {
        public loggingPage()
        {
            InitializeComponent();
            loggingEditor.Text = Preferences.Get("log", " ");
        }
    }
}