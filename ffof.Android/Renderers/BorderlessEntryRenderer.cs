using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using ffof.Controls;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ffof.Droid.Renderers;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace ffof.Droid.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        public BorderlessEntryRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null && Element != null)
            {
                Control.Background = new ColorDrawable(Color.Transparent.ToAndroid());
                Control.SetSelection(Control.Text.Length);
                Control.SetTextColor(Element.TextColor.ToAndroid());
                Control.Gravity = GravityFlags.Center;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "IsFocused")
            {
                Control.SetSelection(Control.Text.Length);
            }
        }

        public override bool DispatchKeyEvent(KeyEvent e)
        {
            if (e.Action == KeyEventActions.Down)
            {
                if (e.KeyCode == Keycode.Del)
                {
                    if (string.IsNullOrEmpty(Control.Text))
                    {
                        (Element as BorderlessEntry)?.OnBackspacePressed();
                    }
                }
            }

            return base.DispatchKeyEvent(e);
        }
    }
}