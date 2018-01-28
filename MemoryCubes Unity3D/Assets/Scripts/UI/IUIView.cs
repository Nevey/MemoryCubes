using System;

public interface IUIView
{
    void Show();
    void Hide();

    UIViewID UIViewID { get; }
}