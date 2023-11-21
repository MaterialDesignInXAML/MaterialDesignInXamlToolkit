using System.Windows.Automation.Peers;

namespace MaterialDesignThemes.Wpf.Automation.Peers;

public class TreeListViewAutomationPeer : ListViewAutomationPeer
{
    public TreeListViewAutomationPeer(TreeListView owner) : base(owner)
    {
    }

    protected override ItemAutomationPeer CreateItemAutomationPeer(object item)
        => new TreeListViewItemAutomationPeer(item, this);
}

public class TreeListViewItemAutomationPeer : ListBoxItemAutomationPeer
{
    public TreeListViewItemAutomationPeer(object owner, SelectorAutomationPeer selectorAutomationPeer)
        : base(owner, selectorAutomationPeer)
    {
    }

    public override object GetPattern(PatternInterface patternInterface)
    {
        if (patternInterface == PatternInterface.ExpandCollapse)
        {
            return this;
        }
        return base.GetPattern(patternInterface);
    }
}
