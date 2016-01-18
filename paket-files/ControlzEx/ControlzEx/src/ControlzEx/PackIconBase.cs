using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ControlzEx
{
    public abstract class PackIconBase : Control
    {
        internal abstract void UpdateData();
    }

    /// <summary>
    /// Base class for creating an icon control for icon packs.
    /// </summary>
    /// <typeparam name="TKind"></typeparam>
    public abstract class PackIconBase<TKind> : PackIconBase
    {
        private static Lazy<IDictionary<TKind, string>> _dataIndex;

        /// <param name="dataIndexFactory">
        /// Inheritors should provide a factory for setting up the path data index (per icon kind).
        /// The factory will only be utilised once, across all closed instances (first instantiation wins).
        /// </param>
        protected PackIconBase(Func<IDictionary<TKind, string>> dataIndexFactory)
        {
            if (dataIndexFactory == null) throw new ArgumentNullException(nameof(dataIndexFactory));

            if (_dataIndex == null)
                _dataIndex = new Lazy<IDictionary<TKind, string>>(dataIndexFactory);
        }

        public static readonly DependencyProperty KindProperty = DependencyProperty.Register(
            "Kind", typeof(TKind), typeof(PackIconBase<TKind>), new PropertyMetadata(default(TKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((PackIconBase)dependencyObject).UpdateData();
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public TKind Kind
        {
            get { return (TKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        private static readonly DependencyPropertyKey DataPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "Data", typeof(string), typeof(PackIconBase<TKind>),
                new PropertyMetadata(default(string)));

        // ReSharper disable once StaticMemberInGenericType
        public static readonly DependencyProperty DataProperty =
            DataPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets the icon path data for the current <see cref="Kind"/>.
        /// </summary>
        public string Data
        {
            get { return (string)GetValue(DataProperty); }
            private set { SetValue(DataPropertyKey, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateData();
        }

        internal override void UpdateData()
        {
            string data = null;
            if (_dataIndex.Value != null)
                _dataIndex.Value.TryGetValue(Kind, out data);
            Data = data;
        }
    }
}
