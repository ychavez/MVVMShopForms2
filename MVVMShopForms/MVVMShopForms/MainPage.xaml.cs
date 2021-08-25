using MVVMShopForms.Models;
using MVVMShopForms.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MVVMShopForms
{
    public partial class MainPage : MasterDetailPage
    {

        public List<MainMenuItem> menuList { get; set; }
        public MainPage()
        {
            InitializeComponent();
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ProductsView)));
            menuList = new List<MainMenuItem>();
            menuList.Add(new MainMenuItem()
            {
                Title = "Productos",
                Icon = "logo_aureapng"

            });
            menuList.Add(new MainMenuItem()
            {
                Title = "Contacto",
                Icon = "logo_aureapng"

            });

            navigationDrawerList.ItemsSource = menuList;

        }
    }
}
