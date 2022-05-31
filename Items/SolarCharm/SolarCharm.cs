﻿using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Combinations.Items.SolarCharm
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class SolarCharm : CombinationsBaseModItem
    {
        public static Asset<Texture2D> GlowMaskTexture;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Slowly regenerates life\n" +
                "Reduces the cooldown of healing potions by 25%\n" +
                "5% increased melee damage\n" +
                "Attacks deal more damage based on proximity\n" +
                "'Come a bit closer'");
            if (Main.netMode != NetmodeID.Server)
            {
                GlowMaskTexture = ModContent.Request<Texture2D>("Combinations/Items/SolarCharm/SolarCharm_HandsOn_Glow");
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
            Item.defense = 4;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FragmentSolar, 3);
            recipe.AddIngredient<CharmOfWarriors.CharmOfWarriors>();
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.pStone = true;
            player.GetDamage(DamageClass.Melee) += 0.05f;
        }

        public static int ItemType() => ModContent.ItemType<SolarCharm>();

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
                StardustCharm.StardustCharm.ItemType(),
                NebulaCharm.NebulaCharm.ItemType(),
                MasterThrowingCharm.MasterThrowingCharm.ItemType()
            };
    }
}