namespace SnippingToolWPF;

public interface ICloneable<out TSelf> : ICloneable
    where TSelf : ICloneable<TSelf>
{
    object ICloneable.Clone()
    {
        return Clone();
    }

    public new TSelf Clone();
}