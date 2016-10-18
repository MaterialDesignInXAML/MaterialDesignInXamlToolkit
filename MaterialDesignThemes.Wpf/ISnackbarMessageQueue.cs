using System;

namespace MaterialDesignThemes.Wpf
{
    public interface ISnackbarMessageQueue
    {
        void Enqueue(object content);

        void Enqueue(object content, object actionContent, Action actionHandler);

        void Enqueue<TArgument>(object content, object actionContent, Action<TArgument> actionHandler, TArgument actionArgument);

        //TODO consider additional variants of Enqueue:
        // ShowAsync(. . .)
    }
}