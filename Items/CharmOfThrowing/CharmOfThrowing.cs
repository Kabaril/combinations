using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace Combinations.Items.CharmOfThrowing
{
    [AutoloadEquip(EquipType.HandsOn)]
    public sealed class CharmOfThrowing : CombinationsBaseModItem
    {
        public override LocalizedText Tooltip {
            get {
                if(Helpers.IsCalamityActive()) {
                    return Language.GetOrRegister("Mods.Combinations.Interop.Calamity.CharmOfThrowing.Tooltip");
                }
                return Language.GetOrRegister("Mods.Combinations.Items.CharmOfThrowing.Tooltip");
            }
        }

        private static DamageClass rogueDamageClass = null;

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Helpers.AddAsymmetricEquipHidden(this, EquipType.HandsOn);
            rogueDamageClass = Helpers.GetCalamityRogueDamageClass();
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
            recipe.AddIngredient(ItemID.Amber, 3);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.pStone = true;
            player.GetCritChance(DamageClass.Throwing) += 8;
            player.GetDamage(DamageClass.Throwing) *= 1.08f;
            if(rogueDamageClass is not null) {
                player.GetCritChance(rogueDamageClass) += 8;
                player.GetDamage(rogueDamageClass) *= 1.08f;
            }
        }

        public static int ItemType() => ModContent.ItemType<CharmOfThrowing>();

        public override int[] IncompatibleAccessories() => new int[]
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
