using SortaKinda.Interfaces;

namespace SortaKinda.Views.Tabs;

public class ToggleFiltersTab : IOneColumnRuleConfigurationTab {
    public ToggleFiltersTab(ISortingRule rule) {
        SortingRule = rule;
    }

    public string TabName => "属性过滤";
    public bool Enabled => true;
    public ISortingRule SortingRule { get; }
    public string FirstLabel => "属性过滤";
    
    public void DrawContents() {
        SortingRule.UntradableFilter.DrawConfig();
        SortingRule.UniqueFilter.DrawConfig();
        SortingRule.DyeableFilter.DrawConfig();
        SortingRule.CollectableFilter.DrawConfig();
        SortingRule.RepairableFilter.DrawConfig();
    }
}