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
    public partial class PageListSpecialist : ContentPage
    {
        public PageListSpecialist(List<Specialist> listSpecialist)
        {
            InitializeComponent();
            listViewSpecialist.ItemsSource = listSpecialist;
        }
    }
}