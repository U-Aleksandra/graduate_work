using graduate_work.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace graduate_work
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageTabbed : Xamarin.Forms.TabbedPage
    {
        [Obsolete]
        public PageTabbed(User user)
        {
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            Children.Add(new PageHome(user));
            Children.Add(new PageProfile(user));

            if(user is Specialist specialist)
            {
                Children.Add(new PageServices(specialist));
            }
        }
    }
}