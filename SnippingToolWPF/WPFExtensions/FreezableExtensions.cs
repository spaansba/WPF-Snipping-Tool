using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace SnippingToolWPF.WPFExtensions;

public static class FreezableExtensions
{
    [return: NotNullIfNotNull("freezable")]
    public static T? CloneIfNotFrozen<T>(
    this T? freezable
    ) where T : Freezable
    {
        return freezable?.IsFrozen switch
        {
            null => null,
            true => freezable,
            false => (T)freezable.Clone(),
        };
    }
}
