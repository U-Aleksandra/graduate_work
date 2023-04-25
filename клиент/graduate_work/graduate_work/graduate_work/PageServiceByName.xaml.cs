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
            ListView listService = new ListView()
            {
                HasUnevenRows = true,
                ItemsSource = services.Select(s => new { s.NameService.nameService, s.Price, s.StartPrice, s.Specialist.Name, s.Specialist.Address }),
                ItemTemplate = new DataTemplate(() =>
                {
                    Label labelNameService = new Label { FontSize = 16, FontFamily = "Roboto", TextColor = Color.FromHex("#5147AC") };
                    labelNameService.SetBinding(Label.TextProperty, "nameService");

                    Label labelPrice = new Label { FontSize = 16, FontFamily = "Roboto", TextColor = Color.FromHex("#5147AC") };
                    labelPrice.SetBinding(Label.TextProperty, "Price");

                    Label labelNameSpecialist = new Label { FontSize = 16, FontFamily = "Roboto", TextColor = Color.FromHex("#5147AC") };
                    labelNameSpecialist.SetBinding(Label.TextProperty, "Name");

                    Label labelAddress = new Label { FontSize = 16, FontFamily = "Roboto", TextColor = Color.FromHex("#5147AC") };
                    labelAddress.SetBinding(Label.TextProperty, "Address");

                    Label labelStartPrice = new Label { FontSize = 16, FontFamily = "Roboto", TextColor = Color.FromHex("#5147AC") };
                    labelStartPrice.SetBinding(Label.TextProperty, "StartPrice");

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(10, 10),
                            Orientation = StackOrientation.Vertical,
                            Children = { labelNameService, labelStartPrice, labelPrice, labelNameSpecialist, labelAddress }
                        }
                    };
                })
            };
            this.Content = new StackLayout { Children = { listService } };
        }
    }
}