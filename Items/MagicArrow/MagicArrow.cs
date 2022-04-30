using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Combinations.Items.MagicArrow
{
    public class MagicArrow : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Flight can be extended for Mana\n" +
                "'Fly with me - and be free'");
        }

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

        public override List<int> IncompatibleAccessories() =>
            new List<int>()
            {
                ItemType(),
            };
    }
}
