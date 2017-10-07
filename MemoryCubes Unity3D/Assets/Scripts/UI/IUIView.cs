using System;

public interface IUIView
{
    void Show();
    void Hide();

    UIViewType UIViewType { get; }
}