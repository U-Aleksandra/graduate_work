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
        public PageWorkDays(int idSpecialist)
        {
            InitializeComponent();
            var response = apiConfig.client.GetAsync(urlWorkDays + $"?idSpecialist={idSpecialist}").Result;
            if(response.StatusCode == HttpStatusCode.OK)
            {
                List<WorkSchedule> listWorkDays = response.Content.ReadFromJsonAsync<List<WorkSchedule>>().Result;
                if (listWorkDays.Any())
                {
                    listViewWorkDays.ItemsSource = listWorkDays;
                }
            }
        }
    }
}