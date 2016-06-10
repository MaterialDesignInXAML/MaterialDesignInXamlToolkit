using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// A control which wraps itself around content and hosts the popup of <see cref="Snackbar"/> instances over this content.
    /// </summary>
    public class SnackbarHost : ContentControl
    {
        public const string SnackbarHostRootName = "SnackbarHostRoot";

        private static readonly HashSet<SnackbarHost> LoadedInstances = new HashSet<SnackbarHost>();

        public static readonly DependencyProperty IdentifierProperty = DependencyProperty.Register(nameof(Identifier), typeof(object), typeof(SnackbarHost), new PropertyMetadata(default(object)));

        /// <summary>
        /// Identifier which is used in conjunction with <see cref="Show(object)"/> to determine where a <see cref="Snackbar"/> should be shown.
        /// </summary>
        public object Identifier
        {
            get
            {
                return GetValue(IdentifierProperty);
            }

            set
            {
                SetValue(IdentifierProperty, value);
            }
        }

        private Grid _snackbarHostRoot;

        static SnackbarHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SnackbarHost), new FrameworkPropertyMetadata(typeof(SnackbarHost)));
        }

        public SnackbarHost()
        {
            Loaded += LoadedHandler;
            Unloaded += UnloadedHandler;
        }

        public override void OnApplyTemplate()
        {
            _snackbarHostRoot = (Grid)GetTemplateChild(SnackbarHostRootName);

            base.OnApplyTemplate();
        }

        private void LoadedHandler(object sender, RoutedEventArgs routedEventArgs)
        {
            LoadedInstances.Add(this);
        }

        private void UnloadedHandler(object sender, RoutedEventArgs routedEventArgs)
        {
            LoadedInstances.Remove(this);
        }

        /// <summary>
        /// Shows a <see cref="Snackbar"/>. To use, <see cref="SnackbarHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="message">The message to show in the Snackbar.</param>
        /// <returns></returns>
        public static async Task ShowAsync(string message)
        {
            await ShowAsync(null, message, null);
        }

        /// <summary>
        /// Shows a <see cref="Snackbar"/>. To use, <see cref="SnackbarHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="hostIdentifier"><see cref="Identifier"/> of the instance where the Snackbar should be shown. Typically this will match an identifer set in XAML. <c>null</c> is allowed.</param>
        /// <param name="message">The message to show in the Snackbar.</param>
        /// <returns></returns>
        public static async Task ShowAsync(object hostIdentifier, string message)
        {
            await ShowAsync(hostIdentifier, message, null);
        }

        /// <summary>
        /// Shows a <see cref="Snackbar"/>. To use, <see cref="SnackbarHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="message">The message to show in the Snackbar.</param>
        /// <param name="snackbarAction">Optional action for the Snackbar. See <see cref="SnackbarAction"/> for details.</param>
        /// <returns></returns>
        public static async Task ShowAsync(string message, SnackbarAction snackbarAction)
        {
            await ShowAsync(null, message, snackbarAction);
        }

        /// <summary>
        /// Shows a <see cref="Snackbar"/>. To use, <see cref="SnackbarHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="hostIdentifier"><see cref="Identifier"/> of the instance where the Snackbar should be shown. Typically this will match an identifer set in XAML. <c>null</c> is allowed.</param>
        /// <param name="message">The message to show in the Snackbar.</param>
        /// <param name="snackbarAction">Optional action for the Snackbar. See <see cref="SnackbarAction"/> for details.</param>
        /// <returns></returns>
        public static async Task ShowAsync(object hostIdentifier, string message, SnackbarAction snackbarAction)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Please provide a non empty value for " + nameof(message) + ".");
            }

            if (LoadedInstances.Count == 0)
            {
                throw new InvalidOperationException("No loaded SnackbarHost instances.");
            }

            LoadedInstances.First().Dispatcher.VerifyAccess();

            List<SnackbarHost> hosts = LoadedInstances.Where(host => Equals(host.Identifier, hostIdentifier)).ToList();

            if (hosts.Count == 0)
            {
                throw new InvalidOperationException("No loaded SnackbarHost have an Identifier property matching " + nameof(hostIdentifier) + " argument.");
            }

            if (hosts.Count > 1)
            {
                throw new InvalidOperationException("Multiple viable SnackbarHost. Specify a unique Identifier on each SnackbarHost, especially where multiple Windows are a concern.");
            }

            if (snackbarAction != null && string.IsNullOrWhiteSpace(snackbarAction.ActionLabel))
            {
                throw new ArgumentException("Please provide an actionLabel.");
            }

            await hosts[0].ShowInternalAsync(message, snackbarAction?.ActionLabel, snackbarAction?.ActionHandler);
        }

        private async Task ShowInternalAsync(string message, string actionLabel, SnackbarActionEventHandler actionHandler)
        {
            // find other visible Snackbars
            List<Snackbar> visibleSnackbars = _snackbarHostRoot.Children.OfType<Snackbar>().ToList();

            // if there is one, first hide it (should be at most one)
            //     use a foreach loop, because visibleSnackbars.ForEach(async visibleSnackbar => await visibleSnackbar.Hide()); will not work correctly on the UI
            foreach(Snackbar visibleSnackbar in visibleSnackbars)
            {
                await visibleSnackbar.Hide();
            }

            // create a new Snackbar, place it inside the host and show it
            Snackbar snackbar = new Snackbar() { Message = message, ActionLabel = actionLabel, ActionHandler = actionHandler };
            _snackbarHostRoot.Children.Add(snackbar);

            // a very short delay, otherwise the visual states and the transitions will not work
            await Task.Delay(10);

            await snackbar.Show();
        }

        internal void RemoveSnackbar(Snackbar snackbar)
        {
            _snackbarHostRoot.Children.Remove(snackbar);
        }
    }
}
