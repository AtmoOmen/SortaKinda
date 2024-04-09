using System.Numerics;
using Dalamud.Interface.Utility;
using ImGuiNET;
using KamiLib.Interfaces;

namespace SortaKinda.Views.Tabs;

public class TutorialConfiguringInventory : ITabItem {
    public string TabName => "使用规则";
    public bool Enabled => true;
    public void Draw() {
        ImGuiHelpers.ScaledDummy(10.0f);
        
        ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(ImGui.GetStyle().ItemSpacing.X, 10.0f * ImGuiHelpers.GlobalScale));
        
        ImGui.TextWrapped(UsingRulesHelp);
        
        ImGui.PopStyleVar();
    }

    private const string UsingRulesHelp = "要使用规则, 先得选中一个分类, 每个分类都会有自己唯一的颜色标识,\n" +
                                          "选中分类后你就可以在右侧的模拟物品栏界面中绘制分类所要应用的区域, 区域可以不连续\n" +
                                          "每当开始整理时, 相关物品会先移入这些区域, 然后根据分类所定义的规则来排序\n" +
                                          "与所有已知分类都无关的物品会被移入 Unsorted 分类区域, 你必须将部分物品槽定义为 Unsorted 分类";
}