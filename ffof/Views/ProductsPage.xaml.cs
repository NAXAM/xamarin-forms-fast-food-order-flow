using System;
using System.ComponentModel;
using System.Diagnostics;
using ffof.Models;
using ffof.ViewModels;
using Xamarin.Forms;

namespace ffof.Views
{
    public partial class ProductsPage : ContentPage
    {

        public ProductsPage()
            : this(new BrandModel
            {
                ColorCode = Color.Indigo.ToHex(),
                PricingCategory = "$$$",
                HighlightUrl = "https://gigamall.com.vn/data/2019/09/20/11493168_Trung-t%C3%A2m-th%C6%B0%C6%A1ng-m%E1%BA%A1i-GIGAMALL-McDonalds-1.jpg",
                LogoUrl = "https://gigamall.com.vn/data/2019/09/20/11491513_LOGO-McDonald_s.jpg",
                LongestWaitingTime = 20,
                Name = "McDonald's",
                Rating = 4.5,
                Remark = "Burgers, America",
                ShortestWaitingTime = 10,
            })
        {
        }

        public ProductsPage(BrandModel brand)
        {
            InitializeComponent();

            BindingContext = new ProductsViewModel(Navigation, brand);

            (BindingContext as INotifyPropertyChanged).PropertyChanged += ProductsPage_PropertyChanged;

            lstProducts.Scrolled += LstProducts_Scrolled;
        }

        private void ProductsPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ProductsViewModel.ProductShown))
            {
                var vm = BindingContext as ProductsViewModel;

                if (originalY < 0)
                {
                    originalY = itemContainer.Y;
                }

                if (vm.ProductShown == true)
                {
                    (itemContainer.Parent as VisualElement).IsVisible = true;
                    itemContainer.TranslateTo(itemContainer.X, originalY);
                }
                else
                {
                    itemContainer.TranslateTo(itemContainer.X, originalY + itemContainer.Height * 1.1);
                    (itemContainer.Parent as VisualElement).IsVisible = false;
                }
            }
        }

        private void LstProducts_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            if (e.VerticalOffset < 152)
            {
                container.Margin = new Thickness(0, 152, 0, -152);
                return;
            }

            var baseline = e.VerticalOffset - 152;
            var delta = Math.Max(152 - baseline, -(Device.RuntimePlatform == Device.iOS ? 32 : 24));

            container.Margin = new Thickness(0, delta, 0, Math.Min(-delta, 0));
        }

        double originalY = -1;
        double lastPannedY = 0;

        void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            Debug.WriteLine("Scroll: " + e.StatusType + ":" + e.TotalY);

            if (originalY < 0)
            {
                originalY = itemContainer.TranslationY;
            }

            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    return;
                case GestureStatus.Canceled:
                case GestureStatus.Completed:
                    if (lastPannedY < itemContainer.Height / 2)
                    {
                        itemContainer.TranslateTo(itemContainer.X, originalY);
                    }
                    else
                    {
                        itemContainer.TranslateTo(itemContainer.X, originalY + itemContainer.Height * 1.1);

                        (BindingContext as ProductsViewModel).ProductShown = false;
                        (BindingContext as ProductsViewModel).ShownProduct = null;
                    }
                    return;
                default:
                    break;
            }

            lastPannedY = e.TotalY;

            var newY = originalY + Math.Max(e.TotalY, 0);
            itemContainer.TranslateTo(itemContainer.X, newY);
        }
    }
}
