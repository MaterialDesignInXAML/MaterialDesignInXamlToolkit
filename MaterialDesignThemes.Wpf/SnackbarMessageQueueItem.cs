using System;

namespace MaterialDesignThemes.Wpf
{
    internal class SnackbarMessageQueueItem
    {
        public SnackbarMessageQueueItem(object content, object actionContent = null, object actionHandler = null, object actionArgument = null, Type argumentType = null, bool isPromoted = false)
        {
            Content = content;
            ActionContent = actionContent;
            ActionHandler = actionHandler;
            ActionArgument = actionArgument;
            ArgumentType = argumentType;
            IsPromoted = isPromoted;
        }

        public object Content { get; }

        public object ActionContent { get; }

        public object ActionHandler { get; }

        public object ActionArgument { get; }

        public Type ArgumentType { get; }

        public bool IsPromoted { get; }
    }
}