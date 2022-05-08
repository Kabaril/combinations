using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Combinations.Items.MoonTablet
{
    public class MoonTablet : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Turns the holder into a werewolf at night\n" +
                "If worn during the night, grants minor increase to damage, melee speed, critical strike chance,\n" +
                "life regeneration, defense, mining speed, and minion knockback");
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 22;
            Item.height = 22;
            Item.value = Item.sellPrice(0, 10, 50);
            Item.rare = ItemRarityID.LightPurple;
            Item.stack = 1;
            Item.canBePlacedInVanityRegardlessOfConditions = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MoonStone);
            recipe.AddIngredient(ItemID.MoonCharm);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Main.dayTime || Main.eclipse)
            {
                player.skyStoneEffects = true;
            }
            player.wolfAcc = true;
            player.hideWolf = hideVisual;
            base.UpdateAccessory(player, hideVisual);
        }

        public override void UpdateVanity(Player player)
        {
            player.hideWolf = false;
            player.forceWerewolf = true;
            base.UpdateVanity(player);
        }

        public static int ItemType() => ModContent.ItemType<MoonTablet>();

        public override List<int> IncompatibleAccessories() =>
            new List<int>()
            {
                ItemType(),
            };
    }
}
