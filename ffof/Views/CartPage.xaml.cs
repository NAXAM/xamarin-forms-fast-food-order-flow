using System;
using System.Collections.Generic;
using ffof.ViewModels;
using Xamarin.Forms;

namespace ffof.Views
{
    public partial class CartPage : ContentPage
    {
        public CartPage()
        {
            InitializeComponent();

            BindingContext = new CartViewModel(Navigation);
        }
    }
}
