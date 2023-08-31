using Combinations.Items.AgletOfTheWind;
using Combinations.Items.MoonTablet;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Combinations.Items.MOPPGear;
using Combinations.Items.DeadlyEnviromentGear;

namespace Combinations
{
    public sealed class CombinationsModSystem : ModSystem
    {
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemID.CelestialShell);
            recipe.AddIngredient<MoonTablet>();
            recipe.AddIngredient(ItemID.NeptunesShell);
            recipe.AddIngredient(ItemID.SunStone);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

            Recipe recipe2 = Recipe.Create(ItemID.LightningBoots);
            recipe2.AddIngredient(ItemID.SpectreBoots);
            recipe2.AddIngredient<AgletOfTheWind>();
            recipe2.AddTile(TileID.TinkerersWorkbench);
            recipe2.Register();

            Recipe recipe3 = Recipe.Create(ItemID.FlameWakerBoots);
            recipe3.AddIngredient(ItemID.SailfishBoots);
            recipe3.AddIngredient(ItemID.HellstoneBar, 15);
            recipe3.AddTile(TileID.Hellforge);
            recipe3.Register();

            Recipe recipe4 = Recipe.Create(ItemID.JellyfishNecklace);
            recipe4.AddIngredient(ItemID.Gel, 25);
            recipe4.AddIngredient(ItemID.Chain);
            recipe4.AddIngredient(ItemID.GoldBar, 5);
            recipe4.AddTile(TileID.TinkerersWorkbench);
            recipe4.Register();

            Recipe recipe5 = Recipe.Create(ItemID.JellyfishNecklace);
            recipe5.AddIngredient(ItemID.Gel, 25);
            recipe5.AddIngredient(ItemID.Chain);
            recipe5.AddIngredient(ItemID.PlatinumBar, 5);
            recipe5.AddTile(TileID.TinkerersWorkbench);
            recipe5.Register();

            Recipe recipe6 = Recipe.Create(ItemID.DivingHelmet);
            recipe6.AddIngredient(ItemID.IronBar, 10);
            recipe6.AddIngredient(ItemID.Glass, 5);
            recipe6.AddIngredient(ItemID.SharkFin);
            recipe6.AddTile(TileID.TinkerersWorkbench);
            recipe6.Register();

            Recipe recipe7 = Recipe.Create(ItemID.DivingHelmet);
            recipe7.AddIngredient(ItemID.LeadBar, 10);
            recipe7.AddIngredient(ItemID.Glass, 5);
            recipe7.AddIngredient(ItemID.SharkFin);
            recipe7.AddTile(TileID.TinkerersWorkbench);
            recipe7.Register();

            Recipe recipe8 = Recipe.Create(ItemID.BottomlessLavaBucket);
            recipe8.AddIngredient(ItemID.LavaBucket, 25);
            recipe8.AddTile(TileID.TinkerersWorkbench);
            recipe8.Register();

            Recipe recipe9 = Recipe.Create(ItemID.BottomlessBucket);
            recipe9.AddIngredient(ItemID.WaterBucket, 25);
            recipe9.AddTile(TileID.TinkerersWorkbench);
            recipe9.Register();

            Recipe recipe10 = Recipe.Create(ItemID.SuperAbsorbantSponge);
            recipe10.AddIngredient(ItemID.Silk, 25);
            recipe10.AddTile(TileID.TinkerersWorkbench);
            recipe10.Register();

            Recipe recipe11 = Recipe.Create(ItemID.LavaAbsorbantSponge);
            recipe11.AddIngredient(ItemID.SuperAbsorbantSponge);
            recipe11.AddIngredient(ItemID.MagmaStone);
            recipe11.AddTile(TileID.TinkerersWorkbench);
            recipe11.Register();

            if(CombinationsConfig.Instance.EnableUnfinishedItems) {
                AddExperimentalRecipes();
            }

            base.AddRecipes();
        }

        private void AddExperimentalRecipes() {
            Recipe recipe = Recipe.Create(ModContent.ItemType<MOPPGear>());
            recipe.AddIngredient<DeadlyEnviromentGear>();
            recipe.AddIngredient(ItemID.AnkhShield);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void AddRecipeGroups()
        {
            if (!RecipeGroup.recipeGroupIDs.ContainsKey(nameof(ItemID.CloudinaBottle)))
            {
                RecipeGroup group = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.CloudinaBottle)}", ItemID.SandstorminaBottle, ItemID.CloudinaBottle, ItemID.FartinaJar, ItemID.BlizzardinaBottle, ItemID.TsunamiInABottle);
                RecipeGroup.RegisterGroup(nameof(ItemID.CloudinaBottle), group);
            }

            base.AddRecipeGroups();
        }
    }
}
