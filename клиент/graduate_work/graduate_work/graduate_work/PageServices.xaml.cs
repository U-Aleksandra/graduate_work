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
    public partial class PageServices : ContentPage
    {
        User localUser;
        public PageServices(User user)
        {
            InitializeComponent();
            localUser = user;
        }

        private async void createServise_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageCreateServise(localUser));
        }
    }
}