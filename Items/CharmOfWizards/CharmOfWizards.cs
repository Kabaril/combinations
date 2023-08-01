using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Combinations.Items.CharmOfWizards
{
    [AutoloadEquip(EquipType.HandsOn)]
    public sealed class CharmOfWizards : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Helpers.AddAsymmetricEquipHidden(this, EquipType.HandsOn);
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
            recipe.AddIngredient(ItemID.Sapphire, 3);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.pStone = true;
            player.statManaMax2 += 20;
            player.GetDamage(DamageClass.Magic) += 0.05f;
        }

        public static int ItemType() => ModContent.ItemType<CharmOfWizards>();


        public override int[] IncompatibleAccessories() => new int[]
        {
            ItemType(),
            CharmOfEndurance.CharmOfEndurance.ItemType(),
            CharmOfRangers.CharmOfRangers.ItemType(),
            CharmOfSummoning.CharmOfSummoning.ItemType(),
            CharmOfThrowing.CharmOfThrowing.ItemType(),
            CharmOfWarriors.CharmOfWarriors.ItemType(),
        };
    }
}
