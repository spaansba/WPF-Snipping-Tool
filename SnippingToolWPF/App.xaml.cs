using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace SnippingToolWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
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
}

