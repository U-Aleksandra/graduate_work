using graduate_work.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace graduate_work
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageHome : ContentPage
    {
		public PageHome(User user)
		{
            InitializeComponent();
        }

        private void ImageButtonNogti_Clicked(object sender, EventArgs e)
        {
             Navigation.PushAsync(new PageServiceByCaterory(3, "Ногтевой сервис"));
        }

        private void ImageButtonMakeup_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageServiceByCaterory(2, "Макияж"));
        }

        private void ImageButtonHair_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageServiceByCaterory(1, "Волосы"));
        }

        private void ImageButtonBrovi_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageServiceByCaterory(5, "Брови"));
        }

        private void ImageButtonResnicy_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageServiceByCaterory(4, "Ресницы"));
        }

        private void ImageButtonDepilaciya_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageServiceByCaterory(8, "Депиляция"));
        }

        private void ImageButtonaKosmetologiya_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageServiceByCaterory(6, "Косметология"));
        }

        private void ImageButtonMassag_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageServiceByCaterory(7, "Массаж"));
        }
    }
}