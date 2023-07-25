﻿using System.Collections.Generic;
using System.Numerics;
using FFXIVClientStructs.FFXIV.Client.Game;
using SortaKinda.Interfaces;
using SortaKinda.Models.Configuration;
using SortaKinda.Models.Enum;
using SortaKinda.Views.SortControllerViews;

namespace SortaKinda.System.Modules;

public class MainInventoryModule : ModuleBase
{
    public override ModuleName ModuleName { get; protected set; } = ModuleName.MainInventory;
    protected override IModuleConfig ModuleConfig { get; set; } = new MainInventoryConfig();

    private List<IInventoryGrid>? inventories;
    private QuadInventoryView? view;
    private long mainInventoryLastCount;

    protected override void Load()
    {
        inventories = new List<IInventoryGrid>();
        foreach (var config in ModuleConfig.InventoryConfigs)
        {
            inventories.Add(new InventoryGrid(config.Type, config));
        }
        
        view = new QuadInventoryView(inventories, Vector2.Zero);
    }

    public override void Draw()
    {
        view?.Draw();
    }
    
    protected override void Update()
    {
        var currentInventoryCount = InventoryController.GetInventoryItemCount(InventoryType.Inventory1, InventoryType.Inventory2, InventoryType.Inventory3, InventoryType.Inventory4);

        if (mainInventoryLastCount != currentInventoryCount)
        {
            if (SortaKindaController.SystemConfig.SortOnInventoryChange) Sort();
            mainInventoryLastCount = currentInventoryCount;
        }
    }

    protected override void Sort()
    {
        if (inventories is null) return;
        
        InventorySorter.SortInventory(InventoryType.Inventory1, inventories.ToArray());
    }
}