using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Combinations.Items.GardeningBoots
{
    public class GardeningBoots : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Allows flight\n" +
                "The wearer can run super fast\n" +
                "Allows the collection of Vine Rope from vines\n" +
                "Flowers grow on the grass you walk on\n" +
                "Does not decrease drop rate when breaking plants");
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 28;
            Item.height = 24;
            Item.value = Item.sellPrice(0, 0, 50) + Item.sellPrice(0, 10);
            Item.rare = ItemRarityID.Pink;
            Item.stack = 1;
            Item.shoeSlot = 16;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<GardeningKit.GardeningKit>();
            recipe.AddIngredient(ItemID.SpectreBoots);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.CordageGuide);
            recipe2.AddIngredient(ItemID.FairyBoots);
            recipe2.AddTile(TileID.TinkerersWorkbench);
            recipe2.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.cordage = true;
            player.accRunSpeed = 6f;
            player.rocketBoots = 2;
            player.vanityRocketBoots = 2;
            if (player.whoAmI == Main.myPlayer)
            {
                player.DoBootsEffect(player.DoBootsEffect_PlaceFlowersOnTile);
            }
        }

        public static int ItemType() => ModContent.ItemType<GardeningBoots>();

        public override List<int> IncompatibleAccessories() =>
            new List<int>()
            {
                ItemType(),
                GardeningKit.GardeningKit.ItemType(),
            };
    }
}
