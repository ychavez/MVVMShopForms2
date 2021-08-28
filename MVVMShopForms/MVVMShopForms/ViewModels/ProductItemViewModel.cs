using MVVMShopForms.Data;
using MVVMShopForms.Models;
using MVVMShopForms.ViewModels.Base;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MVVMShopForms.ViewModels
{
    public class ProductItemViewModel: BaseViewModel
    {
        private Context _Context;
        private ImageSource _ImgSource;


        public Product Product { get; set;  }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ICommand PhotoFromFile { get; set; }
        public ICommand UploadPhoto { get; set; }


        public ImageSource ImgSource { get => _ImgSource; set => SetProperty(ref _ImgSource, value); }

      


        public ProductItemViewModel(Product product = null)
        {
            IsBusy = true;  
            Product = product ?? new Product();
            SaveCommand = new Command(Save);
            DeleteCommand = new Command(Delete);
            _Context = new Context(Constants.ServiceToken);
            UploadPhoto = new Command(TakePhoto);
            PhotoFromFile = new Command(TakeFromFile);


            if (Product?.Picture != null)
                ImgSource = ImageSource.FromStream(() => new MemoryStream(product.Picture));
  
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

        private async void TakePhoto() 
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable ||
                !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Camara no disponible", "La camara no esta disponible", "Ok");
                return;
            }

            var file = await CrossMedia.Current.
                TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Shop",
                    Name = $"{Guid.NewGuid().ToString()}.jpg",
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                    SaveToAlbum = true,
                    SaveMetaData = false
                });

            ImgSource = ImageSource.FromStream(() =>
            {
                using (var memorystream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memorystream);
                    Product.Picture = memorystream.ToArray();
                }
                var stream = file.GetStream();
                return stream;

            });

            
        
        }
        public async void TakeFromFile() 
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.
                    DisplayAlert("Subir fotos no soportado", "Subir fotos no soportado", "Ok");
                return;
            }
            var file = await CrossMedia.Current.
                PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
                });
            if (file == null)
            {
                return;
            }

            ImgSource = ImageSource.FromStream(() =>
            {
                using (var memorystream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memorystream);
                    Product.Picture = memorystream.ToArray();
                }
                var stream = file.GetStream();
                return stream;

            });


        }

    }
}
