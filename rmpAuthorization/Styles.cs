using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace rmpAuthorization
{
    internal class Styles
    {
        // POKA VPADLU)))
        public Style getButtonStyle()
        {
            Style buttonStyle = new Style();
            buttonStyle.Setters.Add(new Setter { Property= Control.FontFamilyProperty, Value= new FontFamily("Verdana")});
            buttonStyle.Setters.Add(new Setter { Property = Control.BorderBrushProperty, Value = (Colors.Black) });

            return buttonStyle;
        }
    }
}
