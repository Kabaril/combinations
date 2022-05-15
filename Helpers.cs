using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Combinations
{
    public static class Helpers
    {
        public static bool HasPlayerAccessoryEquipped(Player player, int type)
        {
            int maxAccessoryIndex = 5 + player.extraAccessorySlots;
            for (int i = 3; i < 3 + maxAccessoryIndex; i++)
            {
                Item accessory = player.armor[i];
                if (!accessory.IsAir)
                {
                    if (type == accessory.type)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool HasPlayerAccessoryEquipped<T>(Player player) where T : ModItem
        {
            int maxAccessoryIndex = 5 + player.extraAccessorySlots;
            for (int i = 3; i < 3 + maxAccessoryIndex; i++)
            {
                Item accessory = player.armor[i];
                if (!accessory.IsAir)
                {
                    if (accessory.ModItem is T)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsPlayerAccessoryVisible<T>(Player player) where T : ModItem
        {
            int max_vanity = player.armor.Length;
            //check if accessory is equipped in vanity slot
            for(int i = 10; i < max_vanity; i++)
            {
                Item accessory = player.armor[i];
                if (!accessory.IsAir)
                {
                    if (accessory.ModItem is T)
                    {
                        return true;
                    }
                }
            }
            int index = -1;
            int maxAccessoryIndex = 5 + player.extraAccessorySlots;
            //check if accessory is equipped in normal slot
            for (int i = 3; i < 3 + maxAccessoryIndex; i++)
            {
                Item accessory = player.armor[i];
                if (!accessory.IsAir)
                {
                    if (accessory.ModItem is T)
                    {
                        index = i;
                        break;
                    }
                }
            }
            //check if it is drawn
            if(index != -1)
            {
                int vanity_index = index + 10;
                //if something is in the vanity slot this wont get drawn
                if (!player.armor[vanity_index].IsAir)
                {
                    return false;
                }
                if(index - 3 > player.hideVisibleAccessory.Length - 1)
                {
                    return true;
                }
                return !player.hideVisibleAccessory[index];
            }
            return false;
        }

        public static bool HasPlayerOneOfAccessoryEquipped(Player player, int[] types)
        {
            int maxAccessoryIndex = 5 + player.extraAccessorySlots;
            for (int i = 3; i < 3 + maxAccessoryIndex; i++)
            {
                Item accessory = player.armor[i];
                if (!accessory.IsAir)
                {
                    if (types.Contains(accessory.type))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool CannotBeWornTogether(Item item_a, Item item_b)
        {
            if(item_a.ModItem is CombinationsBaseModItem a)
            {
                if (a.IncompatibleAccessories().Contains(item_b.type))
                {
                    return true;
                }
            }
            if(item_b.ModItem is CombinationsBaseModItem b)
            {
                if (b.IncompatibleAccessories().Contains(item_a.type))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
