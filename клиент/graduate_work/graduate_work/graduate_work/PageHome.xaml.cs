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
        public User localUser;
		public PageHome(User user)
		{
            InitializeComponent();
            localUser = user;
        }

        private void ImageButtonNogti_Clicked(object sender, EventArgs e)
        {
             Navigation.PushAsync(new PageServiceByCaterory(3, "Ногтевой сервис", localUser));
        }

        private void ImageButtonMakeup_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageServiceByCaterory(2, "Макияж", localUser));
        }

        private void ImageButtonHair_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageServiceByCaterory(1, "Волосы", localUser));
        }

        private void ImageButtonBrovi_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageServiceByCaterory(5, "Брови", localUser));
        }

        private void ImageButtonResnicy_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageServiceByCaterory(4, "Ресницы", localUser));
        }

        private void ImageButtonDepilaciya_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageServiceByCaterory(8, "Депиляция", localUser));
        }

        private void ImageButtonaKosmetologiya_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageServiceByCaterory(6, "Косметология", localUser));
        }

        private void ImageButtonMassag_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageServiceByCaterory(7, "Массаж", localUser));
        }
    }
}