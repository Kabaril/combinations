﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Combinations.Items.MasterThrowingCharm
{
    [AutoloadEquip(EquipType.HandsOn)]
    public sealed class MasterThrowingCharm : CombinationsBaseModItem
    {
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
            Item.rare = ItemRarityID.Yellow;
            Item.stack = 1;
            Item.lifeRegen = BandOfToughness.BandOfToughness.base_regen_value;
            Item.defense = BandOfToughness.BandOfToughness.base_defense_value;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BeetleHusk, 3);
            recipe.AddIngredient<CharmOfThrowing.CharmOfThrowing>();
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

        public static int ItemType() => ModContent.ItemType<MasterThrowingCharm>();

        public override int[] IncompatibleAccessories() =>
            new int[]
            {
                ItemType(),
                CharmOfEndurance.CharmOfEndurance.ItemType(),
                CharmOfRangers.CharmOfRangers.ItemType(),
                CharmOfThrowing.CharmOfThrowing.ItemType(),
                CharmOfWarriors.CharmOfWarriors.ItemType(),
                CharmOfWizards.CharmOfWizards.ItemType(),
                CharmOfSummoning.CharmOfSummoning.ItemType(),
                VortexCharm.VortexCharm.ItemType(),
                SolarCharm.SolarCharm.ItemType(),
                NebulaCharm.NebulaCharm.ItemType(),
                StardustCharm.StardustCharm.ItemType()
            };
    }
}
