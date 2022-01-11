using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections;

namespace LeaveOnlineMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class wvPage : ContentPage
    {
        public string EmpployeeNumber = "", K2ID = "", filename = "";


        public wvPage(string Empnum, string url, string Language)
        {
            InitializeComponent();
            wvName.Source = "" + url + Empnum + Language;
        }

        private async void btnBack_Clicked(object sender, EventArgs e)
        {
            string url = "https://pkdevelop.patkol.com/LeaveOnline/" + filename + ".aspx?emp=" + EmpployeeNumber + "&ID=" + K2ID + "";
            await DisplayAlert(" ted", url, "ok");
        }

        private void Webview_Navigating(object sender, WebNavigatingEventArgs e)
        {
            string saveUrl = "" + (e.Url);
            //await DisplayAlert(" ting", "" + saveUrl, "ok");
            if (saveUrl.Contains("ID=") && saveUrl.Contains("filename="))
            {
                var parameter = getparameter(saveUrl);
                EmpployeeNumber = parameter["emp"];
                K2ID = parameter["ID"];
                filename = parameter["filename"];

                //if (Device.RuntimePlatform == Device.Android)
                //{
                //await Browser.OpenAsync("https://patkoldevelop.patkol.com/LeaveOnlineMobile/previewPDF.aspx?emp=" + EmpployeeNumber + "&ID=" + K2ID + "&filename=" + filename, BrowserLaunchMode.SystemPreferred);
                //}
                Device.OpenUri(new Uri("https://pkdevelop.patkol.com/LeaveOnlineMobile/previewPDF.aspx?emp=" + EmpployeeNumber + "&ID=" + K2ID + "&filename=" + filename));
                
            }


        }
        public Dictionary<string, string> getparameter(string url)
        {
            var dictionary = new Dictionary<string, string>();
            var first = url.Split('?');
            var decode = first[1].Split('&');
            string[] parametername;

            for (int i = 0; i < decode.Length; i++)
            {
                parametername = decode[i].Split('=');
                dictionary.Add(parametername[0], parametername[1]);
            }

            return dictionary;

        }
    }
}