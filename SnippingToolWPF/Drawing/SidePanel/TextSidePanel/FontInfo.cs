using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SnippingToolWPF;

public sealed record FontInfo(bool FontWithSymbol, FontFamily Family)
{
    public static FontInfo CreateFontInfo(FontFamily font)
    {
        return new(IsSymbol(font), font);
    }

    internal static bool IsSymbol(FontFamily font)
    {
        Typeface typeface = font.GetTypefaces().First();
        typeface.TryGetGlyphTypeface(out GlyphTypeface glyph);
        return (!font.Source.Contains("Global")) && (glyph == null || glyph.Symbol);
    }

    public static List<string> FontsToExclude = new List<string>
    {
        "MS Reference Specialty",
        "HoloLens MDL2 Assets",
        "MS Outlook",
        "Segoe MDL2 Assets",
    };
}
