using System.Windows.Media;

namespace SnippingToolWPF.SidePanel.TextSidePanel;

public sealed record FontInfo(bool FontWithSymbol, FontFamily Family)
{
    public static List<string> FontsToExclude = new List<string>
    {
        "MS Reference Specialty",
        "HoloLens MDL2 Assets",
        "MS Outlook",
        "Segoe MDL2 Assets"
    };

    public static FontInfo CreateFontInfo(FontFamily font)
    {
        return new FontInfo(IsSymbol(font), font);
    }

    internal static bool IsSymbol(FontFamily font)
    {
        var typeface = font.GetTypefaces().First();
        typeface.TryGetGlyphTypeface(out var glyph);
        return !font.Source.Contains("Global") && (glyph == null || glyph.Symbol);
    }
}