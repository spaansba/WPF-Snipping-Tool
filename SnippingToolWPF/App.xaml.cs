using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using SnippingToolWPF.Common;

namespace SnippingToolWPF;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App
{
    static App()
    {
        BindingErrorListener.WriteToConsole();

        //Task.Run(() =>
        //{
        //    while (true)
        //    {
        //        Debug.WriteLine("Test");
        //        Thread.Sleep(1000);
        //    }
        //});

        // Set the language settings to the users PC
        FrameworkElement.LanguageProperty.OverrideMetadata(
            typeof(FrameworkElement),
            new FrameworkPropertyMetadata(
                XmlLanguage.GetLanguage(
                    CultureInfo.CurrentCulture.IetfLanguageTag)));
    }
}