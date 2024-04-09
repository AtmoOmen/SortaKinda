using System.Numerics;
using Dalamud.Interface.Utility;
using ImGuiNET;
using KamiLib.Interfaces;

namespace SortaKinda.Views.Tabs;

public class TutorialAboutTab : ITabItem {
    public string TabName => "关于";
    public bool Enabled => true;
    public void Draw() {
        ImGuiHelpers.ScaledDummy(10.0f);
        
        ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(ImGui.GetStyle().ItemSpacing.X, 10.0f * ImGuiHelpers.GlobalScale));
        
        ImGui.TextWrapped(AboutText);
        
        ImGui.PopStyleVar();
    }

    private const string AboutText = "欢迎使用 SortaKinda! 一个可高度自定义的背包整理工具\n" +
                                     "此插件用于精细化分类整理你的背包空间, 让特定类型的物品能够始终待在同一区域\n\n" +
                                     "插件与游戏原生的 isort 指令无关, 也不会与其产生任何交集\n" +
                                     "插件有相较其他任何背包整理分类系统来说更高的优先级\n\n" +
                                     "你可以在设置中的 一般 标签页内手动触发整理逻辑, 这些逻辑也会在你配置好的时间点触发, 以帮助自动整理背包.\n\n" +
                                     "有的时候插件可能会错过一些物品变动, 但一般来说整理逻辑会被频繁触发, 因此无需担心";
}