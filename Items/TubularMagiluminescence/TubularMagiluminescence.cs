using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Combinations.Items.TubularMagiluminescence
{
    [AutoloadEquip(EquipType.Waist)]
    public sealed class TubularMagiluminescence : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Grants the ability to float in water\n" +
                "Increases movement speed and acceleration\n" +
                "Provides light when worn\n" +
                "'How exactly does this float?'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.buyPrice(0, 1) + Item.sellPrice(0, 1);
            Item.rare = ItemRarityID.Green;
            Item.stack = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Magiluminescence);
            recipe.AddIngredient(ItemID.FloatingTube);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.hasMagiluminescence = true;
            player.MountedCenter.ToTileCoordinates();
            //not entirely sure what this does but apparently it is needed for the lighting effect
            DelegateMethods.v3_1 = new Vector3(0.9f, 0.8f, 0.5f);
            Utils.PlotTileLine(player.Center, player.Center + player.velocity * 6f, 20f, DelegateMethods.CastLightOpen);
            Utils.PlotTileLine(player.Left, player.Right, 20f, DelegateMethods.CastLightOpen);
            player.hasFloatingTube = true;
            player.canFloatInWater = true;
        }

        public static int ItemType() => ModContent.ItemType<TubularMagiluminescence>();

        public override int[] IncompatibleAccessories() =>
            new int[]
            {
                ItemType(),
                ItemID.Magiluminescence,
                ItemID.FloatingTube,
            };
    }
}
