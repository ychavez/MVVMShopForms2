using MVVMShopForms.Data;
using MVVMShopForms.Models;
using MVVMShopForms.ViewModels.Base;
using MVVMShopForms.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MVVMShopForms.ViewModels
{
    public class ProductsViewModel: BaseViewModel
    {
        private Context _Context;

        public ICommand AddCommand { get; set; }
        public ICommand Refresh { get; set; }
        public ProductsViewModel()
        {
            _Context = new Context();
            LoadProducts();
            AddCommand = new Command(Add);
            Refresh = new Command(LoadProducts);
        }
        private ObservableCollection<Product> _Products = new ObservableCollection<Product>();

        public ObservableCollection<Product> Products
        { get => _Products; set { SetProperty(ref _Products, value); } }

        public async void LoadProducts() 
        {
            IsBusy = true;
            var products = await _Context.GetProducts();
            Products = new ObservableCollection<Product>(products);
            IsBusy = false;
        }
        public void Add() 
        {
            Navigation.PushAsync(new ProductItemView());
        }
    }
}
