using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SnippingToolWPF.Control
{
    public static class BehaviorExtensions
    {
        public static TBehavior EnsureBehavior<TBehavior>(this DependencyObject obj)
            where TBehavior : Behavior, new()
        {
            var behaviors = Interaction.GetBehaviors(obj);
            var behavior = behaviors.OfType<TBehavior>().FirstOrDefault();
            if (behavior is not null)
                return behavior;
            behavior = new();
            behaviors.Add(behavior);
            return behavior;
        }

        public static void RemoveBehaviors<TBehavior>(this DependencyObject obj)
        where TBehavior : Behavior
        {
            var behaviors = Interaction.GetBehaviors(obj);
            for (var i = behaviors.Count - 1; i >= 0; --i)
            {
                if (behaviors[i] is TBehavior)
                    behaviors.RemoveAt(i);
            }
        }
    }
}
