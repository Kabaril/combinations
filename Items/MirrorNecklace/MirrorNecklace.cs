using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Combinations.Items.MirrorNecklace
{
    [AutoloadEquip(EquipType.Neck)]
    public class MirrorNecklace : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases your max number of minions by 1\n" +
                "Immunity to petrification\n" +
                "Increases summon tag damage");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
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

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PocketMirror);
            recipe.AddIngredient(ItemID.PygmyNecklace);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxMinions++;
            player.buffImmune[BuffID.Stoned] = true;
        }

        public static int ItemType() => ModContent.ItemType<MirrorNecklace>();

        public override List<int> IncompatibleAccessories() =>
            new List<int>()
            {
                        ItemType(),
            };
    }
}
