using MVVMShopForms.Data;
using MVVMShopForms.Models;
using MVVMShopForms.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MVVMShopForms.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private Context _Context;

        public Login User { get; set; }

        public ICommand Login { get; set; }

        public LoginViewModel()
        {
            User = new Login();
            _Context = new Context();
            Login = new Command(login);
        }

        private async void login()
        {
            IsBusy = true;
            string token = await _Context.Login(User);
            if (token == "")
            {
                await Application.Current.MainPage.DisplayAlert("Error al iniciar",
                    "El usuario o la contraseña no son validos ", "Ok");
                User = new Login();
                IsBusy = false;
                return;
            }

            Application.Current.Properties["token"] = token;
            Constants.ServiceToken = token;
            await Application.Current.SavePropertiesAsync();
            Application.Current.MainPage = new MainPage();
            IsBusy = false;

        }
    }
}
