using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippingToolWPF;

public sealed partial class EditSidePanelViewModel : SidePanelViewModel
{
    public EditSidePanelViewModel(DrawingViewModel drawingViewModel) : base(drawingViewModel)
    {
    }

    public override string Header => "Edit";

}
