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
    public abstract class SidePanelViewModel : ObservableValidator
    {
        public abstract string Header { get; }
        protected DrawingViewModel drawingViewModel { get; }
        protected SidePanelViewModel(DrawingViewModel drawingViewModel)
        {
            this.drawingViewModel = drawingViewModel;
        }
    }
}
