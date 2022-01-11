using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;

namespace LeaveOnlineMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        //public async void Button_Clicked(object sender, EventArgs e)
        //{
        //    var emp = EmpNum.Text;
        //    var pass = Password.Text;
        //    if (!String.IsNullOrEmpty(emp) && !String.IsNullOrEmpty(pass))
        //    {
        //        var postData = new List<KeyValuePair<string, string>>();
        //        postData.Add(new KeyValuePair<string, string>("name", emp));
        //        postData.Add(new KeyValuePair<string, string>("password", pass));

        //        var content = new FormUrlEncodedContent(postData);
        //        var client = new HttpClient();
        //        client.BaseAddress = new Uri("https://pkdevelop.patkol.com/API_loginApp");
        //        client.DefaultRequestHeaders.Add("Accept", "application/json");
        //        var response = await client.PostAsync("https://pkdevelop.patkol.com/API_loginApp/index.php", content);
        //        var Empnum = response.Content.ReadAsStringAsync().Result;

        //        if (!string.IsNullOrWhiteSpace(Empnum))
        //        {
        //            //await Navigation.PushModalAsync(new wvPage(("" + Empnum)));
        //            await Navigation.PushModalAsync(new Mainmenu("" + Empnum, "https://pkdevelop.patkol.com/LeaveOnlineMobile/LeaveTranfer.aspx?emp="));
        //        }
        //        else
        //        {
        //            await DisplayAlert("Something wrong", "Please check employee number and password", "Ok");
        //        }
        //    }
        //    else
        //    {
        //        await DisplayAlert("Something wrong", "Please insert employee number and password", "Ok");
        //    }

        //}
        public async void Button_Clicked(object sender, EventArgs e)
        {
            var emp = EmpNum.Text;
            var pass = Password.Text;
            var token = "";
            var deviceSerial = "";
            if (Device.RuntimePlatform == Device.Android)
            {
                InterfaceToken tokenAndroid = DependencyService.Get<InterfaceToken>(DependencyFetchTarget.GlobalInstance);
                InterfaceSerial device = DependencyService.Get<InterfaceSerial>();
                token = tokenAndroid.getToken();
                deviceSerial = device.GetSerial();
            } else if (Device.RuntimePlatform == Device.iOS)
            {
                //iOS stuff
            }

            if (!String.IsNullOrEmpty(emp) && !String.IsNullOrEmpty(pass))
            {
                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("name", emp));
                postData.Add(new KeyValuePair<string, string>("password", pass));
                postData.Add(new KeyValuePair<string, string>("serial", deviceSerial));
                postData.Add(new KeyValuePair<string, string>("token", token));

                var content = new FormUrlEncodedContent(postData);
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://pkdevelop.patkol.com/API_loginApp");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = await client.PostAsync("https://pkdevelop.patkol.com/API_loginApp/index.php", content);
                var Empnum = response.Content.ReadAsStringAsync().Result;

                if (!string.IsNullOrWhiteSpace(Empnum))
                {
                    string Language = await DisplayActionSheet("Select a Language / เลือกภาษา", "Cancel", null, "ไทย", "English");
                    if (!String.IsNullOrEmpty(Language))
                    {
                        if (Language == "ไทย")
                        {
                            Language = "&lg=TH";
                            await Navigation.PushModalAsync(new Mainmenu("" + Empnum, "https://pkdevelop.patkol.com/LeaveOnlineMobile/LeaveTranfer.aspx?emp=", Language));
                        }
                        else if (Language == "English")
                        {
                            Language = "&lg=EN";
                            await Navigation.PushModalAsync(new Mainmenu("" + Empnum, "https://pkdevelop.patkol.com/LeaveOnlineMobile/LeaveTranfer.aspx?emp=", Language));
                        }

                    }
                    //await Navigation.PushModalAsync(new wvPage(("" + Empnum)));
                }
                else
                {
                    await DisplayAlert("Something wrong", "Please check employee number and password", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Something wrong", "Please insert employee number and password", "Ok");
            }
        }
        private void Resetpassword(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ResetPassword());
        }

        //private async void Button_Clicked_1(object sender, EventArgs e)
        //{
        //    InterfaceToken tokenAndroid = DependencyService.Get<InterfaceToken>(DependencyFetchTarget.GlobalInstance);
        //    InterfaceSerial device = DependencyService.Get<InterfaceSerial>();
        //    var result = tokenAndroid.getToken();
        //    string deviceIdentifier = device.GetSerial();
        //    await DisplayAlert("Alert "+deviceIdentifier, "The result is = " + result, "OK");


        //}
    }

}