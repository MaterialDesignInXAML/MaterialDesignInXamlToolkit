using System;

namespace MaterialDesignThemes.Wpf
{
    public interface ISnackbarMessageQueue
    {
        /// <summary>
        /// Queues a notification message for display in a snackbar.
        /// </summary>
        /// <param name="content">Message.</param>
        void Enqueue(object content);

        /// <summary>
        /// Queues a notification message for display in a snackbar.
        /// </summary>
        /// <param name="content">Message.</param>
        /// <param name="actionContent">Content for the action button.</param>
        /// <param name="actionHandler">Call back to be executed if user clicks the action button.</param>
        void Enqueue(object content, object? actionContent, Action? actionHandler);

        /// <summary>
        /// Queues a notification message for display in a snackbar.
        /// </summary>
        /// <param name="content">Message.</param>
        /// <param name="actionContent">Content for the action button.</param>
        /// <param name="actionHandler">Call back to be executed if user clicks the action button.</param>
        /// <param name="actionArgument">Argument to pass to <paramref name="actionHandler"/>.</param>
        void Enqueue<TArgument>(object content, object? actionContent, Action<TArgument?>? actionHandler, TArgument? actionArgument);

        /// <summary>
        /// Queues a notification message for display in a snackbar.
        /// </summary>
        /// <param name="content">Message.</param>
        /// <param name="neverConsiderToBeDuplicate">Subsequent, duplicate messages queued within a short time span will 
        /// be discarded. To override this behaviour and ensure the message always gets displayed set to <c>true</c>.</param>
        void Enqueue(object content, bool neverConsiderToBeDuplicate);

        /// <summary>
        /// Queues a notification message for display in a snackbar.
        /// </summary>
        /// <param name="content">Message.</param>
        /// <param name="actionContent">Content for the action button.</param>
        /// <param name="actionHandler">Call back to be executed if user clicks the action button.</param>
        /// <param name="promote">The message will promoted to the front of the queue.</param>
        void Enqueue(object content, object? actionContent, Action? actionHandler, bool promote);

        /// <summary>
        /// Queues a notification message for display in a snackbar.
        /// </summary>
        /// <param name="content">Message.</param>
        /// <param name="actionContent">Content for the action button.</param>
        /// <param name="actionHandler">Call back to be executed if user clicks the action button.</param>
        /// <param name="actionArgument">Argument to pass to <paramref name="actionHandler"/>.</param>
        /// <param name="promote">The message will be promoted to the front of the queue and never considered to be a duplicate.</param>
        void Enqueue<TArgument>(object content, object? actionContent, Action<TArgument?>? actionHandler, TArgument? actionArgument, bool promote);

        /// <summary>
        /// Queues a notification message for display in a snackbar.
        /// </summary>
        /// <param name="content">Message.</param>
        /// <param name="actionContent">Content for the action button.</param>
        /// <param name="actionHandler">Call back to be executed if user clicks the action button.</param>
        /// <param name="actionArgument">Argument to pass to <paramref name="actionHandler"/>.</param>
        /// <param name="promote">The message will be promoted to the front of the queue.</param>
        /// <param name="neverConsiderToBeDuplicate">The message will never be considered a duplicate.</param>
        /// <param name="durationOverride">Message show duration override.</param>
        void Enqueue<TArgument>(object content, object? actionContent, Action<TArgument?>? actionHandler,
            TArgument? actionArgument, bool promote, bool neverConsiderToBeDuplicate, TimeSpan? durationOverride = null);

        /// <summary>
        /// Queues a notification message for display in a snackbar.
        /// </summary>
        /// <param name="content">Message.</param>
        /// <param name="actionContent">Content for the action button.</param>
        /// <param name="actionHandler">Call back to be executed if user clicks the action button.</param>
        /// <param name="actionArgument">Argument to pass to <paramref name="actionHandler"/>.</param>
        /// <param name="promote">The message will promoted to the front of the queue.</param>
        /// <param name="neverConsiderToBeDuplicate">The message will never be considered a duplicate.</param>
        /// <param name="durationOverride">Message show duration override.</param>
        void Enqueue(object content, object? actionContent, Action<object?>? actionHandler, object? actionArgument,
            bool promote, bool neverConsiderToBeDuplicate, TimeSpan? durationOverride = null);
    }
}