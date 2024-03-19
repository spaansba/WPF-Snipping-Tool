using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SnippingToolWPF.ExtensionMethods;

public static class ShapeExtensions
{
    /// <summary>
    ///     This extension method clones the shape, the reason we mostly do this is so the shape has no parent and we can put
    ///     it in a canvas easier
    /// </summary>
    /// <param name="shape">input shape</param>
    /// <param name="size"></param>
    /// <returns>Cloned Shape</returns>
    /// <exception cref="ArgumentException">If shape is not defined in the Clone method</exception>
    public static DrawingShape Clone(this DrawingShape shape, Size size)
    {
        return shape switch
        {
            RegularPolygonDrawingShape s => s.Clone(size),
            //    RegularPolylineDrawingShape s => s.Clone(size),
            // 👆 those are the shapes that are built-in.
            _ => throw new ArgumentException($"Unknown shape type {shape.GetType()}", nameof(shape))
        };
    }

    //   public static Shape Clone(this Ellipse shape) => CloneCore(shape);
    

    // public static DrawingShape Clone(this RegularPolylineDrawingShape shape)
    //      => CloneCore(shape, new() { Points = new(shape.Points), FillRule = shape.FillRule });
    //private static T CloneCore<T>(T shape)
    //    where T : DrawingShape, new()
    //    => CloneCore(shape, new());

    private static T CloneCore<T>(T shape, T destination)
        where T : DrawingShape
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

    public static BindingBase Clone(this BindingBase binding)
    {
        return binding switch
        {
            Binding b => b.Clone(),
            MultiBinding b => b.Clone(),
            PriorityBinding b => b.Clone(),
            _ => throw new InvalidOperationException() // There aren't any others.
        };
    }

    public static Binding Clone(this Binding binding)
    {
        var result = new Binding
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
            XPath = binding.XPath
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
        var result = new MultiBinding
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
            ValidatesOnNotifyDataErrors = binding.ValidatesOnNotifyDataErrors
        };
        foreach (var rule in binding.ValidationRules)
            result.ValidationRules.Add(rule);
        foreach (var child in binding.Bindings)
            result.Bindings.Add(child);
        return result;
    }

    public static PriorityBinding Clone(this PriorityBinding binding)
    {
        var result = new PriorityBinding
        {
            BindingGroupName = binding.BindingGroupName,
            Delay = binding.Delay,
            FallbackValue = binding.FallbackValue,
            StringFormat = binding.StringFormat,
            TargetNullValue = binding.TargetNullValue
        };
        foreach (var child in binding.Bindings)
            result.Bindings.Add(child);
        return result;
    }

    #endregion
}