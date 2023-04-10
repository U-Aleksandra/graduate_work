using graduate_work.Droid;
using graduate_work.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace graduate_work
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageEditProfile : ContentPage
    {
        private User localUser;
        private Entry entryName;
        private Entry entryPhone;
        private Entry entryDescription;
        private Entry entryAddress;
        Label errorName = new Label() { FontFamily = "Roboto", FontSize = 18, TextColor = Color.Red, Margin = new Thickness(10, 0, 0, 0) };
        Label errorPhone = new Label() { FontFamily = "Roboto", FontSize = 18, TextColor = Color.Red, Margin = new Thickness(10, 0, 0, 0) };
        Label errorAddress = new Label() { FontFamily = "Roboto", FontSize = 18, TextColor = Color.Red, Margin = new Thickness(10, 0, 0, 0) };
        bool _nameIsValid;
        bool _phoneIsValid;
        bool _addressIsValid;

        private readonly string urlUser = $"https://{apiConfig.url}:7113/api/Users";
        private readonly string urlspecialist = $"https://{apiConfig.url}:7113/api/Specialists";
        public PageEditProfile(User user)
        {
            entryName = new Entry() { Text = user.Name, FontFamily = "Roboto", FontSize = 18, TextColor = Color.Black, Margin = new Thickness(10) };
            entryPhone = new Entry() { Text = user.Phone, FontFamily = "Roboto", FontSize = 18, TextColor = Color.Black, Margin = new Thickness(10) };
            
            InitializeComponent();
            if(user is Specialist specialist)
            {
                localUser = specialist;
                entryDescription = new Entry() { Text = specialist.Description, FontFamily = "Roboto", FontSize = 18, TextColor = Color.Black, Margin = new Thickness(10) };
                entryAddress = new Entry() { Text = specialist.Address, FontFamily = "Roboto", FontSize = 18, TextColor = Color.Black, Margin = new Thickness(10) };

                Content = new StackLayout
                {
                    Margin = new Thickness(10, 0, 10, 0),
                    Spacing = 0,
                    Children =
                    {
                        new Label { Text = "Имя", FontFamily="Roboto", FontSize = 15,TextColor = Color.FromHex("#5147AC"), Margin = new Thickness(10)},
                        entryName,
                        errorName,

                        new Label { Text = "О себе", FontFamily="Roboto", FontSize = 15,TextColor = Color.FromHex("#5147AC"), Margin = new Thickness(10)},
                        entryDescription,
                      
                        new Label { Text = "Контакты", FontFamily="Roboto",FontAttributes = FontAttributes.Bold, FontSize = 18,TextColor = Color.FromHex("#5147AC"), Margin = new Thickness(10)},
                        new BoxView { Color = Color.FromHex("#5147AC"), HeightRequest = 1, HorizontalOptions = LayoutOptions.Fill},

                        new Label { Text = "Адрес", FontFamily="Roboto", FontSize = 15,TextColor = Color.FromHex("#5147AC"), Margin = new Thickness(10,0,0,0)},
                        entryAddress,
                        errorAddress,

                        new Label { Text = "Телефон", FontFamily="Roboto", FontSize = 15,TextColor = Color.FromHex("#5147AC"), Margin = new Thickness(10,0,0,0)},
                        entryPhone,
                        errorPhone
                    }
                };
            }
            else
            {
                localUser = user;
                Content = new StackLayout
                {
                    Margin = new Thickness(10, 0, 10, 0),
                    Spacing = 0,
                    Children =
                    {
                        new Label { Text = "Имя", FontFamily="Roboto", FontSize = 15,TextColor = Color.FromHex("#5147AC"), Margin = new Thickness(10,20,0,0)},
                        entryName,
                        errorName,

                        new Label { Text = "Контакты", FontFamily="Roboto",FontAttributes = FontAttributes.Bold, FontSize = 18,TextColor = Color.FromHex("#5147AC"), Margin = new Thickness(10)},
                        new BoxView { Color = Color.FromHex("#5147AC"), HeightRequest = 1, HorizontalOptions = LayoutOptions.Fill},

                        new Label { Text = "Телефон", FontFamily="Roboto", FontSize = 15,TextColor = Color.FromHex("#5147AC"), Margin = new Thickness(10,0,0,0)},
                        entryPhone,
                        errorPhone
                    }
                };
            }
        }

        [Obsolete]
        private async void saveProfileClicked(object sender, EventArgs e)
        {
            if(entryName.Text == string.Empty)
            {
                _nameIsValid = false;
                errorName.Text = "Введите имя";
            }
            else
            {
                _nameIsValid = true;
                errorName.Text = string.Empty;
            }

            if(entryPhone.Text == string.Empty)
            {
                _phoneIsValid = false;
                errorPhone.Text = "Введите номер телефона";
            }
            else
            {
                _phoneIsValid = true;
                errorPhone.Text = string.Empty;
            }

            if(localUser is Specialist specialist)
            {
                if(entryAddress.Text == string.Empty)
                {
                    _addressIsValid = false;
                    errorAddress.Text = "Введите адрес салона";
                }
                else
                {
                    _addressIsValid = true;
                    errorAddress.Text = string.Empty;
                }

                if (_nameIsValid && _phoneIsValid && _addressIsValid)
                {
                    if (entryName.Text.Equals(localUser.Name) && entryPhone.Text.Equals(localUser.Phone) 
                        && entryAddress.Text.Equals(specialist.Address) && entryDescription.Text.Equals(specialist.Description))
                    {
                        await DisplayAlert("Результат", "Данные не были изменены", "Ok");
                    }
                    else
                    {
                        if (!entryPhone.Text.Equals(localUser.Phone))
                        {
                            await DisplayAlert("Внимание!", "Номер телефона изменен! Теперь вход будет осуществляться по номеру телефона: " + entryPhone.Text, "Ok");
                        }

                        localUser.Name = entryName.Text;
                        localUser.Phone = entryPhone.Text;
                        specialist.Address = entryAddress.Text;
                        specialist.Description = entryDescription.Text;

                        JsonContent content = JsonContent.Create(specialist);
                        var response = await apiConfig.client.PutAsync(urlspecialist, content);
                        string result = await response.Content.ReadAsStringAsync();
                        await DisplayAlert("Результат", result, "Ok");

                        Navigation.RemovePage(this);
                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                        await Navigation.PushModalAsync(new NavigationPage(new PageTabbed(specialist)));
                    }
                }
            }
            else
            {
                if (_nameIsValid && _phoneIsValid)
                {
                    if (entryName.Text.Equals(localUser.Name) && entryPhone.Text.Equals(localUser.Phone))
                    {
                        await DisplayAlert("Результат", "Данные не были изменены", "Ok");
                    }
                    else
                    {
                        if (!entryPhone.Text.Equals(localUser.Phone))
                        {
                            await DisplayAlert("Внимание!", "Номер телефона изменен! Теперь вход будет осуществляться по номеру телефона: " + entryPhone.Text, "Ok");
                        }

                        localUser.Name = entryName.Text;
                        localUser.Phone = entryPhone.Text;

                        JsonContent content = JsonContent.Create(localUser);
                        var response = await apiConfig.client.PutAsync(urlUser, content);
                        string result = await response.Content.ReadAsStringAsync();
                        await DisplayAlert("Результат", result, "Ok");

                        Navigation.RemovePage(this);
                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                        await Navigation.PushModalAsync(new NavigationPage(new PageTabbed(localUser)));
                    }
                }
            }
        }
    }
}