using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Combinations.Items.CharmOfThrowing;
using Combinations.Items.MasterThrowingCharm;
using System;

namespace Combinations
{
    public class CombinationsGlobalItem : GlobalItem
    {
        private static int[] ThrowingBuffAccessories => new int[] { CharmOfThrowing.ItemType(), MasterThrowingCharm.ItemType() };

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if(Helpers.CannotBeWornTogether(equippedItem, incomingItem))
            {
                return false;
            }
            return base.CanAccessoryBeEquippedWith(equippedItem, incomingItem, player);
        }

        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if(item.DamageType == DamageClass.Throwing || (item.DamageType == DamageClass.Ranged && item.consumable) || (item.DamageType == DamageClass.Melee && item.consumable))
            {
                if(Helpers.HasPlayerOneOfAccessoryEquipped(player, ThrowingBuffAccessories))
                {
                    velocity.X *= 1.2f;
                    velocity.Y *= 1.2f;
                    damage = (int)Math.Ceiling(damage * 1.08d);

                    if(item.DamageType != DamageClass.Throwing)
                    {
                        //just convert crit to damage in this case
                        damage = (int)Math.Ceiling(damage * 1.08d);
                    }
                }
            }
            base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
    }
}
