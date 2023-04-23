using graduate_work.Droid;
using graduate_work.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace graduate_work
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageServiceByCaterory : ContentPage
    {
        private readonly string urlSearchByCategory = $"https://{apiConfig.url}:7113/api/Users/GetServicesByCategory";
        private readonly string urlSearchByName = $"https://{apiConfig.url}:7113/api/Users/GetServicesByName";
        private List<NameService> localListNameService;
        public PageServiceByCaterory(int categoryId, string nameCategory)
        {
            InitializeComponent();
            this.Title = nameCategory;
            var response = apiConfig.client.GetAsync(urlSearchByCategory + $"?categoryId={categoryId}").Result;
            List<NameService> listNameServices = response.Content.ReadFromJsonAsync<List<NameService>>().Result;
            nameServiceList.ItemsSource = listNameServices.Select(l => l.nameService);
            localListNameService = listNameServices;
        }

        private async void nameServiceList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            string item = e.SelectedItem.ToString(); 
            var nameServices = localListNameService.FirstOrDefault(l => l.nameService == item);
            int nameServicesId = nameServices.Id;
            var response = await apiConfig.client.GetAsync(urlSearchByName + $"?nameServisesId={nameServicesId}");
            List<Service> services = JsonConvert.DeserializeObject<List<Service>>(response.Content.ReadAsStringAsync().Result);
            await Navigation.PushAsync(new PageServiceByName(services));
        }
    }
}