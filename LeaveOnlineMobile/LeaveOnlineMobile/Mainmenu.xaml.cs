using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LeaveOnlineMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Mainmenu : MasterDetailPage
    {
        public Mainmenu(string Empnum, string url, string Language)
        {
            InitializeComponent();
            this.Master = new Master("" + Empnum, "" + Language);
            this.Detail = new NavigationPage(new wvPage("" + Empnum, "" + url, "" + Language));
        }
    }
}