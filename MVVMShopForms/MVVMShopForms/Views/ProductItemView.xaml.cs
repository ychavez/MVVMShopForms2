using MVVMShopForms.Models;
using MVVMShopForms.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MVVMShopForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductItemView : ContentPage
    {
        ProductItemViewModel viewModel;
        public ProductItemView(Product product = null)
        {
            InitializeComponent();
            BindingContext = viewModel = new ProductItemViewModel(product) { Navigation = Navigation };
            btnDelete.IsEnabled = product != null;
        }
    }
}