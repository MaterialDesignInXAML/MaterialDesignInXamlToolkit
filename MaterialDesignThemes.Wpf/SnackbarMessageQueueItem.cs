using System;
using System.Collections.Generic;

namespace MaterialDesignThemes.Wpf
{
    internal class SnackbarMessageQueueItem
    {
        public SnackbarMessageQueueItem(object content, TimeSpan duration, object actionContent = null, Action<object> actionHandler = null, object actionArgument = null, 
                                        bool isPromoted = false, bool showAlways = false)
        {
            Content = content;
            Duration = duration;
            ActionContent = actionContent;
            ActionHandler = actionHandler;
            ActionArgument = actionArgument;
            IsPromoted = isPromoted;
            ShowAlways = showAlways;
        }

        /// <summary>
        /// The content to be displayed
        /// </summary>
        public object Content { get; }

        /// <summary>
        /// Message show duration.
        /// </summary>
        public TimeSpan Duration { get; }

        /// <summary>
        /// The content for the action button on the snackbar
        /// </summary>
        public object ActionContent { get; }

        /// <summary>
        /// Handler to be invoked when the action button is clicked
        /// </summary>
        public Action<object> ActionHandler { get; }

        /// <summary>
        /// The argument to pass to the <see cref="ActionHandler"/> delegate.
        /// </summary>
        public object ActionArgument { get; }

        /// <summary>
        /// Promote the message, pushing it in front of any message that is not promoted.
        /// </summary>
        public bool IsPromoted { get; }

        /// <summary>
        /// Always show this message, even if it's a duplicate
        /// </summary>
        public bool ShowAlways { get; }

        /// <summary>
        /// Last time this message was shown
        /// </summary>
        public DateTime LastShownAt { get; set; }

        public bool MessageExpired()
        {
            if (LastShownAt == null)
                return true;
            else
                return LastShownAt <= DateTime.Now.Subtract(Duration);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SnackbarMessageQueueItem message))
                return false;

            return Content.Equals(message.Content) && ActionContent.Equals(message.ActionContent);
        }

        public override int GetHashCode()
        {
            var hashCode = -724337062;
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(Content);
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(ActionContent);
            return hashCode;
        }
    }
}
