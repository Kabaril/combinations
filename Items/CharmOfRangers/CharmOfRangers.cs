using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Combinations.Items.CharmOfRangers
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class CharmOfRangers : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Slowly regenerates life\n" +
                "Reduces the cooldown of healing potions by 25%\n" +
                "5% increased ranged damage\n" +
                "5% ranged critical strike chance");
            DisplayName.SetDefault("Charm of Rangers");
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
            recipe.AddIngredient<CharmOfEndurance.CharmOfEndurance>();
            recipe.AddIngredient(ItemID.Diamond, 3);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.pStone = true;
            player.GetDamage(DamageClass.Ranged) += 0.05f;
            player.GetCritChance(DamageClass.Ranged) += 5;
        }

        public static int ItemType() => ModContent.ItemType<CharmOfRangers>();

        public override List<int> IncompatibleAccessories() => new List<int>()
        {
            ItemType(),
            CharmOfEndurance.CharmOfEndurance.ItemType(),
            CharmOfSummoning.CharmOfSummoning.ItemType(),
            CharmOfThrowing.CharmOfThrowing.ItemType(),
            CharmOfWarriors.CharmOfWarriors.ItemType(),
            CharmOfWizards.CharmOfWizards.ItemType(),
        };
    }
}
