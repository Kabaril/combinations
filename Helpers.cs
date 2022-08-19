using System;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace Combinations
{
    [Autoload(false)]
    public static class Helpers
    {
        public static void Unload()
        {
            _Calamity_Mod_Player_Type_Is_Scanned = false;
            _Calamity_Mod_Player_Type = null;
            _Calamity_Mod_Player_Method = null;
        }

        public static bool HasPlayerItemInInventory(Player player, int type)
        {
            foreach(Item item in player.inventory)
            {
                if (!item.IsAir)
                {
                    if (type == item.type)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool HasPlayerItemInInventory<T>(Player player) where T : ModItem
        {
            foreach (Item item in player.inventory)
            {
                if (!item.IsAir)
                {
                    if (item.ModItem is T)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

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

        public static Item GetInitilizedDummyItem(int type)
        {
            try
            {
                Item item = new Item();
                item.SetDefaults(type);
                return item;
            } catch
            {
                return null;
            }
        }

        private static bool _Calamity_Mod_Player_Type_Is_Scanned = false;
        private static Type _Calamity_Mod_Player_Type = null;
        private static Type GetCalamityModPlayer()
        {
            if(!_Calamity_Mod_Player_Type_Is_Scanned)
            {
                if (ModContent.TryFind("CalamityMod/CalamityPlayer", out ModPlayer calamityPlayer))
                {
                    _Calamity_Mod_Player_Type = calamityPlayer.GetType();
                }
                _Calamity_Mod_Player_Type_Is_Scanned = true;
            }
            return _Calamity_Mod_Player_Type;
        }

        public static bool IsCalamityActive()
        {
            Type calamity_player_type = GetCalamityModPlayer();
            if(calamity_player_type is not null)
            {
                return true;
            }
            return false;
        }

        public static object GetCalamityModPlayerInstance(Player player)
        {
            Type calamity_player_type = GetCalamityModPlayer();
            if(calamity_player_type is null)
            {
                return null;
            }
            MethodInfo get_calamity_mod_player_instance = GetCalamityPlayerMethod(calamity_player_type);
            return get_calamity_mod_player_instance.Invoke(player, Array.Empty<object>());
        }

        public static void SetCalamityModPlayerAttribute<T>(object calamity_mod_player, string property_name, T value)
        {
            Type calamity_player_type = GetCalamityModPlayer();
            if (calamity_player_type is null)
            {
                return;
            }
            FieldInfo field = calamity_player_type.GetField(property_name);
            if(field is null)
            {
                return;
            }
            if(field.FieldType != value.GetType())
            {
                return;
            }
            try
            {
                field.SetValue(calamity_mod_player, value);
            }
            catch
            {

            }
        }

        private static MethodInfo _Calamity_Mod_Player_Method = null;

        private static MethodInfo GetCalamityPlayerMethod(Type calamity_player_type)
        {
            if(_Calamity_Mod_Player_Method is null)
            {
                MethodInfo method = typeof(Player).GetMethod("GetModPlayer", BindingFlags.Instance | BindingFlags.Public, Type.EmptyTypes);
                _Calamity_Mod_Player_Method = method.MakeGenericMethod(calamity_player_type);
            }
            return _Calamity_Mod_Player_Method;
        }
    }
}
