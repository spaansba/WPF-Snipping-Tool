namespace SnippingToolWPF;

public interface ICloneable<out TSelf> : ICloneable
where TSelf : ICloneable<TSelf>
{
    public new TSelf Clone();
    object ICloneable.Clone() => this.Clone();
}
