using graduate_work.Droid;
using graduate_work.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace graduate_work
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageInput : ContentPage
    {
        private readonly string url = $"https://{apiConfig.url}:7113/api/Users/Login";

        private bool _phoneIsValid;
        private bool _passwordIsValid;
        public PageInput ()
		{
			InitializeComponent ();
			
		}

        [Obsolete]
        private async void buttonInput_Clicked(object sender, EventArgs e)
        {

            if ((string.IsNullOrEmpty(phoneEntry.Text)) ||
			(string.IsNullOrWhiteSpace(phoneEntry.Text)))
			{
				errorPhone.Text = "Введите номер телефона";
                _phoneIsValid = false;
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

            
            if ((string.IsNullOrEmpty(passwordEntry.Text)) ||
			(string.IsNullOrWhiteSpace(passwordEntry.Text)))
			{
				errorPassword.Text = "Введите пароль";
                _passwordIsValid = false;
			}
			else
			{
				errorPassword.Text = " ";
                _passwordIsValid = true;
			}

            if(_phoneIsValid && _passwordIsValid)
            {
                JsonContent content = JsonContent.Create(new User(phoneEntry.Text, passwordEntry.Text));
                var response = await apiConfig.client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();
                User user = System.Text.Json.JsonSerializer.Deserialize<User>(result);

                if (response.StatusCode == HttpStatusCode.OK) 
                {
                    await Navigation.PushModalAsync(new NavigationPage(new PageTabbed(user)));
                }
                else
                    await DisplayAlert("Результат", result, "Ok");
            }
            
        }
    }
}