using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Expression = System.Linq.Expressions.Expression;

namespace SnippingToolWPF
{
    /// <summary>
    /// We create this to alter the band colors of built-in ToolbarTray programmatically
    /// </summary>
 
    public sealed class BandInfo
    {
        public IReadOnlyList<ToolBar> Toolbars { get; set; } = Array.Empty<ToolBar>();
        public double Thickness { get; set; }
    }

    public class CustomToolbarTray : ToolBarTray
    {

        private static readonly Action<ToolBarTray, List<BandInfo>> refreshBandInfo = CreateGetBandInfoDelegate();
        private readonly List<BandInfo> bandThicknesses = new();

        private static readonly DependencyProperty BandBackgroundBrushesProperty = DependencyProperty.Register(
            name: nameof(BandBackgroundBrushes),
            propertyType: typeof(List<Brush?>),
            ownerType: typeof(CustomToolbarTray),
            typeMetadata: new FrameworkPropertyMetadata());

        public CustomToolbarTray()
        {
            BandBackgroundBrushes = new();
        }

        public List<Brush?> BandBackgroundBrushes
        {
            get
            {
                var result = (List<Brush?>)this.GetValue(BandBackgroundBrushesProperty);
                if (result != null)
                {
                    return result;
                }
                result = new();
                this.BandBackgroundBrushes = result;
                return result;
            }
            set => this.SetValue(BandBackgroundBrushesProperty,value);
        }


        protected override Size MeasureOverride(Size constraint)
        {
            Size result = base.MeasureOverride(constraint);
            refreshBandInfo(this, this.bandThicknesses);
            return result;
        }

        private Brush? GetBandBrush(int index)
        {
            var brushes = this.BandBackgroundBrushes;
            if (index < 0 || index >= brushes.Count)
            {
                return null;
            }
            return brushes[index];
        }

        private Rect GetBandRect(int index)
        {
            var myThickness = this.bandThicknesses[index].Thickness;
            var priorThicknesses = 0d;
            for (var i = 0; i < index; ++i)
                priorThicknesses += this.bandThicknesses[i].Thickness;
            if (this.Orientation is Orientation.Horizontal)
            {
                return new Rect(0, priorThicknesses, this.RenderSize.Width, myThickness);
            }
            return new Rect(priorThicknesses, 0, myThickness, this.RenderSize.Height);
        }

        private int GetBandCount()
        {
            return this.bandThicknesses.Count;
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            int bandCount = this.GetBandCount();
            for (int band = 0; band < bandCount; band++)
            {
                Rect rect = GetBandRect(band);
                Brush? brush = GetBandBrush(band);
                if (brush != null)
                {
                    dc.DrawRectangle(brush, null, rect);
                }
            }
        }

        private static void FillItems<T>(List<BandInfo> destination,List<T> source,Func<T, double> getThickness,Func<T, List<ToolBar>> getToolbars)
        {
            destination.EnsureCapacity(source.Count);
            for (var i = 0; i < source.Count; ++i)
            {
                var thickness = getThickness(source[i]);
                var toolbars = getToolbars(source[i]);

                if (i >= destination.Count)
                    destination.Add(new());

                destination[i].Thickness = thickness;
                destination[i].Toolbars = toolbars;
            }
            while (destination.Count > source.Count)
                destination.RemoveAt(destination.Count - 1);
        }

        private static Action<ToolBarTray, List<BandInfo>> CreateGetBandInfoDelegate()
        {
            var bandsField = typeof(ToolBarTray).GetField( "_bands", BindingFlags.NonPublic | BindingFlags.Instance);
            Debug.Assert(bandsField is not null, "Could not find _bands field on ToolbarTray");
            var fillItemsNonGenericMethod = typeof(CustomToolbarTray).GetMethod( nameof(FillItems), BindingFlags.NonPublic | BindingFlags.Static);
            Debug.Assert(fillItemsNonGenericMethod is not null, $"Could not find {nameof(FillItems)} method on {nameof(CustomToolbarTray)}");
            var bandInfoListType = bandsField.FieldType;
            var bandInfoType = bandInfoListType.GenericTypeArguments[0];

            var fillItemsMethod = fillItemsNonGenericMethod.MakeGenericMethod(bandInfoType);
            var getThicknessDelegate = CreateGetThicknessDelegate(bandInfoType);
            var getToolbarsDelegate = CreateGetToolbarsDelegate(bandInfoType);

            var toolbarTray = Expression.Parameter(typeof(ToolBarTray));
            var destination = Expression.Parameter(typeof(List<BandInfo>));
            var source = Expression.Variable(bandInfoListType);

            var body = Expression.Block(variables: new[] { source },Expression.Assign(source, Expression.Field(toolbarTray, bandsField)),
            Expression.Call( null, fillItemsMethod, destination, source, Expression.Constant(getThicknessDelegate),  Expression.Constant(getToolbarsDelegate)));
            return Expression.Lambda<Action<ToolBarTray, List<BandInfo>>>(body, toolbarTray,destination).Compile();

            static Delegate CreateGetThicknessDelegate(Type bandInfoType)
            {
                var parameter = Expression.Parameter(bandInfoType);
                return Expression.Lambda( Expression.Property(parameter, "Thickness"), parameter).Compile();
            }

            static Delegate CreateGetToolbarsDelegate(Type bandInfoType)
            {
                var parameter = Expression.Parameter(bandInfoType);
                return Expression.Lambda( Expression.Property(parameter, "Band"), parameter ).Compile();
            }
        }
    }
}
