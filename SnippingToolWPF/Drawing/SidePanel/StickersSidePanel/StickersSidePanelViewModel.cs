using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippingToolWPF
{
    public sealed class StickersSidePanelViewModel : SidePanelViewModel
    {
        public override string Header => "Stickers";
        public StickersSidePanelViewModel(DrawingViewModel drawingViewModel) : base(drawingViewModel)
        {
        }
    }
}
