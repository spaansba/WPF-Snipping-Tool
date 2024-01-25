using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SnippingToolWPF
{
    public sealed partial class TextSidePanelViewModel : SidePanelViewModel
    {
        [ObservableProperty]
        private FontInfo? fontFamily = defaultFont;

        [ObservableProperty]
        public double fontSize = 12;
        public static List<double> FontSizeList { get; } = [8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72, 94, 130];

        public override string Header => "Text";
        public TextSidePanelViewModel(DrawingViewModel drawingViewModel) : base(drawingViewModel)
        {
        }

        public static IReadOnlyList<FontInfo> AllFontFamilies { get; } =
           Fonts.SystemFontFamilies
               .Where(font => !FontInfo.FontsToExclude.Contains(font.ToString()))
               .OrderBy(static font => font.ToString())
               .Select(FontInfo.CreateFontInfo)
               .ToList()
               .AsReadOnly();
        
        private static readonly FontInfo? defaultFont = AllFontFamilies.FirstOrDefault(static x => x.ToString() == "Calibri");
    }
}
