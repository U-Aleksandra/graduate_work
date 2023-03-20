using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net;
using System.Net.Http;
using Newtonsoft;
using Newtonsoft.Json;
using System.Collections.Generic;
using graduate_work.Droid;
using graduate_work.Models;
using System.Text;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace graduate_work
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageRegist : ContentPage
	{
        private readonly string url = $"https://{apiConfig.url}:7113/api/Users/Regist";


        private bool _nameIsValid;
        private bool _phoneIsValid;
        private bool _passwordIsValid;

        public PageRegist ()
		{
			InitializeComponent ();
		}

        private async void buttonRegist_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(entryName.Text) ||
            string.IsNullOrWhiteSpace(entryName.Text))
            {
                errorName.Text = "Введите имя";
                _nameIsValid = false;
            }
            else
            {
                errorName.Text = " ";
                _nameIsValid = true;
            }

            if (string.IsNullOrEmpty(phoneEntry.Text) ||
            string.IsNullOrWhiteSpace(phoneEntry.Text))
            {
                errorPhone.Text = "Введите номер телефона";
            }
            else if (phoneEntry.Text.Length < 16)
            {
                errorPhone.Text = "Неправильный формат";
                _phoneIsValid = false;
            }
            else
            {
                errorPhone.Text = " ";
                _phoneIsValid = true;
            }

            if (string.IsNullOrEmpty(passwordEntry.Text) ||
            string.IsNullOrWhiteSpace(passwordEntry.Text))
            {
                errorPassword.TextColor = Color.Red;
                errorPassword.Text = "Введите пароль";
            }

            if (_nameIsValid && _phoneIsValid && _passwordIsValid)
            {
                JsonContent content = JsonContent.Create(new User(entryName.Text, phoneEntry.Text, passwordEntry.Text, checkBoxSpecial.IsChecked));
                var response = await apiConfig.client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    await DisplayAlert("Результат", result, "Ok");
                    if (checkBoxSpecial.IsChecked)
                        await Navigation.PushAsync(new PageRegistSpecialist());
                    else
                        await Navigation.PushAsync(new PageRegistSpecialist());
                }
                else
                {
                    await DisplayAlert("Результат", result, "Ok");
                }
            }

        }
        private void passwordEntryReturn_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (passwordEntryReturn.Text != passwordEntry.Text && !string.IsNullOrEmpty(passwordEntryReturn.Text))
            {
                errorPassword.TextColor = Color.Red;
                errorPassword.Text = "Пароли не совпадают";
                _passwordIsValid = false;
            }
            else if(!string.IsNullOrEmpty(passwordEntryReturn.Text))
            {
                errorPassword.TextColor = Color.Green;
                errorPassword.Text = "Пароли совпадают";
                _passwordIsValid = true;
            }
        }
    }
}