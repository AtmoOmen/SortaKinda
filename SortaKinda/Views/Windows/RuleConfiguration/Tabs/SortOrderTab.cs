using System;
using Dalamud.Interface.Components;
using Dalamud.Interface.Utility;
using ImGuiNET;
using KamiLib.Utility;
using SortaKinda.Interfaces;

namespace SortaKinda.Views.Tabs;

public class SortOrderTab : ITwoColumnRuleConfigurationTab {
    public SortOrderTab(ISortingRule rule) {
        SortingRule = rule;
    }

    public string TabName => "排序顺序";
    public bool Enabled => true;
    public ISortingRule SortingRule { get; }
    public string FirstLabel => "排序依据: ";
    public string SecondLabel => "排序选项";

    public void DrawLeftSideContents() {
        var sortMode = SortingRule.SortMode;
        DrawRadioEnum(ref sortMode);

        SortingRule.SortMode = sortMode;
    }

    public void DrawRightSideContents() {
        ImGui.Text("排序依据: ");
        ImGuiComponents.HelpMarker("依据拼音首字母");
        var sortDirection = SortingRule.Direction;
        DrawRadioEnum(ref sortDirection);

        ImGuiHelpers.ScaledDummy(8.0f);
        ImGui.Text("填充顺序:");
        ImGuiComponents.HelpMarker("上: 物品先填充进区域的左上角\n下: 物品先填充进区域的右下角");
        var fillMode = SortingRule.FillMode;
        DrawRadioEnum(ref fillMode);

        SortingRule.Direction = sortDirection;
        SortingRule.FillMode = fillMode;
    }

    private static void DrawRadioEnum<T>(ref T configValue) where T : Enum {
        foreach (Enum value in Enum.GetValues(configValue.GetType())) {
            var isSelected = Convert.ToInt32(configValue);
            if (ImGui.RadioButton($"{value.Label()}##{configValue.GetType()}", ref isSelected, Convert.ToInt32(value))) {
                configValue = (T) value;
            }
        }
    }
}