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
    public partial class PageListAppointments : ContentPage
    {
        private readonly string urlUser = $"https://{apiConfig.url}:7113/api/Users/GetAppointments";
        private readonly string urlSpecialist = $"https://{apiConfig.url}:7113/api/Specialists/GetAppointments";
        private readonly string urlDelete = $"https://{apiConfig.url}:7113/api/Users/DeleteAppointment";
        private int idAppointment;
        private User localUser;
        public PageListAppointments(User user)
        {
            localUser = user;
            InitializeComponent();
            if(user is Specialist specialist)
            {
                var response = apiConfig.client.GetAsync(urlSpecialist + $"?idSpecialist={specialist.Id}").Result;
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<Appointments> listSpecialistAppoinments = response.Content.ReadFromJsonAsync<List<Appointments>>().Result;
                    List<SelectAppoinments> listAppointments = AppointmentsParse(listSpecialistAppoinments);
                    listViewAppoinments.ItemsSource = listAppointments.Where(l => l.DateApointment >= DateTime.Today).OrderBy(l => l.DateApointment);
                }
            }
            else
            {
                var response = apiConfig.client.GetAsync(urlUser + $"?idUser={user.Id}").Result;
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<Appointments> listUserAppoinments = response.Content.ReadFromJsonAsync<List<Appointments>>().Result;
                    List<SelectAppoinments> listAppointments = AppointmentsParse(listUserAppoinments);
                    listViewAppoinments.ItemsSource = listAppointments.Where(l => l.DateApointment >= DateTime.Today).OrderBy(l => l.DateApointment);
                }
            }
        }

        private List<SelectAppoinments> AppointmentsParse(List<Appointments> appointments)
        {
            List<SelectAppoinments> selectAppointments = new List<SelectAppoinments>();
            foreach (var appointment in appointments)
            {
                selectAppointments.Add(new SelectAppoinments()
                {
                    Id = appointment.Id,
                    DateApointment = appointment.DateApointment,
                    StartTime = appointment.StartTime,
                    EndTime = appointment.EndTime,
                    Note = appointment.Note,
                    NameSpecialist = appointment.Specialist.Name,
                    NameUser = appointment.User.Name,
                    Address = appointment.Specialist.Address,
                    UserPhone = appointment.User.Phone,
                    SpecialistPhone = appointment.Specialist.Phone
                });
            }
            return selectAppointments;
        }

        private async void listViewAppoinments_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            SelectAppoinments itemAppoinments = e.SelectedItem as SelectAppoinments;
            idAppointment = itemAppoinments.Id;
        }

        private async void DeleteAppointment_Clicked(object sender, EventArgs e)
        {
            if(idAppointment != 0)
            {
                var response = apiConfig.client.DeleteAsync(urlDelete + $"?idAppointments={idAppointment}").Result;
                string result = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    await DisplayAlert("Результат", result, "Ok");
                    await Navigation.PushModalAsync(new NavigationPage(new PageTabbed(localUser)));
                }
            }
            else await DisplayAlert("Результат", "Выделите запись!", "Ok");
        }
    }
}