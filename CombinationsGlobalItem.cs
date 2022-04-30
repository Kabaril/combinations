using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Combinations.Items.CharmOfThrowing;

namespace Combinations
{
    public class CombinationsGlobalItem : GlobalItem
    {
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
                if(Helpers.HasPlayerAccessoryEquipped<CharmOfThrowing>(player))
                {
                    velocity.X *= 1.1f;
                    velocity.Y *= 1.1f;
                    damage = (int)(damage * 1.08f);

                    if(item.DamageType != DamageClass.Throwing)
                    {
                        //just convert crit to damage in this case
                        damage = (int)(damage * 1.08f);
                    }
                }
            }
            base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
    }
}
