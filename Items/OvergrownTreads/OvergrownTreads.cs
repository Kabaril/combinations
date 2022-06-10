using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Combinations.Items.OvergrownTreads
{
    [AutoloadEquip(EquipType.Shoes)]
    public class OvergrownTreads : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Allows flight\n" +
                "The wearer can run super fast\n" +
                "Allows the collection of Vine Rope from vines\n" +
                "Flowers grow on the grass you walk on\n" +
                "Does not decrease drop rate when breaking plants\n" +
                "Increases alchemy plant collection\n" +
                "Increases health and mana recovery");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 28;
            Item.height = 24;
            Item.value = Item.sellPrice(0, 16);
            Item.rare = ItemRarityID.Lime;
            Item.stack = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<DruidTreads.DruidTreads>();
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddIngredient(ItemID.Mushroom, 10);
            recipe.AddIngredient(ItemID.GlowingMushroom, 10);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
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

        public static int ItemType() => ModContent.ItemType<OvergrownTreads>();

        public override List<int> IncompatibleAccessories() =>
            new List<int>()
            {
                ItemType(),
                GardeningKit.GardeningKit.ItemType(),
                GardeningBoots.GardeningBoots.ItemType(),
                DruidTreads.DruidTreads.ItemType(),
            };
    }
}
