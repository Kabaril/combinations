using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Combinations.Items.BuildersPack
{
    [AutoloadEquip(EquipType.Back)]
    public class BuildersPack : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases mining speed by 25%\n" +
                "Increases block & wall placement speed\n" +
                "Automatically paints placed objects\n" +
                "Increases block placement & tool range by 4\n" +
                "Hold UP to reach higher");
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 28;
            Item.height = 24;
            Item.value = Item.sellPrice(0, 8);
            Item.rare = ItemRarityID.LightRed;
            Item.stack = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ArchitectGizmoPack);
            recipe.AddIngredient(ItemID.Toolbelt);
            recipe.AddIngredient(ItemID.AncientChisel);
            recipe.AddIngredient(ItemID.PortableStool);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.autoPaint = true;
            player.equippedAnyWallSpeedAcc = true;
            player.equippedAnyTileSpeedAcc = true;
            player.equippedAnyTileRangeAcc = true;
            player.autoPaint = true;
            player.blockRange++;
            player.pickSpeed -= 0.25f;
            player.portableStoolInfo.SetStats(26, 26, 26);
            if(player.whoAmI == Main.myPlayer)
            {
                Player.tileRangeX += 1;
                Player.tileRangeY += 1;
            }
        }

        public static int ItemType() => ModContent.ItemType<BuildersPack>();

        public override List<int> IncompatibleAccessories() =>
            new List<int>()
            {
                ItemType(),
            };
    }
}
