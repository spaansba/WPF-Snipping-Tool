using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace SnippingToolWPF.Control
{
    class SliderTitleAndTextBox
    {
        public static readonly DependencyProperty SliderHeaderProperty = DependencyProperty.RegisterAttached(
        name: "SliderHeader",
        propertyType: typeof(string),
        ownerType: typeof(SliderTitleAndTextBox), new PropertyMetadata("Header not set"));

        public static string GetSliderHeader(DependencyObject target) => (string)target.GetValue(SliderHeaderProperty);
        public static void SetSliderHeader(DependencyObject target, string value) => target.SetValue(SliderHeaderProperty, value);

        public static readonly DependencyProperty SliderValueInTextProperty = DependencyProperty.RegisterAttached(
        name: "SliderValueInText",
        propertyType: typeof(string),
        ownerType: typeof(SliderTitleAndTextBox), new PropertyMetadata("Value not set"));

        public static string GetSliderValueInText(DependencyObject target) => (string)target.GetValue(SliderValueInTextProperty);
        public static void SetSliderValueInText(DependencyObject target, string value) => target.SetValue(SliderValueInTextProperty, value);
    }
}
