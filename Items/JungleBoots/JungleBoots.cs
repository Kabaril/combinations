using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Combinations.Items.JungleBoots
{
    [AutoloadEquip(EquipType.Shoes)]
    public class JungleBoots : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Allows flight\n" +
                "The wearer can run super fast\n" +
                "Allows the collection of Vine Rope from vines\n" +
                "Flowers grow on the grass you walk on\n" +
                "Does not decrease drop rate when breaking plants\n" +
                "Increases alchemy plant collection\n" +
                "Increases health and mana recovery\n" +
                "Summons spores over time that will damage enemies");
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 28;
            Item.height = 24;
            Item.value = Item.sellPrice(0, 20);
            Item.rare = ItemRarityID.Expert;
            Item.stack = 1;
            Item.expert = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<OvergrownTreads.OvergrownTreads>();
            recipe.AddIngredient(ItemID.SporeSac);
            recipe.AddIngredient(ItemID.JungleSpores, 10);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.cordage = true;
            player.accRunSpeed = 6f;
            player.rocketBoots = 2;
            player.vanityRocketBoots = 2;
            player.SporeSac(Item);
            player.sporeSac = true;
            if (player.whoAmI == Main.myPlayer)
            {
                player.DoBootsEffect(player.DoBootsEffect_PlaceFlowersOnTile);
            }
        }

        public static int ItemType() => ModContent.ItemType<JungleBoots>();

        public override List<int> IncompatibleAccessories() =>
            new List<int>()
            {
                ItemType(),
                ItemID.SporeSac,
                GardeningKit.GardeningKit.ItemType(),
                GardeningBoots.GardeningBoots.ItemType(),
                DruidTreads.DruidTreads.ItemType(),
                OvergrownTreads.OvergrownTreads.ItemType(),
            };
    }
}
