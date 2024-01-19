using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Snipping_Tool_WPF
{
    public class CustomButton : Button
    {
        static CustomButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
            typeof(CustomButton),
            new FrameworkPropertyMetadata(typeof(CustomButton))
            );
        }

        public static readonly DependencyProperty IconLocationProperty =
        DependencyProperty.Register(
        name: nameof(IconLocation),
        propertyType: typeof(Dock),
        ownerType: typeof(CustomButton),
        typeMetadata: new FrameworkPropertyMetadata(defaultValue: Dock.Left));

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            name: nameof(Icon),
            propertyType: typeof(object),
            ownerType: typeof(CustomButton));

        public object? Icon
        {
            get => GetValue(IconLocationProperty);
            set => SetValue(IconLocationProperty, value);
        }

        public Dock IconLocation
        {
            get => (Dock)GetValue(IconLocationProperty);
            set => SetValue(IconLocationProperty, value);
        }

    }
}
