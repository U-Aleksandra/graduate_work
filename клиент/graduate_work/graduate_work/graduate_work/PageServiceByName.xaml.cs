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
            serviceList.ItemsSource = services.Select(s => new {s.NameService.nameService, s.Price, s.ServicesTime, s.Specialist.Name, s.Specialist.Address});
        }
    }
}