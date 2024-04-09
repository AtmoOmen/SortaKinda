﻿using SortaKinda.Interfaces;
using SortaKinda.Models.Enums;
using SortaKinda.System;

namespace SortaKinda.Views.Tabs;

public class MainInventoryTab : IInventoryConfigurationTab {
    public string TabName => "物品栏";
    public bool Enabled => true;
    public void DrawInventory() => SortaKindaController.ModuleController.DrawModule(ModuleName.MainInventory);
}