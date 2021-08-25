using MVVMShopForms.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MVVMShopForms.Data
{
    public class Context
    {
        

        private RestService _RestService;

        public Context() => _RestService = new RestService(Constants.ServiceUrlBase);

        public Context(string token) =>
            _RestService = new RestService(Constants.ServiceUrlBase, token);

        public async Task<List<Product>> GetProducts()
            => await _RestService.GetDataAsync<Product>("Products");

        public async Task AddProduct(Product product)
            => await _RestService.PostAsync(product, "Products");

        public async Task UpdateProduct(Product product) 
        {
            if (product.Id != 0)
                await _RestService.PutDataAsync(product, "Products", product.Id); 
        }

        public async Task DeleteProduct(Product product)
        {
            if (product.Id != 0)
                await _RestService.DeleteAsync("Products", product.Id);

        }

    }
}
