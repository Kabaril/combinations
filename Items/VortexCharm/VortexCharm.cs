using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Combinations.Items.VortexCharm
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class VortexCharm : CombinationsBaseModItem
    {
        public static Asset<Texture2D> GlowMaskTexture;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Slowly regenerates life\n" +
                "Reduces the cooldown of healing potions by 25%\n" +
                "5% increased ranged damage\n" +
                "5% ranged critical strike chance\n" +
                "Applies random effects when striking enemies");
            if (Main.netMode != NetmodeID.Server)
            {
                GlowMaskTexture = ModContent.Request<Texture2D>("Combinations/Items/VortexCharm/VortexCharm_HandsOn_Glow");
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
            recipe.AddIngredient(ItemID.FragmentVortex, 3);
            recipe.AddIngredient<CharmOfRangers.CharmOfRangers>();
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.pStone = true;
            player.GetDamage(DamageClass.Ranged) += 0.05f;
            player.GetCritChance(DamageClass.Ranged) += 5;
        }

        public static int ItemType() => ModContent.ItemType<VortexCharm>();

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
                StardustCharm.StardustCharm.ItemType(),
                SolarCharm.SolarCharm.ItemType(),
                NebulaCharm.NebulaCharm.ItemType(),
                MasterThrowingCharm.MasterThrowingCharm.ItemType()
            };
    }
}
