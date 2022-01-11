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
	public partial class ResetPassword : ContentPage
	{
		public ResetPassword()
		{
			InitializeComponent ();
            
            wvName.Source = "https://pkdevelop.patkol.com/LeaveOnlineMobile/ResetPassword";
        }
        private async void Webview_Navigating(object sender, WebNavigatingEventArgs e)
        {
            string saveUrl = ""+(e.Url);
            bool containsSearchResult = saveUrl.Contains("ASCOMPLETE");
            bool containsSearchResultBack = saveUrl.Contains("ASBACK");
            if (containsSearchResult == true)
            {
                await DisplayAlert("Alert", "your password is changed", "OK");
                await Navigation.PopModalAsync();
            }
            else if (containsSearchResultBack == true)
            {
                await Navigation.PopModalAsync();
            }
        }
        
    }
}