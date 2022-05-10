using Combinations.Items.AgletOfTheWind;
using Combinations.Items.MoonTablet;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Combinations
{
	public class Combinations : Mod
	{
        public override void Load()
        {
            Logger.Info("Initializing Combinations");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(ItemID.CelestialShell);
            recipe.AddIngredient<MoonTablet>();
            recipe.AddIngredient(ItemID.NeptunesShell);
            recipe.AddIngredient(ItemID.SunStone);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            Recipe recipe2 = CreateRecipe(ItemID.LightningBoots);
            recipe2.AddIngredient(ItemID.SpectreBoots);
            recipe2.AddIngredient<AgletOfTheWind>();
            recipe2.AddTile(TileID.TinkerersWorkbench);
            recipe2.Register();
            base.AddRecipes();
        }

        public override void Unload()
        {
            Logger.Info("Unloading Combinations");
        }
    }
}