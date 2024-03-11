using System.Runtime.CompilerServices;

namespace SnippingToolWPF.Common;

/// <summary>
/// Boxes allow us to cache values, for example if we cache TRUE / FALSE there will be only 2 boxes instead of N * 2 boxes
/// We create a generic Boxes class so each type can create boxes and thus safe memory
/// 
/// e.g. if int box 10 exists > use that box
/// if it doesnt exist > create new box
/// 
/// Basically we transform Value types into Reference Types (hold the reference of a value in memory, not the value)
/// </summary>
/// If using this dont forget to copy DependencyObjectExtensions class

internal static class Boxes
{

    //Dont use => as it will create a new box every time instead of reusing existing box
    public static object True { get; } = true;

    public static object False { get; } = false;
    public static object NaN { get; } = double.NaN;
    public static object PositiveInfinity { get; } = double.PositiveInfinity;
    public static object DoubleZero { get; } = 0d;
    public static object IntegerZero { get; } = 0;

    public static object Box(bool value)
    {
        if (value)
            return True;
        return False;
    }

    public static object Box(int value)
    {
        if (value == 0)
            return IntegerZero;
        return value;
    }
    public static object Box(double value)
    {
        if (double.IsNaN(value))
            return NaN;
        if (double.IsPositiveInfinity(value))
            return PositiveInfinity;
        if (value == 0)
            return DoubleZero;
        return value;
    }

    public static object? Box<T>(T? value)
    {
        if (value is null)
            return null;
        if (typeof(T) == typeof(bool))
            return Box(Unsafe.As<T, bool>(ref value));
        if (typeof(T) == typeof(int))
            return Box(Unsafe.As<T, int>(ref value));
        if (typeof(T) == typeof(double))
            return Box(Unsafe.As<T, double>(ref value));
        return value;
    }
}
