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
                    ListView listService = new ListView()
                    {
                        HasUnevenRows = true,
                        ItemsSource = listServices.Select(s => new { s.NameService.nameService, s.Price, s.StartPrice, s.ServicesTime }),
                        ItemTemplate = new DataTemplate(() =>
                        {
                            Label labelNameService = new Label { FontSize = 16, FontFamily = "Roboto", TextColor = Color.FromHex("#5147AC") };
                            labelNameService.SetBinding(Label.TextProperty, "nameService");

                            Label labelPrice = new Label { FontSize = 16, FontFamily = "Roboto", TextColor = Color.FromHex("#5147AC") };
                            labelPrice.SetBinding(Label.TextProperty, "Price");

                            Label labelStartPrice = new Label { FontSize = 16, FontFamily = "Roboto", TextColor = Color.FromHex("#5147AC") };
                            labelStartPrice.SetBinding(Label.TextProperty, "StartPrice");

                            Label labelServiseTime = new Label { FontSize = 16, FontFamily = "Roboto", TextColor = Color.FromHex("#5147AC"), FormattedText = "{0:h:mm tt}" };
                            labelServiseTime.SetBinding(Label.TextProperty, "ServicesTime");

                            Label labelDescription = new Label { FontSize = 16, FontFamily = "Roboto", TextColor = Color.FromHex("#5147AC") };
                            labelDescription.SetBinding(Label.TextProperty, "DescriptionService");

                            return new ViewCell
                            {
                                View = new StackLayout
                                {
                                    Padding = new Thickness(10, 10),
                                    Orientation = StackOrientation.Vertical,
                                    Children = { labelNameService, labelStartPrice, labelPrice, labelServiseTime, labelDescription }
                                }
                            };
                        })
                    };
                    this.Content = new StackLayout { Children = { listService } };
                }
            }
        }

        private async void createServise_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageCreateServise(localUser));
        }
    }
}