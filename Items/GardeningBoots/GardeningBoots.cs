using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Combinations.Items.GardeningBoots
{
    public sealed class GardeningBoots : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
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
            player.cordage = !CombinationsConfig.Instance.DisableVineRopeForBoots;
            player.accRunSpeed = 6f;
            player.rocketBoots = 2;
            player.vanityRocketBoots = 2;
            if (player.whoAmI == Main.myPlayer)
            {
                player.DoBootsEffect(player.DoBootsEffect_PlaceFlowersOnTile);
            }
        }

        public static int ItemType() => ModContent.ItemType<GardeningBoots>();

        public override int[] IncompatibleAccessories() =>
            new int[]
            {
                ItemType(),
                GardeningKit.GardeningKit.ItemType(),
            };
    }
}
