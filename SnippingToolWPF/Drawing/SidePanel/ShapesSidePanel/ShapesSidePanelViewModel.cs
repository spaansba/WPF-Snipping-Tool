using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippingToolWPF
{
    public sealed class ShapesSidePanelViewModel : SidePanelViewModel
    {
        public override string Header => "Shapes";
        public ShapesSidePanelViewModel(DrawingViewModel drawingViewModel) : base(drawingViewModel)
        {
        }

    }
}
