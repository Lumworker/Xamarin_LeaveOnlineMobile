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
    public partial class Master : ContentPage
    {
        string emp = "";
        string lg = "";
        //string Language = "";
        public Master(string emp_num, string Language)
        {
            InitializeComponent();
            this.emp = emp_num;
            this.lg = Language;
            if (this.lg == "&lg=TH")
            {
                MyTask.Text = "งานของฉัน";
                History.Text = "ประวัติ/สิทธิ์ การลา";
                Request.Text = "การขออนุมัติใบลา";
                CancelRequest.Text = "ยกเลิกใบลา";
                LogOut.Text = "ออกจากระบบ";
            }
        }

        private void Button_History(object sender, EventArgs e)
        {

            Navigation.PopModalAsync();
            Navigation.PushModalAsync(new Mainmenu(emp, "https://pkdevelop.patkol.com/LeaveOnlineMobile/LeaveTranfer.aspx?emp=", lg));
        }

        private void Button_Request(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
            Navigation.PushModalAsync(new Mainmenu(emp, "https://pkdevelop.patkol.com/LeaveOnlineMobile/LeaveForm.aspx?emp=", lg));
        }

        private void Button_MyTask(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
            Navigation.PushModalAsync(new Mainmenu(emp, "https://pkdevelop.patkol.com/LeaveOnlineMobile/MyTask.aspx?emp=", lg));
        }

        private void Button_Cancel(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
            Navigation.PushModalAsync(new Mainmenu(emp, "https://pkdevelop.patkol.com/LeaveOnlineMobile/CancelLeaveForm.aspx?emp=", lg));
        }
        private void Button_Logout(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        //private void Close_Modal(object sender, EventArgs e)
        //{
        //    Navigation.PopModalAsync();
        //}
    }
}