using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Combinations.Items.CharmOfThrowing;
using Combinations.Items.MasterThrowingCharm;

namespace Combinations
{
    public sealed class CombinationsGlobalItem : GlobalItem
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
            DamageClass rogue = Helpers.GetCalamityRogueDamageClass();
            bool isRogueDamage = (rogue is not null) && item.DamageType == rogue;
            if(item.DamageType == DamageClass.Throwing || isRogueDamage) {
                if(Helpers.HasPlayerOneOfAccessoryEquipped(player, ThrowingBuffAccessories))
                {
                    velocity.X *= 1.2f;
                    velocity.Y *= 1.2f;

                    //return early to avoid applying buff twice in special cases
                    base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
                    return;
                }
            }
            if(item.consumable && (item.DamageType == DamageClass.Ranged || item.DamageType == DamageClass.Melee))
            {
                if(Helpers.HasPlayerOneOfAccessoryEquipped(player, ThrowingBuffAccessories))
                {
                    velocity.X *= 1.2f;
                    velocity.Y *= 1.2f;
                    //just convert crit to damage in this case
                    damage = (int)(damage * 1.16d + 1d);
                }
            }
            base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
    }
}
