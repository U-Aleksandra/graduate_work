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
    public partial class PageWorkSchedule : ContentPage
    {
        private readonly string urlWorkSchedule = $"https://{apiConfig.url}:7113/api/Specialists/GetWorkShedule";
        User localUser;
        public PageWorkSchedule(User user)
        {
            InitializeComponent();
            localUser = user;
            if (user is Specialist specialist)
            {
                var response = apiConfig.client.GetAsync(urlWorkSchedule + $"?specialistId={specialist.Id}").Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    List<WorkSchedule> listWorkSchedule = response.Content.ReadFromJsonAsync<List<WorkSchedule>>().Result;
                    if (listWorkSchedule.Any())
                    {
                        listViewWorkSchedule.ItemsSource = listWorkSchedule;
                        BindingContext = this;
                    }
                }
            }
        }

        private async void createWorkSchedule_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageCreateWorkSchedule(localUser));
        }
    }
}