using System;
using Xamarin.Forms;

namespace ffof.Controls
{
    public class BorderlessEntry : Entry
    {
        public delegate void BackspaceEventHandler(object sender, EventArgs e);

        public event BackspaceEventHandler OnBackspace;

        public void OnBackspacePressed()
        {
            OnBackspace?.Invoke(this, null);
        }

        public static readonly BindableProperty IsFocusProperty = BindableProperty
            .Create(nameof(IsFocus), typeof(bool), typeof(BorderlessEntry), propertyChanged: OnFocusChanged);

        private static void OnFocusChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if ((bool)newValue)
            {
                (bindable as Entry).Focus();
            }
            else
            {
                (bindable as Entry).Unfocus();
            }
        }

        public bool IsFocus
        {
            get
            {
                return (bool)GetValue(IsFocusProperty);
            }

            set
            {
                SetValue(IsFocusProperty, value);
            }
        }
    }
}

