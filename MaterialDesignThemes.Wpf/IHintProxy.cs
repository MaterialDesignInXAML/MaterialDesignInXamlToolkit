﻿namespace MaterialDesignThemes.Wpf;

/// <summary>
/// This interface is the adapter from UiControl (like <see cref="TextBox"/>, <see cref="ComboBox"/> and others) to <see cref="SmartHint"/>
/// <para/>
/// You should implement this interface in order to use SmartHint for your own control.
/// </summary>
public interface IHintProxy : IDisposable
{
    /// <summary>
    /// Checks to see if the targeted control can be deemed as logically 
    /// empty, even if not null, affecting the current hint display.
    /// </summary>
    /// <returns></returns>
    bool IsEmpty();

    /// <summary>
    /// Targeted control has keyboard focus
    /// </summary>
    /// <returns></returns>
    bool IsFocused();

    bool IsLoaded { get; }

    bool IsVisible { get; }

    event EventHandler? ContentChanged;

    event EventHandler? IsVisibleChanged;

    event EventHandler? Loaded;

    event EventHandler? FocusedChanged;
}
