using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Combinations.Items.DruidTreads
{
    [AutoloadEquip(EquipType.Shoes)]
    public sealed class DruidTreads : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Allows flight\n" +
                "The wearer can run super fast\n" +
                "Allows the collection of Vine Rope from vines\n" +
                "Flowers grow on the grass you walk on\n" +
                "Does not decrease drop rate when breaking plants\n" +
                "Increases alchemy plant collection");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 28;
            Item.height = 24;
            Item.value = Item.sellPrice(0, 10, 60);
            Item.rare = ItemRarityID.Lime;
            Item.stack = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<GardeningBoots.GardeningBoots>();
            recipe.AddIngredient(ItemID.StaffofRegrowth);
            recipe.AddIngredient(ItemID.GoldBar, 10);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient<GardeningBoots.GardeningBoots>();
            recipe2.AddIngredient(ItemID.StaffofRegrowth);
            recipe2.AddIngredient(ItemID.PlatinumBar, 10);
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

        public override int[] IncompatibleAccessories() =>
            new int[]
            {
                ItemType(),
                GardeningKit.GardeningKit.ItemType(),
                GardeningBoots.GardeningBoots.ItemType(),
            };

        public static int ItemType() => ModContent.ItemType<DruidTreads>();
    }
}