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
                    ListView listViewWorkSchedule = new ListView()
                    {
                        HasUnevenRows = true,
                        ItemsSource = listWorkSchedule,
                        ItemTemplate = new DataTemplate(() =>
                        {
                            Label labelDate = new Label { FontSize = 16, FontFamily = "Roboto", TextColor = Color.FromHex("#5147AC"), Text = "Дата" };
                            Label date = new Label { FontSize = 16, FontFamily = "Roboto", TextColor = Color.Black };
                            date.SetBinding(Label.TextProperty, "Date");

                            Label labelStartWork = new Label { FontSize = 16, FontFamily = "Roboto", TextColor = Color.FromHex("#5147AC"), Text = "Начало рабочего дня" };
                            Label startWork = new Label { FontSize = 16, FontFamily = "Roboto", TextColor = Color.Black };
                            startWork.SetBinding(Label.TextProperty, "StartWork");

                            Label labelEndWork = new Label { FontSize = 16, FontFamily = "Roboto", TextColor = Color.FromHex("#5147AC"), Text = "Конец рабочего дня" };
                            Label endWork = new Label { FontSize = 16, FontFamily = "Roboto", TextColor = Color.Black };
                            endWork.SetBinding(Label.TextProperty, "EndWork");

                            Label labelStartBreak = new Label { FontSize = 16, FontFamily = "Roboto", TextColor = Color.FromHex("#5147AC"), Text = "Начало перерыва" };
                            Label startBreak = new Label { FontSize = 16, FontFamily = "Roboto", TextColor = Color.Black };
                            startBreak.SetBinding(Label.TextProperty, "StartBreak");

                            Label labelEndBreak = new Label { FontSize = 16, FontFamily = "Roboto", TextColor = Color.FromHex("#5147AC"), Text = "Конец перерыва" };
                            Label endBreak = new Label { FontSize = 16, FontFamily = "Roboto", TextColor = Color.Black };
                            endBreak.SetBinding(Label.TextProperty, "EndBreak");

                            return new ViewCell
                            {
                                View = new StackLayout
                                {
                                    Padding = new Thickness(10, 10),
                                    Orientation = StackOrientation.Vertical,
                                    Children = {
                                        new StackLayout
                                        {
                                            Orientation= StackOrientation.Horizontal,
                                            Children = { labelDate, date}
                                        },
                                        new StackLayout
                                        {
                                            Orientation= StackOrientation.Horizontal,
                                            Children = { labelStartWork, startWork }
                                        },
                                        new StackLayout
                                        {
                                            Orientation= StackOrientation.Horizontal,
                                            Children = { labelEndWork, endWork }
                                        },
                                        new StackLayout
                                        {
                                            Orientation= StackOrientation.Horizontal,
                                            Children = { labelStartBreak, startBreak }
                                        },
                                        new StackLayout
                                        {
                                            Orientation= StackOrientation.Horizontal,
                                            Children = { labelEndBreak, endBreak }
                                        }
                                    }
                                }
                            };
                        })
                    };
                    ScrollView scrollView = new ScrollView();
                    scrollView.Content = new StackLayout { Children = { listViewWorkSchedule } };
                    this.Content = scrollView;
                }
            }
        }

        private async void createWorkSchedule_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageCreateWorkSchedule(localUser));
        }
    }
}