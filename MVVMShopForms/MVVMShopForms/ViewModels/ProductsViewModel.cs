using MVVMShopForms.Data;
using MVVMShopForms.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVVMShopForms.ViewModels
{
    public class ProductsViewModel
    {
        private Context _Context;
        public ProductsViewModel()
        {
            _Context = new Context();
            LoadProducts();
        }
        public List<Product> Products = new List<Product> { new Product { Description = "Prueba" } };

        public async void LoadProducts() 
        {
            Products = new List<Product>();
            Products.AddRange(await _Context.GetProducts());
        }
    }
}
