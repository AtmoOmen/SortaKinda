using KamiLib.Interfaces;
using SortaKinda.System;

namespace SortaKinda.Views.Tabs;

public class GeneralConfigurationTab : ITabItem {
    public string TabName => "一般";
    public bool Enabled => true;
    public void Draw() => SortaKindaController.DrawConfig();
}