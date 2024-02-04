using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;

namespace SnippingToolWPF.Drawing.Tools
{

    //TODO : hold ctrl to snap the end to the beginning of the line 
    public sealed class PencilTool : IDrawingTool<Polyline>
    {
        private readonly PencilsSidePanelViewModel options;
        public PencilTool(PencilsSidePanelViewModel options)
        {
            this.options = options;
        }

        public Polyline Visual { get; } = new Polyline();

        public void Begin(Point position)
        {
            Visual.StrokeThickness = this.options.Thickness;
            Visual.Stroke = Brushes.Black; // TODO: Allow setting this in PencilsSidePanelViewModel
            Visual.Opacity = this.options.RealOpacity; 
            Visual.Points.Clear();
            Visual.Points.Add(position);
        }
        public void Continue(Point position)
        {
            Visual.Points.Add(position);
        }
        public Polyline Finish()
        {
            return new Polyline()
            {
                Stroke = Visual.Stroke,
                StrokeThickness = Visual.StrokeThickness,
                Opacity = Visual.Opacity,
                Points = new PointCollection(Visual.Points),
            };
        }
    }
}
