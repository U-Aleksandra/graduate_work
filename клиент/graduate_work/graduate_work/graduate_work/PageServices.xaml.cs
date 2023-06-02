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
                        listViewService.ItemsSource = ServicesParse(listServices);
                        BindingContext = this;
                    }
                }
            }
        }

        private List<SelectService> ServicesParse(List<Service> services)
        {
            List<SelectService> selectServices = new List<SelectService>();
            foreach (var service in services)
            {
                selectServices.Add(new SelectService()
                {
                    Id = service.Id,
                    nameService = service.NameService.nameService,
                    Price = service.Price,
                    StartPrice = service.StartPrice,
                    ServicesTime = service.ServicesTime,
                    BreakTime = service.BreakTime,
                    DescriptionService = service.DescriptionService

                });
            }
            return selectServices;
        }

        private async void createServise_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageCreateServise(localUser));
        }

        private async void listViewService_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            SelectService service = e.SelectedItem as SelectService;
            await Navigation.PushAsync(new PageEditService(service, localUser));
        }
    }
}