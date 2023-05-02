using graduate_work.Droid;
using graduate_work.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace graduate_work
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCreateServise : ContentPage
    {
        private readonly string urlSearchByCategory = $"https://{apiConfig.url}:7113/api/Users/GetServicesByCategory";
        private readonly string urlCreateService = $"https://{apiConfig.url}:7113/api/Specialists/CreateService";
        List<NameService> listNameServices;
        Specialist localSpecialist;
        public PageCreateServise(User user)
        {
            InitializeComponent();
            if(user is Specialist specialist)
            {
                localSpecialist = specialist;
                var response = apiConfig.client.GetAsync(urlSearchByCategory + $"?categoryId={specialist.Category.Id}").Result;
                listNameServices = response.Content.ReadFromJsonAsync<List<NameService>>().Result;
                pickerService.ItemsSource = listNameServices.Select(l => l.nameService).ToList();
            }
        }

        private async void saveServise_Clicked(object sender, EventArgs e)
        {
            decimal price;
            Decimal.TryParse(entryPrice.Text, out price);
            DateTime timeService;
            DateTime.TryParse(timePickerService.Time.ToString(), out timeService);
            DateTime timeBreak;
            DateTime.TryParse(timePickerBreak.Time.ToString(), out timeBreak);
            NameService nameService = listNameServices.FirstOrDefault(l => l.nameService == pickerService.Items[pickerService.SelectedIndex]);

            JsonContent content = JsonContent.Create(new Service(entryNameServise.Text, price, checkBoxPrice.IsChecked, timeService, timeBreak)
            { NameService = nameService,
              Specialist = localSpecialist
            });
            var response = await apiConfig.client.PostAsync(urlCreateService, content);
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