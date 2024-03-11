using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SnippingToolWPF;

public static class WithBindingExtensions
{
    /// <summary>
    /// Extension method to simplify setting up data bindings for any DependencyObject
    /// </summary>
    public static T WithBinding<T>(this T target,
        DependencyProperty targetProperty,
        PropertyPath path,
        object source,
        IValueConverter? converter = default,
        object? converterParameter = default) where T : DependencyObject
    {
        BindingOperations.SetBinding(target, targetProperty, new Binding()
        {
            Source = source,
            Converter = converter,
            Path = path,
            ConverterParameter = converterParameter
        });
        return target;
    }

    /// <summary>
    /// Same as WithBinding but:
    /// Before, we stitched together the strings (via an interpolated string). The PropertyPath constructor will then just unstitch them.
    /// Now, we are just skipping the stitching part of the process.
    /// </summary>
    public static T WithBindingPathParts<T>(
    this T target,
    DependencyProperty targetProperty,
    object source,
    params string[] pathParts) where T : DependencyObject
    {
        if (pathParts.Length is 0)
            throw new ArgumentException("Must provide at least one path part", nameof(pathParts));

        BindingOperations.SetBinding(target, targetProperty, new Binding()
        {
            Source = source,
            Path = new PropertyPath(
                GetPath(pathParts.Length),
                pathParts
            ),
        });
        return target;
    }

    #region Get Path
    /// <summary>
    /// Optimized path for 3 or less values (most common) if more than 3 values take the slow route
    /// </summary>
    private static string GetPath(int length) => length switch
    {
        1 => "(0)",
        2 => "(0).(1)",
        3 => "(0).(1).(2)",
        <= 10 => GetPathSlow(length),
        _ => GetPathReallySlow(length),
    };

    /// <summary>
    /// Doesnt work on strings longer than 10
    /// </summary>
    private static string GetPathSlow(int length)
    {
        var requiredLength = length + (length * 2) + (length - 1);
        return string.Create(
            requiredLength,
            length,
            static (span, length) =>
            {
                for (var i = 0; i < length; ++i)
                {

                    if (i is not 0)
                    {
                        span[0] = '.';
                        span = span[1..];
                    }
                    span[0] = '(';
                    span = span[1..];
                    _ = i.TryFormat(span, out var written, default, default);
                    span = span[written..];
                    span[0] = ')';
                    span = span[1..];
                }
            }
        );
    }
    private static string GetPathReallySlow(int length)
    {
        return string.Join(".",Enumerable
                .Range(0, length)
                .Select(num => $"({num})")
        );
    }
    #endregion
}
