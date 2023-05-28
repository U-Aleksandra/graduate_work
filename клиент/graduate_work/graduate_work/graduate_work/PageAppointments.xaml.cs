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
    public partial class PageAppointments : ContentPage
    {
        private readonly string url = $"https://{apiConfig.url}:7113/api/Users/CreateAppointment";
        private TimeSpan localStartService;
        private TimeSpan localEndService;
        private SelectService localSelectService;
        private User localUser;
        private DateTime localDate;
        public PageAppointments(TimeSpan startService, DateTime date, SelectService selectService, User user)
        {
            InitializeComponent();
            localStartService = startService;
            localDate = date.Date;
            localSelectService = selectService;
            localUser = user;

            labelDateDervice.Text = $"Запись на " + date.ToString("dd-MM-yyyy");
            labelNameService.Text = $"Услуга: {selectService.nameService}";
            if (selectService.StartPrice) labelStartPrice.Text = "Цена от ";
            else labelStartPrice.Text = "Цена: ";
            labelPrice.Text = selectService.Price.ToString();
            labelStartService.Text = $"Начало записи: {startService}";
            localEndService = startService + selectService.ServicesTime + selectService.BreakTime;
            labelEndService.Text = $"Конец записи: {localEndService}";
            labelNameSpecialist.Text = $"Специалист: {selectService.Name}";
            labelAddress.Text = $"Адрес: {selectService.Address}";
        }

        private async void createAppointment_Clicked(object sender, EventArgs e)
        {
            JsonContent content = JsonContent.Create(new Appointments(localDate, localStartService, localEndService, entryComment.Text)
            {
                Specialist = localSelectService.specialist,
                User = localUser
            });
            var response = await apiConfig.client.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();
            await DisplayAlert("Результат", result, "Ok");
            if(response.StatusCode == HttpStatusCode.OK)
            {
                await Navigation.PushModalAsync(new NavigationPage(new PageTabbed(localUser)));
            }
        }
    }
}