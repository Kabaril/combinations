using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Combinations.Items.CharmOfThrowing
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class CharmOfThrowing : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Slowly regenerates life\n" +
                "Reduces the cooldown of healing potions by 25%\n" +
                "8% increased throwing damage\n" +
                "8% increased throwing critical strike chance\n" +
                "15% increased throwing velocity\n" +
                "'The path ahead is unclear'");
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 22;
            Item.height = 22;
            Item.value = Item.sellPrice(0, 5);
            Item.rare = ItemRarityID.LightPurple;
            Item.stack = 1;
            Item.lifeRegen = 2;
            Item.defense = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<CharmOfEndurance.CharmOfEndurance>();
            recipe.AddIngredient(ItemID.Amber, 3);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.pStone = true;
            player.GetCritChance(DamageClass.Throwing) += 8;
        }

        public static int ItemType() => ModContent.ItemType<CharmOfThrowing>();

        public override List<int> IncompatibleAccessories() => new List<int>()
        {
            ItemType(),
            CharmOfEndurance.CharmOfEndurance.ItemType(),
            CharmOfRangers.CharmOfRangers.ItemType(),
            CharmOfSummoning.CharmOfSummoning.ItemType(),
            CharmOfWarriors.CharmOfWarriors.ItemType(),
            CharmOfWizards.CharmOfWizards.ItemType(),
        };
    }
}
