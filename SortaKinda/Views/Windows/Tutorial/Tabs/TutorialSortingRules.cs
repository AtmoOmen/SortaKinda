using System.Numerics;
using Dalamud.Interface.Utility;
using ImGuiNET;
using KamiLib.Interfaces;

namespace SortaKinda.Views.Tabs;

public class TutorialSortingRules : ITabItem {
    public string TabName => "排序规则";
    public bool Enabled => true;
    public void Draw() {
        ImGuiHelpers.ScaledDummy(10.0f);
        
        ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(ImGui.GetStyle().ItemSpacing.X, 10.0f * ImGuiHelpers.GlobalScale));
        
        ImGui.TextWrapped(SortingRulesHelp);
        
        ImGui.PopStyleVar();
    }

    private const string SortingRulesHelp = "(原文基本就是 使用规则 下提及的排序情况可能有的多种不同情况, 因此这里删去不再翻译)";
}