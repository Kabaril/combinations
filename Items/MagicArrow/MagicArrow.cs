using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Combinations.Items.MagicArrow
{
    public sealed class MagicArrow : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public static readonly string InlineWikiLibValue = @"
# Magic Arrow ![Combinations/Items/MagicArrow/MagicArrow]t-4

The Magic Arrow allows the wearer to continue flight for Mana.

For one second of Flight 90 Mana must be expended,

meaning if the player has a maximum of 200 Mana the flight can be extended for 2.22 seconds

If the Player runs out of Mana and has the Mana Flower (or similiar),

a Mana recovery Potion is consumed automatically.

If the player has the Mana Sickness debuff no Potion is consumed.

The Magic Arrow can be obtained by fishing in Hardmode.

To get the Magic Arrow the optimal Fishing Power is 220 to 250.";

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 22;
            Item.height = 22;
            Item.value = Item.sellPrice(0, 6);
            Item.rare = ItemRarityID.LightRed;
            Item.stack = 1;
        }

        public static int ItemType() => ModContent.ItemType<MagicArrow>();

        public override int[] IncompatibleAccessories() =>
            new int[]
            {
                ItemType(),
            };
    }
}
