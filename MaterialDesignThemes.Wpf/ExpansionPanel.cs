using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Collections;

namespace MaterialDesignThemes.Wpf
{
    public class ExpansionPanel : ListBox
    {
        public ExpansionPanel()
        {

        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ExpansionPanelItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ExpansionPanelItem();
        }

        public static readonly DependencyProperty ItemHeaderTemplateProperty = DependencyProperty.Register(
            nameof(ItemHeaderTemplate), typeof(DataTemplate), typeof(ExpansionPanel), new PropertyMetadata(default(DataTemplate)));


        public DataTemplate ItemHeaderTemplate
        {
            get => (DataTemplate)GetValue(ItemHeaderTemplateProperty);
            set => SetValue(ItemHeaderTemplateProperty, value);
        }
    }
        
    public class ExpansionPanelItem : ListBoxItem
    {
        public ExpansionPanelItem()
        {

        }

        //public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register(
        //    nameof(HeaderTemplate), typeof(DataTemplate), typeof(ExpansionPanelItem), new PropertyMetadata(default(DataTemplate)));

        //public DataTemplate HeaderTemplate
        //{
        //    get => (DataTemplate)GetValue(HeaderTemplateProperty);
        //    set => SetValue(HeaderTemplateProperty, value);
        //}
        
    }
}