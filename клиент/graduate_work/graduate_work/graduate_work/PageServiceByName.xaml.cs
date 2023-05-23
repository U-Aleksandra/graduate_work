using graduate_work.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace graduate_work
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageServiceByName : ContentPage
    {
        public PageServiceByName(List<Service> services)
        {
            InitializeComponent();
            if (services.Any())
            {
                List<SelectService> localListService = ServicesParse(services);
                listViewService.ItemsSource = localListService;
                BindingContext = this;
            }
        }
        private async void listViewService_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            SelectService itemService = e.SelectedItem as SelectService;
            if (itemService != null)
            {
                await Navigation.PushAsync(new PageWorkDays(itemService.Id));
            }
        }
        private List<SelectService> ServicesParse(List<Service> services)
        {
            List<SelectService> selectServices = new List<SelectService>();
            foreach (var service in services)
            {
                selectServices.Add(new SelectService()
                {
                    Id = service.Specialist.Id,
                    nameService = service.NameService.nameService,
                    Price = service.Price,
                    StartPrice = service.StartPrice,
                    ServicesTime = service.ServicesTime,
                    BreakTime = service.BreakTime,
                    Name = service.Specialist.Name,
                    Address = service.Specialist.Address

                });
            }
            return selectServices;
        }
    }

}