namespace SnippingToolWPF;

/// <summary>
/// This Class adds no properties just handles the setup for Visual Cloning
/// </summary>
/// <typeparam name="TSelf"></typeparam>
public abstract class DrawingShape<TSelf> : DrawingShape, ICloneable<TSelf>
where TSelf : DrawingShape<TSelf>, new()
{
    protected virtual void PopulateClone(TSelf clone)
    {
        // clone each property defined on the non-generic DrawingShape
        // derived classes would override this method, and clone the properties new to that type
    }

    public sealed override TSelf Clone()
    {
        var clone = new TSelf();
        PopulateClone(clone);
        return clone;
    }
    object ICloneable.Clone() => this.Clone();
}
