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
    public partial class PageCreateServise : ContentPage
    {
        private readonly string urlSearchByCategory = $"https://{apiConfig.url}:7113/api/Users/GetServicesByCategory";
        public PageCreateServise(User user)
        {
            InitializeComponent();
            if(user is Specialist specialist)
            {
                var response = apiConfig.client.GetAsync(urlSearchByCategory + $"?categoryId={specialist.Category.Id}").Result;
                List<NameService> listNameServices = response.Content.ReadFromJsonAsync<List<NameService>>().Result;
                pickerService.ItemsSource = listNameServices.Select(l => l.nameService).ToList();
            }
        }
    }
}