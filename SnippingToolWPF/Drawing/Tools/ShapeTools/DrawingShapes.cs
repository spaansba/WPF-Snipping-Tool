
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnippingToolWPF.Drawing.Tools.ShapeTools
{
    public class DrawingShape : Shape
    {
        public DrawingShape() : base()
        {
        }

        protected override Geometry DefiningGeometry => throw new NotImplementedException();
    }
}
