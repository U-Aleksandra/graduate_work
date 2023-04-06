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
    public partial class PageEditProfile : ContentPage
    {
        public PageEditProfile(User user)
        {
            InitializeComponent();
            Content = new StackLayout
            {
                Margin = new Thickness(10, 0, 10, 0),
                Spacing = 0,
                Children =
                    {
                     new Label { Text = user.Name }
                    }
            };
        }
    }
}