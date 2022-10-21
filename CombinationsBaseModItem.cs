using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Combinations
{
    public abstract class CombinationsBaseModItem : ModItem
    {
        public virtual int[] IncompatibleAccessories() => Array.Empty<int>();

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (slot < 10) // This allows the accessory to equip in vanity slots with no reservations
            {
                List<int> indexes = FindDifferentEquippedExclusiveAccessories();
                if (indexes.Count > 1)
                {
                    //Cant swap here because there are multiple accessories worn that are not allowed
                    return false;
                }
                if (indexes.Count == 1)
                {
                    //swap it
                    return indexes[0] == slot;
                }
            }
            return base.CanEquipAccessory(player, slot, modded);
        }

        public List<int> FindDifferentEquippedExclusiveAccessories()
        {
            List<int> accs = new List<int>();
            int maxAccessoryIndex = 5 + Main.LocalPlayer.extraAccessorySlots;
            for (int i = 3; i < 3 + maxAccessoryIndex; i++)
            {
                Item otherAccessory = Main.LocalPlayer.armor[i];
                if (!otherAccessory.IsAir)
                {
                    if (IncompatibleAccessories().Contains(otherAccessory.type))
                    {
                        accs.Add(i);
                    }
                }
            }
            return accs;
        }
    }
}