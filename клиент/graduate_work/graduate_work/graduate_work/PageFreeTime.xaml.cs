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
    public partial class PageFreeTime : ContentPage
    {
        private User localUser;
        private DateTime localDate;
        private SelectService localSelectService;
        public PageFreeTime(List<TimeSpan> listFreeTime, DateTime date, SelectService selectService, User user)
        {
            InitializeComponent();
            localUser = user;
            localDate = date;
            localSelectService = selectService;
            if (listFreeTime.Any())
            {
                listViewFreeTime.ItemsSource = listFreeTime;
            }
        }

        private async void listViewFreeTime_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            string timeString = e.SelectedItem.ToString();
            TimeSpan timeStartService = TimeSpan.Parse(timeString);
            await Navigation.PushAsync(new PageAppointments(timeStartService, localDate, localSelectService,localUser));
        }
    }
}