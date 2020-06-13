using System.Collections.ObjectModel;
using System.Windows.Input;
using Bogus.DataSets;
using ffof.Models;
using ffof.Views;
using Xamarin.Forms;

namespace ffof.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<BrandModel> Brands { get; set; }

        public ICommand ViewProductsCommand { get; set; }

        public MainViewModel(INavigation navigation)
        {
            var fake = new Bogus.Faker<BrandModel>()
                .RuleFor(x => x.ColorCode, o => o.PickRandom(
                    new[] {
                        Color.AliceBlue.ToHex(),
                        Color.Aqua.ToHex(),
                        Color.BlueViolet.ToHex(),
                        Color.Chocolate.ToHex(),
                        Color.Cornsilk.ToHex(),
                        Color.DarkGreen.ToHex(),
                        Color.DarkOrange.ToHex(),
                        Color.DeepPink.ToHex(),
                        Color.Indigo.ToHex(),
                        }
                    ))
                .RuleFor(x => x.HighlightUrl, o => o.Image.PicsumUrl())
                .RuleFor(x => x.LogoUrl, o => o.Image.PicsumUrl())
                .RuleFor(x => x.LongestWaitingTime, o => o.Random.Int(15, 60))
                .RuleFor(x => x.Name, o => o.Company.CompanyName())
                .RuleFor(x => x.Rating, o => o.Random.Double(0, 1))
                .RuleFor(x => x.Remark, o => o.Lorem.Slug())
                .RuleFor(x => x.ShortestWaitingTime, o => o.Random.Int(3, 12))
                .RuleFor(x => x.PricingCategory, o => o.Random.CollectionItem(new[] { "$", "$$", "$$$", "$$$" }));

            var items = fake.Generate(100);
            Brands = new ObservableCollection<BrandModel>(items);

            ViewProductsCommand = new Command<BrandModel>(brand =>
            {
                navigation.PushAsync(new ProductsPage(brand));
            });
        }
    }
}
