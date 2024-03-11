using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SnippingToolWPF.SidePanel.TextSidePanel;

public sealed partial class TextSidePanelViewModel(DrawingViewModel drawingViewModel)
    : SidePanelViewModel(drawingViewModel)
{
    [ObservableProperty]
    private FontInfo? fontFamily = DefaultFont;

    [ObservableProperty]
    private double fontSize = 12;

    [ObservableProperty]
    private Color borderColor;

    [ObservableProperty]
    private HorizontalAlignment alignment = HorizontalAlignment.Left;

    public static List<double> FontSizeList { get; } = [8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72, 94, 130];

    public override string Header => "Text";

    public static IReadOnlyList<FontInfo> AllFontFamilies { get; } =
       Fonts.SystemFontFamilies
           .Where(font => !FontInfo.FontsToExclude.Contains(font.ToString()))
           .OrderBy(static font => font.ToString())
           .Select(FontInfo.CreateFontInfo)
           .ToList()
           .AsReadOnly();
    
    private static readonly FontInfo? DefaultFont = AllFontFamilies.FirstOrDefault(static x => x.ToString() == "Calibri");
}
