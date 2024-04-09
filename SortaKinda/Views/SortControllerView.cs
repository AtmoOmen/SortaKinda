using System;
using System.Linq;
using Dalamud.Interface;
using Dalamud.Interface.Utility;
using Dalamud.Utility;
using ImGuiNET;
using KamiLib.Game;
using Newtonsoft.Json;
using SortaKinda.Interfaces;
using SortaKinda.Models;
using SortaKinda.Views.Windows;

namespace SortaKinda.Views.SortControllerViews;

public class SortControllerView {
    private readonly SortingRuleListView listView;
    private readonly ISortController sortController;

    public SortControllerView(ISortController sortingController) {
        sortController = sortingController;
        listView = new SortingRuleListView(sortingController, sortingController.Rules);
    }

    public void Draw() {
        DrawHeader();
        DrawRules();
    }

    private void DrawHeader() {
        var importExportButtonSize = ImGuiHelpers.ScaledVector2(23.0f, 23.0f);
        var sortButtonSize = ImGuiHelpers.ScaledVector2(100.0f, 23.0f);

        ImGui.SetCursorPosY(ImGui.GetCursorPosY() + 3.0f * ImGuiHelpers.GlobalScale);
        ImGui.TextUnformatted("排序规则");

        ImGui.SameLine(ImGui.GetContentRegionAvail().X - importExportButtonSize.X * 3.0f - sortButtonSize.X - ImGui.GetStyle().ItemSpacing.X * 3.0f);
        ImGui.PushFont(UiBuilder.IconFont);
        if (ImGui.Button($"{FontAwesomeIcon.Question.ToIconString()}##HelpButton", importExportButtonSize)) {
            TutorialWindow.Instance.Open();
        }
        ImGui.PopFont();
        if (ImGui.IsItemHovered()) ImGui.SetTooltip("打开帮助窗口");

        ImGui.SameLine();
        ImGui.PushFont(UiBuilder.IconFont);
        if (ImGui.Button($"{FontAwesomeIcon.Clipboard.ToIconString()}##ImportButton", importExportButtonSize)) {
            ImportRules();
        }
        ImGui.PopFont();
        if (ImGui.IsItemHovered()) ImGui.SetTooltip("从剪贴板导入");

        ImGui.SameLine();
        ImGui.PushFont(UiBuilder.IconFont);
        if (ImGui.Button($"{FontAwesomeIcon.ExternalLinkAlt.ToIconString()}##ImportButton", importExportButtonSize)) {
            ExportRules();
        }
        ImGui.PopFont();
        if (ImGui.IsItemHovered()) ImGui.SetTooltip("导出至剪贴板");

        ImGui.SameLine();
        if (ImGui.Button("手动排序", sortButtonSize)) {
            sortController.SortAllInventories();
        }

        ImGui.Separator();
    }

    private void ImportRules() {
        try {
            var decodedString = Convert.FromBase64String(ImGui.GetClipboardText());
            var uncompressed = Util.DecompressString(decodedString);

            if (uncompressed.IsNullOrEmpty()) {
                Chat.PrintError("导入排序规则失败, 请重试");
                return;
            }

            if (JsonConvert.DeserializeObject<SortingRule[]>(uncompressed) is { } rules) {
                if (rules.Length is 0) {
                    Chat.PrintError("导入排序规则失败, 请失败");
                    return;
                }

                var addedCount = 0;
                foreach (var rule in rules) {
                    if (!sortController.Rules.Any(existingRule => existingRule.Id == rule.Id)) {
                        rule.Index = sortController.Rules.Count;
                        sortController.Rules.Add(rule);
                        addedCount++;
                    }
                }

                Chat.Print("导入", $"从剪贴板接收了 {rules.Length} 条排序规则");
                Chat.Print("导入", $"添加了 {addedCount} 条新的排序规则");
                sortController.SaveConfig();
            }
        }
        catch {
            Chat.PrintError("导入过程中发生未知错误");
        }
    }

    private void ExportRules() {
        var rules = sortController.Rules.ToArray()[1..];
        var jsonString = JsonConvert.SerializeObject(rules);
        
        var compressed = Util.CompressString(jsonString);
        ImGui.SetClipboardText(Convert.ToBase64String(compressed));

        Chat.Print("导出", $"导出了 {rules.Length} 条规则至剪贴板");
    }

    private void DrawRules() => listView.Draw();
}