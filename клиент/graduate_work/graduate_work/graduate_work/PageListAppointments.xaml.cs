using graduate_work.Droid;
using graduate_work.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace graduate_work
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageListAppointments : ContentPage
    {
        private readonly string urlUser = $"https://{apiConfig.url}:7113/api/Users/GetAppointments";
        public PageListAppointments(User user)
        {
            InitializeComponent();
            if(user is Specialist specialist)
            {

            }
            else
            {
                var response = apiConfig.client.GetAsync(urlUser + $"?idUser={user.Id}").Result;
                List<Appointments> listUserAppoinments = response.Content.ReadFromJsonAsync<List<Appointments>>().Result;
                listViewAppoinments.ItemsSource = listUserAppoinments.Where(l => l.DateApointment >= DateTime.Today).OrderBy(l => l.DateApointment);
            }

        }
    }
}