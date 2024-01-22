using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippingToolWPF
{
    public partial class DrawingViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(SidePanelContent))]
        private SidePanelContentKind sidePanelContentKind;

        public object? SidePanelContent => SidePanelContentKind switch
        {
            SidePanelContentKind.Pencils => "Pencils", // Headers of each button
            SidePanelContentKind.Shapes => "Shapes",
            SidePanelContentKind.Stickers => "Stickers",
            SidePanelContentKind.Text => "Text",
            _ => null,
        };

        public IReadOnlyList<SidePanelContentKind> AllSidePanelContentKinds { get; } = Enum.GetValues<SidePanelContentKind>();
    }
}
