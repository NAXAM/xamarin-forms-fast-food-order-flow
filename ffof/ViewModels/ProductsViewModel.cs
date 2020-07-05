using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Bogus;
using ffof.Models;
using ffof.Views;
using Xamarin.Forms;
using Xamarin.Essentials;
using PropertyChanged;
namespace ffof.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ProductsViewModel
    {
        private bool productShown;
        private bool hasCartItems;

        public BrandModel Brand { get; set; }

        public ObservableCollection<IGrouping<string, ProductModel>> Products { get; set; }

        public ObservableCollection<CategoryModel> Categories { get; set; }

        public double ScreenWidth => DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;

        public bool ProductShown { get; set; }

        public bool HasCartItems { get; set; }

        public ProductModel CurrentProduct { get; set; }

        public ICommand ViewProductDetailCommand { get; set; }

        public ICommand AddToCartCommand { get; set; }

        public ICommand ViewCartCommand { get; set; }

        public ICommand GoBackCommand { get; set; }

        public ProductsViewModel(INavigation navigation, BrandModel brand)
        {
            Brand = brand;

            var categoryFaker = new Faker<CategoryModel>()
                .RuleFor(x => x.Code, o => o.Lorem.Slug(1))
                .RuleFor(x => x.Name, o => o.Commerce.Categories(1)[0])
                .RuleFor(x => x.PictureUrl, o => o.Image.LoremFlickrUrl());
            Categories = new ObservableCollection<CategoryModel>(categoryFaker.Generate(10));

            var productFaker = new Faker<ProductModel>()
                .RuleFor(x => x.Pricing, o => o.Random.Double(10, 200))
                .RuleFor(x => x.Category, o => o.PickRandom(Categories.Select(x => x.Name)))
                .RuleFor(x => x.Name, o => o.Commerce.ProductName())
                .RuleFor(x => x.PictureUrl, o => o.Image.LoremFlickrUrl());

            var items = productFaker.Generate(200);

            Products = new ObservableCollection<IGrouping<string, ProductModel>>(
                items.GroupBy(x => x.Category)
                );

            ViewProductDetailCommand = new Command<ProductModel>(product =>
            {
                CurrentProduct = product;
                ProductShown = true;
                HasCartItems = true;
            });

            AddToCartCommand = new Command<ProductModel>(product =>
            {
                ProductShown = false;
                CurrentProduct = null;
            });

            GoBackCommand = new Command(() =>
            {
                navigation.PopAsync();
            });

            ViewCartCommand = new Command(() =>
            {
                navigation.PushAsync(new CartPage());
            });
        }
    }
}
