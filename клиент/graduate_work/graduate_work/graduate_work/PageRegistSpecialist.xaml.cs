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
	public partial class PageRegistSpecialist : ContentPage
	{
		public PageRegistSpecialist ()
		{
			InitializeComponent ();
		}

        private void buttonAddSpecial_Clicked(object sender, EventArgs e)
        {
			if(pickerActivity.SelectedIndex == -1)
			{
				errorActivity.Text = "Выберите направление деятельности";
			}
			else
			{
				errorActivity.Text = " ";

            }
			if(((string.IsNullOrEmpty(entryAdress.Text)) ||
			(string.IsNullOrWhiteSpace(entryAdress.Text))))
			{
				errorAdress.Text = "Введите адрес салона";
			}
			else
			{
				errorAdress.Text = " ";
			}
        }
    }
}