using MVVMShopForms.Data;
using MVVMShopForms.Models;
using MVVMShopForms.ViewModels.Base;
using System.Collections.ObjectModel;


namespace MVVMShopForms.ViewModels
{
    public class ProductsViewModel: BaseViewModel
    {
        private Context _Context;
        public ProductsViewModel()
        {
            _Context = new Context();
            LoadProducts();
        }
        private ObservableCollection<Product> _Products = new ObservableCollection<Product>();

        public ObservableCollection<Product> Products
        { get => _Products; set { SetProperty(ref _Products, value); } }

        public async void LoadProducts() 
        {
            var products = await _Context.GetProducts();
            Products = new ObservableCollection<Product>(products);
            
        }
    }
}
