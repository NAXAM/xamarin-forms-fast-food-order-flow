using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ffof.iOS.Renderers;
using ffof.Controls;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace ffof.iOS.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        protected override UITextField CreateNativeControl()
        {
            var textField = new UIBackwardsTextField(RectangleF.Empty);
            textField.BorderStyle = UITextBorderStyle.None;
            textField.ClipsToBounds = true;

            if (Element != null)
            {
                textField.OnDeleteBackward += (o, ea) =>
                {
                    (Element as BorderlessEntry)?.OnBackspacePressed();
                };
            }

            return textField;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            if (Element != null && Control != null)
            {
                Debug.WriteLine(Control.ToString());
                Debug.WriteLine(Element.ToString());

                (Control as UIBackwardsTextField).OnDeleteBackward += (o, ea) =>
                {
                    (Element as BorderlessEntry)?.OnBackspacePressed();
                };
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control != null)
            {
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;
                if (Control.Enabled == false && Element != null)
                {
                    Control.TextColor = Element.TextColor.ToUIColor();
                }
            }
        }
    }

    public class UIBackwardsTextField : UITextField
    {
        public delegate void DeleteBackwardEventHandler(object sender, EventArgs e);

        public event DeleteBackwardEventHandler OnDeleteBackward;

        public UIBackwardsTextField(CGRect frame)
            : base(frame)
        {
        }

        public void OnDeleteBackwardPressed()
        {
            OnDeleteBackward?.Invoke(null, null);
        }

        public UIBackwardsTextField()
        {
            BorderStyle = UITextBorderStyle.RoundedRect;
            ClipsToBounds = true;
        }

        public override void DeleteBackward()
        {
            base.DeleteBackward();
            OnDeleteBackwardPressed();
        }
    }
}