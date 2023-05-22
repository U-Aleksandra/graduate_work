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
    public partial class PageServices : ContentPage
    {
        private readonly string urlServiceBySpecialist = $"https://{apiConfig.url}:7113/api/Users/GetServicesBySpecialist";
        User localUser;
        public PageServices(User user)
        {
            InitializeComponent();
            localUser = user;
            if(user is Specialist specialist)
            {
                var response = apiConfig.client.GetAsync(urlServiceBySpecialist + $"?idSpecialist={specialist.Id}").Result;
                if(response.StatusCode == HttpStatusCode.OK)
                {
                    List<Service> listServices = response.Content.ReadFromJsonAsync<List<Service>>().Result;
                    if (listServices.Any())
                    {
                        listViewService.ItemsSource = listServices.Select(s => new { s.NameService.nameService, s.Price, s.StartPrice, s.ServicesTime, s.DescriptionService });
                        BindingContext = this;
                    }
                }
            }
        }

        private async void createServise_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageCreateServise(localUser));
        }
    }
}