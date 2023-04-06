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
    public partial class PageProfile : ContentPage
    {
        private User localUser;
        public PageProfile(User user)
        {
            localUser = user;

            InitializeComponent();
            if (user is Specialist specialist)
            {
                Content = new StackLayout
                {
                    Margin = new Thickness(10, 0, 10, 0),
                    Spacing = 0,
                    Children =
                    {
                        new Label { Text = "Имя", FontFamily="Roboto", FontSize = 15,TextColor = Color.FromHex("#5147AC"), Margin = new Thickness(10)},
                        new Label { Text = specialist.Name, FontFamily="Roboto", FontSize = 18,TextColor = Color.Black, Margin = new Thickness(10)},
                        new BoxView { Color = Color.FromHex("#5147AC"), HeightRequest = 1, HorizontalOptions = LayoutOptions.Fill},

                        new Label { Text = "О себе", FontFamily="Roboto", FontSize = 15,TextColor = Color.FromHex("#5147AC"), Margin = new Thickness(10)},
                        new Label { Text = specialist.Description, FontFamily="Roboto", FontSize = 18,TextColor = Color.Black, Margin = new Thickness(10)},
                        new BoxView { Color = Color.FromHex("#5147AC"), HeightRequest = 1, HorizontalOptions = LayoutOptions.Fill},

                        new Label { Text = "Направление деятельности", FontFamily="Roboto", FontSize = 15,TextColor = Color.FromHex("#5147AC"), Margin = new Thickness(10)},
                        new Label { Text = specialist.Category, FontFamily="Roboto", FontSize = 18,TextColor = Color.Black, Margin = new Thickness(10)},
                        new BoxView { Color = Color.FromHex("#5147AC"), HeightRequest = 1, HorizontalOptions = LayoutOptions.Fill},

                        new Label { Text = "Контакты", FontFamily="Roboto",FontAttributes = FontAttributes.Bold, FontSize = 18,TextColor = Color.FromHex("#5147AC"), Margin = new Thickness(10)},
                        new BoxView { Color = Color.FromHex("#5147AC"), HeightRequest = 1, HorizontalOptions = LayoutOptions.Fill},

                        new Label { Text = "Адрес", FontFamily="Roboto", FontSize = 15,TextColor = Color.FromHex("#5147AC"), Margin = new Thickness(10)},
                        new Label { Text = specialist.Address, FontFamily="Roboto", FontSize = 18,TextColor = Color.Black, Margin = new Thickness(10)},
                        new BoxView { Color = Color.FromHex("#5147AC"), HeightRequest = 1, HorizontalOptions = LayoutOptions.Fill},

                        new Label { Text = "Телефон", FontFamily="Roboto", FontSize = 15,TextColor = Color.FromHex("#5147AC"), Margin = new Thickness(10)},
                        new Label { Text = specialist.Phone, FontFamily="Roboto", FontSize = 18,TextColor = Color.Black, Margin = new Thickness(10)},
                        new BoxView { Color = Color.FromHex("#5147AC"), HeightRequest = 1, HorizontalOptions = LayoutOptions.Fill}
                    }
                };
            }
            else
            {
                Content = new StackLayout
                {
                    Margin = new Thickness(10, 0, 10, 0),
                    Spacing = 0,
                    Children =
                    {
                        new Label { Text = "Имя", FontFamily = "Roboto", FontSize = 15, TextColor = Color.FromHex("#5147AC"), Margin = new Thickness(10) },
                        new Label { Text = user.Name, FontFamily = "Roboto", FontSize = 18, TextColor = Color.Black, Margin = new Thickness(10) },
                        new BoxView { Color = Color.FromHex("#5147AC"), HeightRequest = 1, HorizontalOptions = LayoutOptions.Fill },

                        new Label { Text = "Контакты", FontFamily = "Roboto", FontAttributes = FontAttributes.Bold, FontSize = 18, TextColor = Color.FromHex("#5147AC"), Margin = new Thickness(10) },
                        new BoxView { Color = Color.FromHex("#5147AC"), HeightRequest = 1, HorizontalOptions = LayoutOptions.Fill },

                        new Label { Text = "Телефон", FontFamily = "Roboto", FontSize = 15, TextColor = Color.FromHex("#5147AC"), Margin = new Thickness(10) },
                        new Label { Text = user.Phone, FontFamily = "Roboto", FontSize = 18, TextColor = Color.Black, Margin = new Thickness(10) },
                        new BoxView { Color = Color.FromHex("#5147AC"), HeightRequest = 1, HorizontalOptions = LayoutOptions.Fill }
                    }
                };
            }
        }
        private async void edidProfileClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageEditProfile(localUser));
        }
    }
}