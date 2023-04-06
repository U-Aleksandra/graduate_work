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
    public partial class PageProfile : ContentPage
    {
        public PageProfile(User user)
        {
            InitializeComponent();
            labelName.Text = user.Name;
            labelPhone.Text = user.Phone;
            labelPassword.Text = user.Password;
            labelSpecialist.Text = user.isSpecialist.ToString();
            if(user is Specialist specialist)
            {
                labelCategory.Text = specialist.Category;
            }
        }
    }
}