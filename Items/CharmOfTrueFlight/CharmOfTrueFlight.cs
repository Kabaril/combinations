using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Combinations.Items.CharmOfTrueFlight
{
    public class CharmOfTrueFlight : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Grants infinite flight\n" +
            "Increases flight and jump mobility\n" +
            "'Flying is like breathing'");
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 22;
            Item.height = 22;
            Item.value = Item.sellPrice(0, 16);
            Item.rare = ItemRarityID.Master;
            Item.stack = 1;
            Item.master = true;
            Item.wingSlot = 44;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<MagicArrow.MagicArrow>();
            recipe.AddIngredient(ItemID.EmpressFlightBooster);
            recipe.AddIngredient(ItemID.RainbowWings);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.AddCondition(Terraria.Localization.NetworkText.FromLiteral("Master Mode only"), (r) => Main.masterMode);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //always sets flight to max
            player.wingTime = player.wingTimeMax;
            player.empressBrooch = true;
        }

        public static int ItemType() => ModContent.ItemType<CharmOfTrueFlight>();

        public override List<int> IncompatibleAccessories() =>
            new List<int>()
            {
                ItemType(),
            };
    }
}
