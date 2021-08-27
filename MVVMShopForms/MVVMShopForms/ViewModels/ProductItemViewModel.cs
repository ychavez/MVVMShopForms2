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
    public class ProductItemViewModel: BaseViewModel
    {
        private Context _Context;
        public Product Product { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ProductItemViewModel(Product product = null)
        {
            IsBusy = true;  
            Product = product ?? new Product();
            SaveCommand = new Command(Save);
            DeleteCommand = new Command(Delete);
            _Context = new Context();
            IsBusy = false;
        }
        private async void Save() 
        {
            IsBusy = true;
            if (Product.Id == 0)
                await _Context.AddProduct(Product);
            else
                await _Context.UpdateProduct(Product);
            IsBusy = false;

            await Navigation.PopAsync();
        }

        private async void Delete()
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Atencion",
                "Estas seguro que quieres borrar este articulo", "Yes", "No");
           
            if (!answer)
                return;
           
            
            IsBusy = true;
            await _Context.DeleteProduct(Product);
            await Navigation.PopAsync();
            IsBusy = false;
        }

    }
}
