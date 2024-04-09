using System.Numerics;
using Dalamud.Interface.Utility;
using ImGuiNET;
using KamiLib.Interfaces;

namespace SortaKinda.Views.Tabs;

public class TutorialAdvancedSorting : ITabItem {
    public string TabName => "进阶技巧";
    public bool Enabled => true;
    public void Draw() {
        ImGuiHelpers.ScaledDummy(10.0f);
        
        ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(ImGui.GetStyle().ItemSpacing.X, 10.0f * ImGuiHelpers.GlobalScale));
        
        ImGui.TextWrapped(AdvancedTech);

        ImGui.PopStyleVar();
    }

    private const string AdvancedTech = "SortaKinda 会依据特定顺序应用排序规则, " +
                                        "这样你就可以通过组合多个规则以让满足多个排序规则的物品始终待在特定区域\n\n" +
                                        "排序规则总是从列表最上方开始生效, 直至最下方, " +
                                        "如果一个物品同时满足多个排序条件的话, 那么排在最下方的才是真正控制其位置的分类\n\n" +
                                        "你可以使用插件的这一特性来让一般的排序规则分类位于最顶上, 而精细化的分类位于最底下";
}