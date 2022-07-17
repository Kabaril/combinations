using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Combinations.Items.CharmOfEndurance
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class CharmOfEndurance : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Slowly regenerates life\n" +
                "Reduces the cooldown of healing potions by 25%");
            DisplayName.SetDefault("Charm of Endurance");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 22;
            Item.height = 22;
            Item.value = Item.sellPrice(0, 5);
            Item.rare = ItemRarityID.LightPurple;
            Item.stack = 1;
            Item.lifeRegen = BandOfToughness.BandOfToughness.base_regen_value;
            Item.defense = BandOfToughness.BandOfToughness.base_defense_value;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<BandOfToughness.BandOfToughness>();
            recipe.AddIngredient(ItemID.PhilosophersStone);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.CharmofMyths);
            recipe2.AddIngredient(ItemID.Shackle);
            recipe2.AddTile(TileID.TinkerersWorkbench);
            recipe2.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.pStone = true;
        }

        public static int ItemType() => ModContent.ItemType<CharmOfEndurance>();

        public override List<int> IncompatibleAccessories() =>
            new List<int>()
            {
                        ItemType(),
            };
    }
}
