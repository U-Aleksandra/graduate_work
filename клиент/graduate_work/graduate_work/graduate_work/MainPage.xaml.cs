using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace graduate_work
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void buttonInput_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageInput());
        }

        private async void buttonRegist_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageRegist());
        }
    }
}
