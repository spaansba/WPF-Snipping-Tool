﻿using System.Windows;
using System.Windows.Controls;

namespace SnippingToolWPF.SidePanel;

/// <summary>
///     Converts the enum into DataTemplate
/// </summary>
public sealed class SidePanelContentKindIconSelector : DataTemplateSelector
{
    public DataTemplate? Pencils { get; set; }
    public DataTemplate? Shapes { get; set; }
    public DataTemplate? Stickers { get; set; }
    public DataTemplate? Text { get; set; }
    public DataTemplate? Edit { get; set; }

    public override DataTemplate? SelectTemplate(object? item, DependencyObject container)
    {
        if (item is not SidePanelContentKind kind)
            return base.SelectTemplate(item, container);
        return kind switch
        {
            SidePanelContentKind.Pencils => Pencils,
            SidePanelContentKind.Shapes => Shapes,
            SidePanelContentKind.Stickers => Stickers,
            SidePanelContentKind.Text => Text,
            SidePanelContentKind.Edit => Edit,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}