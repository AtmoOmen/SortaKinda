using System;
using Dalamud.Interface.Components;
using ImGuiNET;
using KamiLib.Utility;
using SortaKinda.Interfaces;
using SortaKinda.Models.Enums;

namespace SortaKinda.Views.Tabs;

public class OtherFiltersTab : ITwoColumnRuleConfigurationTab {
    public OtherFiltersTab(ISortingRule rule) {
        SortingRule = rule;
    }

    public string TabName => "其他过滤";
    public bool Enabled => true;
    public ISortingRule SortingRule { get; }
    public string FirstLabel => "范围过滤";
    public string SecondLabel => "物品稀有度过滤";

    public void DrawLeftSideContents() {
        SortingRule.ItemLevelFilter.DrawConfig();
        SortingRule.VendorPriceFilter.DrawConfig();
    }

    public void DrawRightSideContents() {
        foreach (var enumValue in Enum.GetValues<ItemRarity>()) {
            var enabled = SortingRule.AllowedItemRarities.Contains(enumValue);
            if (ImGuiComponents.ToggleButton($"{enumValue.Label()}", ref enabled)) {
                if (enabled) SortingRule.AllowedItemRarities.Add(enumValue);
                if (!enabled) SortingRule.AllowedItemRarities.Remove(enumValue);
            }

            ImGui.SameLine();
            ImGui.SetCursorPosY(ImGui.GetCursorPosY() + 3.0f);
            ImGui.TextUnformatted(enumValue.Label());
        }
    }
}