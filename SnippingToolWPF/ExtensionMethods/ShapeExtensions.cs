using System.Diagnostics;
using System.Dynamic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
    public static Shape Clone(this Shape shape) => shape switch
    {
        Ellipse s => s.Clone<Ellipse>(true),
        Line s => s.Clone<Line>(true),
        Path s => s.Clone<Path>(true),
        Polygon s => s.Clone<Polygon>(true),
        Polyline s => s.Clone<Polyline>(true),
        Rectangle s => s.Clone<Rectangle>(true),
        // 👆 those are the shapes that are built-in.
        _ => throw new ArgumentException($"Unknown shape type {shape.GetType()}", nameof(shape)),
    };

    public static T Clone<T>(this T obj) where T : DependencyObject => CloneUnsafe(obj, true);

    public static T Clone<T>(this T obj, bool cloneBindings)
    where T : DependencyObject, new()
    => Clone<T>(obj, static () => new T(), cloneBindings);

    public static T Clone<T>(T source, Func<T> createInstance, bool cloneBindings)
    where T : DependencyObject
    {
        var destination = createInstance(); // Create an instance of T
        var localValueEnumerator = source.GetLocalValueEnumerator();
        while (localValueEnumerator.MoveNext())
        {
            var entry = localValueEnumerator.Current;
            Debug.WriteLine($"Property: {entry.Property.Name}, Value: {entry.Value}");
            if (entry.Property.ReadOnly is false)
            {
                SetProperty(source, destination, entry.Property, entry.Value, cloneBindings);
            }
        }
        return destination;
    }

    /// <summary>
    /// Performs a shallow clone of an object, cloning any bindings that are used
    /// </summary>
    /// <typeparam name="T">type of object to clone</typeparam>
    /// <param name="obj"> obj to clone</param>
    /// <param name="cloneBindings"></param>
    /// <returns></returns>
    /// <remarks>
    /// <b>IMPORTANT</b> Ensure the type has a public parameterless constructor
    /// </remarks>
    internal static T CloneUnsafe<T>(this T obj, bool cloneBindings)
    where T : DependencyObject
    => Clone<T>(obj, Activator.CreateInstance<T>, cloneBindings);


    private static void SetProperty(DependencyObject source, DependencyObject desination, DependencyProperty property, object? value, bool cloneBindings)
    {
        var binding = BindingOperations.GetBindingBase(source, property);
        if (cloneBindings is false || binding is null)
            desination.SetValue(property, value); // Set the property if its not a binding
        else
            BindingOperations.SetBinding(desination, property, Clone(binding));
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

