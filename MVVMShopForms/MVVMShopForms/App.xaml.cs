using MVVMShopForms.Data;
using MVVMShopForms.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MVVMShopForms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (Current.Properties.ContainsKey("token"))
            {
                string token = Current.Properties["token"].ToString();
                var _context = new Context();
                if (_context.CheckToken(token))
                {
                    Constants.ServiceToken = token;
                    MainPage = new MainPage();
                    return;
                }


            }
            MainPage = new NavigationPage(new Login());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
