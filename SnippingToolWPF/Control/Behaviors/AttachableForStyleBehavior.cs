﻿using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SnippingToolWPF.Control;

public abstract class AttachableForStyleBehavior<TSelf, TComponent> : Behavior<TComponent>
where TComponent : DependencyObject
where TSelf : AttachableForStyleBehavior<TSelf, TComponent>, new()
{
    public static readonly DependencyProperty IsEnabledForStyleProperty = DependencyProperty.RegisterAttached(
        nameof(IsEnabledForStyle),
        typeof(bool),
        typeof(AttachableForStyleBehavior<TSelf, TComponent>),
        new FrameworkPropertyMetadata(false, OnIsEnabledForStyleChanged)
    );

    public bool IsEnabledForStyle
    {
        get => this.GetValue<bool>(IsEnabledForStyleProperty);
        set => this.SetValue<bool>(IsEnabledForStyleProperty, value);
    }

    private static void OnIsEnabledForStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not TComponent component)
            return;
        if (e.NewValue is true)
        {
            component.EnsureBehavior<TSelf>();
        }
        else
        {
            component.RemoveBehaviors<TSelf>();
        }
    }
}
