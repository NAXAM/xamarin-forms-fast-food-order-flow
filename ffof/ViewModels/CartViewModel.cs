using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Bogus;
using ffof.Models;
using Xamarin.Forms;
using ffof.Views;
namespace ffof.ViewModels
{
    public class CartViewModel
    {
        public ObservableCollection<ProductModel> Products { get; set; }

        public ObservableCollection<ProductModel> RelevantProducts { get; set; }

        public string TotalPrice
        {
            get
            {
                int result = 0;
                foreach (var product in Products)
                {
                    result += (int)product.Pricing;
                }
                return String.Format("{0}k", result);
            }
        }

        public ICommand GoBackCommand { get; set; }

        public ICommand OrderCommand { get; set; }

        public CartViewModel(INavigation navigation)
        {
            var productFaker = new Faker<ProductModel>()
                   .RuleFor(x => x.Pricing, o => o.Random.Double(10, 200))
                   .RuleFor(x => x.Name, o => o.Commerce.ProductName())
                   .RuleFor(x => x.PictureUrl, o => o.Image.PicsumUrl());

            var items = productFaker.Generate(5);
            Products = new ObservableCollection<ProductModel>(items);

            items = productFaker.Generate(10);
            RelevantProducts = new ObservableCollection<ProductModel>(items);

            GoBackCommand = new Command(() =>
            {
                navigation.PopAsync();
            });

            OrderCommand = new Command(() =>
            {
                navigation.PushModalAsync(new FinishPage());
            });
        }
    }
}
