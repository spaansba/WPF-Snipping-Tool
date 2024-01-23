using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippingToolWPF
{
    public sealed class PencilsSidePanelViewModel : SidePanelViewModel
    {
        public override string Header => "Pencils";
        public PencilsSidePanelViewModel(DrawingViewModel drawingViewModel) : base(drawingViewModel)
        {
        }
    }
}
