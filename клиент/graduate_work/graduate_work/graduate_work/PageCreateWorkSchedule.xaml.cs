using graduate_work.Droid;
using graduate_work.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace graduate_work
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCreateWorkSchedule : ContentPage
    {
        private readonly string urlCreateWorkSchedule = $"https://{apiConfig.url}:7113/api/Specialists/CreateWorkSchedule";
        Specialist localSpecialist;
        bool _isValidDate;
        bool _isValidWork;
        bool _isValidBreak;
        public PageCreateWorkSchedule(User user)
        {
            InitializeComponent();
            if(user is Specialist specialist) 
            { 
                localSpecialist = specialist;
            }
        }
        private async void saveWorkSchedule_Clicked(object sender, EventArgs e)
        {
            if (datePiker.Date >= DateTime.Today)
            {
                _isValidDate = true;
            }
            else
            {
                await DisplayAlert("Внимание!", "Выбранная дата уже прошла", "Ok");
                _isValidDate = false;
            }

            if (timePickerStartWork.Time < timePickerEndWork.Time)
            {
                _isValidWork = true;
                errorBreak.Text = string.Empty;
            }
            else
            {
                _isValidWork = false;
                errorBreak.Text = "Неправильно составлен график";
            }

            if (timePickerStartBreak.Time < timePickerEndBreak.Time && timePickerStartBreak.Time < timePickerEndWork.Time &&
                timePickerStartBreak.Time > timePickerStartWork.Time && timePickerEndBreak.Time < timePickerEndWork.Time &&
                timePickerEndBreak.Time > timePickerStartWork.Time)
            {
                _isValidBreak = true;
                errorBreak.Text = string.Empty;
            }
            else
            {
                _isValidBreak = false;
                errorBreak.Text = "Неправильно составлен график";
            }

            if (_isValidDate && _isValidWork && _isValidBreak)
            {
                JsonContent content = JsonContent.Create(new WorkSchedule(datePiker.Date, timePickerStartWork.Time, timePickerEndWork.Time,
               timePickerStartBreak.Time, timePickerEndBreak.Time)
                {
                    Specialist = localSpecialist
                });
                var response = await apiConfig.client.PostAsync(urlCreateWorkSchedule, content);
                string result = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    await DisplayAlert("Результат", result, "Ok");
                    await Navigation.PopAsync();
                }
                else
                    await DisplayAlert("Результат", result, "Ok");
            }
        }
    }
}