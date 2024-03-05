using System.Diagnostics;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnippingToolWPF.ExtensionMethods;

public static class ShapeExtensions
{
    /// <summary>
    /// This extension method clones the shape, the reason we mostly do this is so the shape has no parent and we can put it in a canvas easier
    /// </summary>
    /// <param name="shape">input shape</param>
    /// <returns>Cloned Shape</returns>
    /// <exception cref="ArgumentException">If shape is not defined in the Clone method</exception>
    public static Shape Clone(this Shape shape, Size size) => shape switch
    {
        Ellipse s => s.Clone(),
        Line s => s.Clone(size),
        Path s => s.Clone(size),
        Polygon s => s.Clone(size),
        Polyline s => s.Clone(),
        // 👆 those are the shapes that are built-in.
        _ => throw new ArgumentException($"Unknown shape type {shape.GetType()}", nameof(shape)),
    };

    public static Shape Clone(this Ellipse shape) => CloneCore(shape);

    public static Shape Clone(this Line shape, Size size)
        => CloneCore(shape, new() { X1 = shape.X1, X2 = shape.X2, Y1 = shape.Y1, Y2 = shape.Y2 });
    public static Shape Clone(this Path shape, Size size)
        => CloneCore(shape, new() { Data = shape.Data });

    public static Shape Clone(this Polygon shape, Size scaleSize)
    {
        ArgumentNullException.ThrowIfNull(shape);

        var scaledPoints = new PointCollection();

        foreach (var point in shape.Points)
        {
            scaledPoints.Add(new Point(point.X * scaleSize.Width, point.Y * scaleSize.Height));
        }

        var clonedShape = new Polygon
        {
            Points = scaledPoints,
            FillRule = shape.FillRule
        };

        clonedShape = CloneCore(shape, clonedShape);
        return clonedShape;
    }

    public static Shape Clone(this Polyline shape)
        => CloneCore(shape, new() { Points = new(shape.Points), FillRule = shape.FillRule });

    public static Shape Clone(this Rectangle shape, Size size)
        => CloneCore(shape, new() { RadiusX = shape.RadiusX, RadiusY = shape.RadiusY });

    private static T CloneCore<T>(T shape)
        where T : Shape, new()
        => CloneCore(shape, new());

    private static T CloneCore<T>(T shape, T destination)
        where T : Shape
    {
        destination.Stroke = shape.Stroke;
        destination.StrokeThickness = shape.StrokeThickness;
        destination.Opacity = shape.Opacity;
        destination.UseLayoutRounding = shape.UseLayoutRounding;
        destination.StrokeDashCap = shape.StrokeDashCap;
        destination.StrokeStartLineCap = shape.StrokeStartLineCap;
        destination.StrokeEndLineCap = shape.StrokeEndLineCap;
        destination.StrokeLineJoin = shape.StrokeLineJoin;
        destination.Effect = shape.Effect;
        destination.Fill = shape.Fill;
        destination.Width = shape.Width;
        destination.Height = shape.Height;
        Canvas.SetLeft(destination, Canvas.GetLeft(shape));
        Canvas.SetTop(destination, Canvas.GetTop(shape));
        return destination;
    }
    
    #region Binding Cloning
    public static BindingBase Clone(this BindingBase binding) => binding switch
    {
        Binding b => b.Clone(),
        MultiBinding b => b.Clone(),
        PriorityBinding b => b.Clone(),
        _ => throw new InvalidOperationException(), // There aren't any others.
    };

    public static Binding Clone(this Binding binding)
    {
        var result = new Binding()
        {
            BindingGroupName = binding.BindingGroupName,
            Delay = binding.Delay,
            FallbackValue = binding.FallbackValue,
            StringFormat = binding.StringFormat,
            TargetNullValue = binding.TargetNullValue,
            AsyncState = binding.AsyncState,
            BindsDirectlyToSource = binding.BindsDirectlyToSource,
            Converter = binding.Converter,
            ConverterCulture = binding.ConverterCulture,
            ConverterParameter = binding.ConverterParameter,
            IsAsync = binding.IsAsync,
            Mode = binding.Mode,
            NotifyOnSourceUpdated = binding.NotifyOnSourceUpdated,
            NotifyOnTargetUpdated = binding.NotifyOnTargetUpdated,
            NotifyOnValidationError = binding.NotifyOnValidationError,
            Path = binding.Path,
            UpdateSourceExceptionFilter = binding.UpdateSourceExceptionFilter,
            UpdateSourceTrigger = binding.UpdateSourceTrigger,
            ValidatesOnDataErrors = binding.ValidatesOnDataErrors,
            ValidatesOnExceptions = binding.ValidatesOnExceptions,
            ValidatesOnNotifyDataErrors = binding.ValidatesOnNotifyDataErrors,
            XPath = binding.XPath,
        };
        if (binding.ElementName is not null)
            result.ElementName = binding.ElementName;
        if (binding.Source is not null)
            result.Source = binding.Source;
        if (binding.RelativeSource is not null)
            result.RelativeSource = binding.RelativeSource;
        foreach (var rule in binding.ValidationRules)
            result.ValidationRules.Add(rule);
        return result;
    }

    public static MultiBinding Clone(this MultiBinding binding)
    {
        var result = new MultiBinding()
        {
            BindingGroupName = binding.BindingGroupName,
            Delay = binding.Delay,
            FallbackValue = binding.FallbackValue,
            StringFormat = binding.StringFormat,
            TargetNullValue = binding.TargetNullValue,
            Converter = binding.Converter,
            ConverterCulture = binding.ConverterCulture,
            ConverterParameter = binding.ConverterParameter,
            Mode = binding.Mode,
            NotifyOnSourceUpdated = binding.NotifyOnSourceUpdated,
            NotifyOnTargetUpdated = binding.NotifyOnTargetUpdated,
            NotifyOnValidationError = binding.NotifyOnValidationError,
            UpdateSourceExceptionFilter = binding.UpdateSourceExceptionFilter,
            UpdateSourceTrigger = binding.UpdateSourceTrigger,
            ValidatesOnDataErrors = binding.ValidatesOnDataErrors,
            ValidatesOnExceptions = binding.ValidatesOnExceptions,
            ValidatesOnNotifyDataErrors = binding.ValidatesOnNotifyDataErrors,
        };
        foreach (var rule in binding.ValidationRules)
            result.ValidationRules.Add(rule);
        foreach (var child in binding.Bindings)
            result.Bindings.Add(child);
        return result;
    }

    public static PriorityBinding Clone(this PriorityBinding binding)
    {
        var result = new PriorityBinding()
        {
            BindingGroupName = binding.BindingGroupName,
            Delay = binding.Delay,
            FallbackValue = binding.FallbackValue,
            StringFormat = binding.StringFormat,
            TargetNullValue = binding.TargetNullValue,
        };
        foreach (var child in binding.Bindings)
            result.Bindings.Add(child);
        return result;
    }
    #endregion
}

