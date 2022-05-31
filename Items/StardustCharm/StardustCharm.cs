﻿using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Combinations.Items.StardustCharm
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class StardustCharm : CombinationsBaseModItem
    {
        public static Asset<Texture2D> GlowMaskTexture;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Slowly regenerates life\n" +
                "Reduces the cooldown of healing potions by 25%\n" +
                "5% increased summon damage\n" +
                "8% increased whip speed\n" +
                "Minion attacks will make struck enemies vulnerable to you");
            if (Main.netMode != NetmodeID.Server)
            {
                GlowMaskTexture = ModContent.Request<Texture2D>("Combinations/Items/StardustCharm/StardustCharm_HandsOn_Glow");
            }
        }

        public override void Unload()
        {
            GlowMaskTexture = null;
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 22;
            Item.height = 22;
            Item.value = Item.sellPrice(0, 5);
            Item.rare = ItemRarityID.Red;
            Item.stack = 1;
            Item.lifeRegen = 2;
            Item.defense = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FragmentStardust, 3);
            recipe.AddIngredient<CharmOfSummoning.CharmOfSummoning>();
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.pStone = true;
            player.GetDamage(DamageClass.Summon) += 0.05f;
            float multiplier = 1f / 1.08f;
            player.GetAttackSpeed(DamageClass.SummonMeleeSpeed) *= multiplier;
        }

        public static int ItemType() => ModContent.ItemType<StardustCharm>();

        public override List<int> IncompatibleAccessories() =>
            new List<int>()
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
                MasterThrowingCharm.MasterThrowingCharm.ItemType()
            };
    }
}