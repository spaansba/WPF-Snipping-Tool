﻿using SnippingToolWPF.Drawing.Editing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnippingToolWPF.Drawing.Shapes;

public abstract class DrawingShape : Decorator, IShape
{
    private readonly TextBlock textBlock;
    private readonly Canvas canvas;
    public DrawingShape()
    {
        this.textBlock = SetupTextBlock(this);
        var rotateTransform = new RotateTransform();
        BindingOperations.SetBinding(rotateTransform,RotateTransform.AngleProperty,new Binding() { Source = this, Path = new PropertyPath(DrawingShape.AngleProperty) });

        this.canvas = new Canvas()
        {
            Children = { textBlock },
            LayoutTransform = rotateTransform, // TODO: Is RenderTransform enough?  We should use that, if we can.
        };
        this.Child = this.canvas;
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.Property == VisualProperty) //If Visual gets replaced call OnVisualChanged
            this.OnVisualChanged(e.OldValue as UIElement, e.NewValue as UIElement);
    }

    protected virtual void OnVisualChanged(UIElement? oldValue, UIElement? newValue)
    {
        this.canvas.Children.Clear();
        if (newValue is not null)
            this.canvas.Children.Add(newValue);
        this.canvas.Children.Add(this.textBlock);
    }

    public static readonly DependencyProperty VisualProperty = DependencyProperty.Register(
        nameof(Visual),
        typeof(UIElement),
        typeof(DrawingShape),
        new FrameworkPropertyMetadata(
            default(Visual),
            FrameworkPropertyMetadataOptions.AffectsMeasure));

    public UIElement? Visual
    {
        get => this.GetValue<UIElement?>(VisualProperty);
        set => this.SetValue<UIElement?>(VisualProperty, value);
    }

    private static TextBlock SetupTextBlock(DrawingShape parent)
    {
        var textBlock = new TextBlock();
        textBlock.SetBinding(TextBlock.TextProperty, new Binding() { Source = parent, Path = new PropertyPath(DrawingShape.TextProperty) });
        return textBlock;
    }

    #region Dependency properties
    public static readonly DependencyProperty TextProperty = TextBlock.TextProperty.AddOwner(typeof(DrawingShape));
    public string? Text
    {
        get => this.GetValue<string?>(TextProperty);
        set => this.SetValue<string?>(TextProperty, value);
    }

    public static readonly DependencyProperty FontSizeProperty = TextBlock.FontSizeProperty.AddOwner(typeof(DrawingShape));
    public double FontSize
    {
        get => this.GetValue<double>(FontSizeProperty);
        set => this.SetValue<double>(FontSizeProperty, value);
    }

    public static readonly DependencyProperty StrokeProperty = Shape.StrokeProperty.AddOwner(typeof(DrawingShape));
    public Brush? Stroke
    {
        get => this.GetValue<Brush?>(StrokeProperty);
        set => this.SetValue<Brush?>(StrokeProperty, value);
    }

    public static readonly DependencyProperty FillProperty = Shape.FillProperty.AddOwner(typeof(DrawingShape));
    public Brush? Fill
    {
        get => this.GetValue<Brush?>(FillProperty);
        set => this.SetValue<Brush?>(FillProperty, value);
    }

    public static readonly DependencyProperty StrokeThicknessProperty = Shape.StrokeThicknessProperty.AddOwner(typeof(DrawingShape));
    public double StrokeThickness
    {
        get => this.GetValue<double>(StrokeThicknessProperty);
        set => this.SetValue<double>(StrokeThicknessProperty, value);
    }

    public static readonly DependencyProperty StretchProperty = Shape.StretchProperty.AddOwner(typeof(DrawingShape));
    public Stretch? Stretch
    {
        get => this.GetValue<Stretch?>(StretchProperty);
        set => this.SetValue<Stretch?>(StretchProperty, value);
    }

    public static readonly DependencyProperty StrokeDashArrayProperty = Shape.StrokeDashArrayProperty.AddOwner(typeof(DrawingShape));
    public DoubleCollection? StrokeDashArray
    {
        get => this.GetValue<DoubleCollection?>(StrokeDashArrayProperty);
        set => this.SetValue<DoubleCollection?>(StrokeDashArrayProperty, value);
    }

    public static readonly DependencyProperty StrokeDashCapProperty = Shape.StrokeDashCapProperty.AddOwner(typeof(DrawingShape));
    public PenLineCap? StrokeDashCap
    {
        get => this.GetValue<PenLineCap?>(StrokeDashCapProperty);
        set => this.SetValue<PenLineCap?>(StrokeDashCapProperty, value);
    }

    public static readonly DependencyProperty StrokeDashOffsetProperty = Shape.StrokeDashOffsetProperty.AddOwner(typeof(DrawingShape));
    public double StrokeDashOffset
    {
        get => this.GetValue<double>(StrokeDashOffsetProperty);
        set => this.SetValue<double>(StrokeDashOffsetProperty, value);
    }


    public static readonly DependencyProperty StrokeEndLineCapProperty = Shape.StrokeEndLineCapProperty.AddOwner(typeof(DrawingShape));
    public PenLineCap? StrokeEndLineCap
    {
        get => this.GetValue<PenLineCap?>(StrokeEndLineCapProperty);
        set => this.SetValue<PenLineCap?>(StrokeEndLineCapProperty, value);
    }

    public static readonly DependencyProperty StrokeLineJoinProperty = Shape.StrokeLineJoinProperty.AddOwner(typeof(DrawingShape));
    public PenLineJoin? StrokeLineJoin
    {
        get => this.GetValue<PenLineJoin?>(StrokeLineJoinProperty);
        set => this.SetValue<PenLineJoin?>(StrokeLineJoinProperty, value);
    }

    public static readonly DependencyProperty StrokeMiterLimitProperty = Shape.StrokeMiterLimitProperty.AddOwner(typeof(DrawingShape));
    public double StrokeMiterLimit
    {
        get => this.GetValue<double>(StrokeMiterLimitProperty);
        set => this.SetValue<double>(StrokeMiterLimitProperty, value);
    }

    public static readonly DependencyProperty StrokeStartLineCapProperty = Shape.StrokeStartLineCapProperty.AddOwner(typeof(DrawingShape));
    public PenLineCap? StrokeStartLineCap
    {
        get => this.GetValue<PenLineCap?>(StrokeStartLineCapProperty);
        set => this.SetValue<PenLineCap?>(StrokeStartLineCapProperty, value);
    }

    public static readonly DependencyProperty FillRuleProperty = Polygon.FillRuleProperty.AddOwner(typeof(DrawingShape));
    public FillRule? FillRule
    {
        get => this.GetValue<FillRule?>(FillRuleProperty);
        set => this.SetValue<FillRule?>(FillRuleProperty, value);
    }

    public static readonly DependencyProperty LeftProperty = Canvas.LeftProperty.AddOwner(typeof(DrawingShape));
    public double Left
    {
        get => this.GetValue<double>(LeftProperty);
        set => this.SetValue<double?>(LeftProperty, value);
    }

    public static readonly DependencyProperty TopProperty = Canvas.TopProperty.AddOwner(typeof(DrawingShape));
    public double Top
    {
        get => this.GetValue<double>(TopProperty);
        set => this.SetValue<double>(TopProperty, value);
    }

    public static readonly DependencyProperty AngleProperty = RotateTransform.AngleProperty.AddOwner(typeof(DrawingShape)
        , new PropertyMetadata(1.0));
    public double Angle
    {
        get => this.GetValue<double>(AngleProperty);
        set => this.SetValue<double>(AngleProperty, value);
    }

    public static readonly DependencyProperty PointsProperty = Polygon.PointsProperty.AddOwner(typeof(DrawingShape));
    public PointCollection? Points
    {
        get => this.GetValue<PointCollection?>(PointsProperty);
        set => this.SetValue<PointCollection?>(PointsProperty, value);
    }


    #endregion Dependency properties
}