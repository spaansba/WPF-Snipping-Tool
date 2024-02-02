using CommunityToolkit.Mvvm.ComponentModel;
using SnippingToolWPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SnippingToolWPF
{
    public partial class DrawingViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(SidePanelContent))]
        private SidePanelContentKind sidePanelContentKind = SidePanelContentKind.Pencils;

        //List of the side panel enums
        public IReadOnlyList<SidePanelContentKind> AllSidePanelContentKinds { get; } = Enum.GetValues<SidePanelContentKind>();

        private readonly PencilsSidePanelViewModel pencilsPanel;
        private readonly ShapesSidePanelViewModel shapesPanel;
        private readonly StickersSidePanelViewModel stickersPanel;
        private readonly TextSidePanelViewModel textPanel;

        public DrawingViewModel()
        {
            this.pencilsPanel = new PencilsSidePanelViewModel(this);
            this.shapesPanel = new ShapesSidePanelViewModel(this);
            this.stickersPanel = new StickersSidePanelViewModel(this);
            this.textPanel = new TextSidePanelViewModel(this);
        }

        public SidePanelViewModel? SidePanelContent => SidePanelContentKind switch
        {
            SidePanelContentKind.Pencils => pencilsPanel,
            SidePanelContentKind.Shapes => shapesPanel,
            SidePanelContentKind.Stickers => stickersPanel,
            SidePanelContentKind.Text => textPanel,
            _ => null,
        };


        public ObservableCollection<UIElement> DrawingObjects { get; } = new ObservableCollection<UIElement>();

    }
}
