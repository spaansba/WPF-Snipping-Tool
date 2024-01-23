using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SnippingToolWPF
{
    public abstract class SidePanelViewModel : ObservableObject
    {
        public abstract string Header { get; }
        protected DrawingViewModel drawingViewModel { get; }

        protected SidePanelViewModel(DrawingViewModel drawingViewModel)
        {
            this.drawingViewModel = drawingViewModel;
        }
    }

    public sealed class PencilsSidePanelViewModel : SidePanelViewModel
    {
        public override string Header => "Pencils";
        public PencilsSidePanelViewModel(DrawingViewModel drawingViewModel) : base(drawingViewModel)
        {
        }
    }

    public sealed class ShapesSidePanelViewModel : SidePanelViewModel
    {
        public override string Header => "Shapes";
        public ShapesSidePanelViewModel(DrawingViewModel drawingViewModel) : base(drawingViewModel)
        {
        }
        
    }

    public sealed class StickersSidePanelViewModel : SidePanelViewModel
    {
        public override string Header => "Stickers";
        public StickersSidePanelViewModel(DrawingViewModel drawingViewModel) : base(drawingViewModel)
        {
        }
        
    }

    public sealed partial class TextSidePanelViewModel : SidePanelViewModel
    {
        [ObservableProperty]
        private FontFamily? fontFamily = defaultFont;

        [ObservableProperty]
        public double fontSize = 12;
        public static List<double> FontSizeList => new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72, 94, 130 };

        public override string Header => "Text";
        public TextSidePanelViewModel(DrawingViewModel drawingViewModel) : base(drawingViewModel)
        {
        }

        //TODO: Remove some of the weird fonts that dont work and just show squares
        public static IReadOnlyList<FontFamily> AllFontFamilies { get; } = 
            Fonts.SystemFontFamilies
            .OrderBy(static font => font.ToString())
            .ToList()
            .AsReadOnly();

        private static readonly FontFamily? defaultFont = AllFontFamilies.FirstOrDefault(static x => x.ToString() == "Calibri");
    }
}
