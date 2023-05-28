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
    public partial class PageWorkDays : ContentPage
    {
        private readonly string urlWorkDays = $"https://{apiConfig.url}:7113/api/Users/GetWorkDays";
        private readonly string urlFreeTime = $"https://{apiConfig.url}:7113/api/Users/GetOfFreeTime";
        private SelectService localSelectService;
        private User localUser;
        public PageWorkDays(SelectService selectService, User user)
        {
            InitializeComponent();
            localSelectService = selectService;
            localUser = user;
            var response = apiConfig.client.GetAsync(urlWorkDays + $"?idSpecialist={selectService.specialist.Id}").Result;
            if(response.StatusCode == HttpStatusCode.OK)
            {
                List<WorkSchedule> listWorkDays = response.Content.ReadFromJsonAsync<List<WorkSchedule>>().Result;
                if (listWorkDays.Any())
                {
                    listViewWorkDays.ItemsSource = listWorkDays.Select(l => l.Date);
                }
            }
        }

        private async void listViewWorkDays_SelectedItem(object sender, SelectedItemChangedEventArgs e)
        {
            string dateString = e.SelectedItem.ToString();
            DateTime date = DateTime.Parse(dateString);
            var response = apiConfig.client.GetAsync(urlFreeTime + $"?dateWork={date.Date}&idSpecialist={localSelectService.specialist.Id}" +
                $"&serviceTime={localSelectService.ServicesTime}&serviceBreak={localSelectService.BreakTime}").Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                List<TimeSpan> listFreeTime = response.Content.ReadFromJsonAsync<List<TimeSpan>>().Result;
                await Navigation.PushAsync(new PageFreeTime(listFreeTime, date, localSelectService, localUser));
            }
        }
    }
}