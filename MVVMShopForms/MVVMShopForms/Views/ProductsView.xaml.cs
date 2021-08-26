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
    public partial class ProductsView : ContentPage
    {
        private ProductsViewModel viewModel;
        public ProductsView()
        {
            InitializeComponent();
            BindingContext = viewModel = new ProductsViewModel() { Navigation = Navigation };
            
        }
    }
}