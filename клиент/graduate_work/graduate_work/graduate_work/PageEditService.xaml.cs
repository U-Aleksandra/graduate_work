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
    public partial class PageEditService : ContentPage
    {
        private readonly string urlEditService = $"https://{apiConfig.url}:7113/api/Specialists/PutService";

        bool _isValidPrice;
        bool _isValidTimeService;
        private Service service;
        private User localUser;
        public PageEditService(SelectService selectService, User user)
        {
            InitializeComponent();
            localUser = user;
            entryNameServise.Text = selectService.DescriptionService;
            entryPrice.Text = selectService.Price.ToString();
            checkBoxPrice.IsChecked = selectService.StartPrice;
            timePickerService.Time = selectService.ServicesTime;
            timePickerBreak.Time = selectService.BreakTime;
            service = new Service(selectService.Id,selectService.DescriptionService, selectService.Price, selectService.StartPrice,
                selectService.ServicesTime, selectService.BreakTime);
        }

        private async void SaveService_Clicked(object sender, EventArgs e)
        {
            if (((string.IsNullOrEmpty(entryPrice.Text)) ||
            (string.IsNullOrWhiteSpace(entryPrice.Text))))
            {
                errorPrice.Text = "Введите стоимость услуги";
                _isValidPrice = false;
            }
            else
            {
                errorPrice.Text = string.Empty;
                _isValidPrice = true;
            }
            if (timePickerService.Time.ToString() == "00:00:00")
            {
                errorTimeService.Text = "Введите продолжительность услуги";
                _isValidTimeService = false;
            }
            else
            {
                errorTimeService.Text = string.Empty;
                _isValidTimeService = true;
            }
            if ( _isValidPrice && _isValidTimeService)
            {
                decimal price;
                Decimal.TryParse(entryPrice.Text, out price);

                service.DescriptionService = entryNameServise.Text;
                service.Price = price;
                service.StartPrice = checkBoxPrice.IsChecked;
                service.ServicesTime = timePickerService.Time;
                service.BreakTime = timePickerBreak.Time;

                JsonContent content = JsonContent.Create(service);
                var response = await apiConfig.client.PutAsync(urlEditService, content);
                string result = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    await DisplayAlert("Результат", result, "Ok");
                    await Navigation.PushModalAsync(new NavigationPage(new PageTabbed(localUser)));
                }
                else
                    await DisplayAlert("Результат", result, "Ok");
            }
        }
    }
}