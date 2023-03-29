﻿using graduate_work.Droid;
using graduate_work.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net;

namespace graduate_work
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageRegistSpecialist : ContentPage
	{
        private readonly string url = $"https://{apiConfig.url}:7113/api/Specialists/Regist";

		private bool _isValidActivity;
		private bool _isValidAddess;

		private User localUser;
        public PageRegistSpecialist (User user)
		{
			InitializeComponent ();
			localUser = user;
		}

        [Obsolete]
        private async void buttonAddSpecial_Clicked(object sender, EventArgs e)
        {
			if(pickerActivity.SelectedIndex == -1)
			{
				errorActivity.Text = "Выберите направление деятельности";
				_isValidActivity = false;
			}
			else
			{
				errorActivity.Text = " ";
				_isValidActivity = true;
            }
			if(((string.IsNullOrEmpty(entryAdress.Text)) ||
			(string.IsNullOrWhiteSpace(entryAdress.Text))))
			{
				errorAdress.Text = "Введите адрес салона";
				_isValidAddess = false;
			}
			else
			{
				errorAdress.Text = " ";
				_isValidAddess = true;
			}

			if(_isValidActivity && _isValidAddess)
			{
				JsonContent content = JsonContent.Create(new Specialist(localUser.Name, localUser.Phone, localUser.Password, localUser.isSpecialist, pickerActivity.Items[pickerActivity.SelectedIndex], entryAdress.Text));
                var response = await apiConfig.client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();
                Specialist specialist = System.Text.Json.JsonSerializer.Deserialize<Specialist>(result);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    await Navigation.PushModalAsync(new NavigationPage(new PageTabbed(specialist)));

                }
                else
                    await DisplayAlert("Результат", result, "Ok");
            }
        }
    }
}