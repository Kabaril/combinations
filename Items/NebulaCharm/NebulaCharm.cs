using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Combinations.Items.NebulaCharm
{
    [AutoloadEquip(EquipType.HandsOn)]
    public sealed class NebulaCharm : CombinationsBaseModItem
    {
        public static Asset<Texture2D> GlowMaskTexture;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Slowly regenerates life\n" +
                "Reduces the cooldown of healing potions by 25%\n" +
                "Increases maximum mana by 20\n" +
                "5% increased magic damage\n" +
                "Grants a damage charging buff");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            if (Main.netMode != NetmodeID.Server)
            {
                GlowMaskTexture = ModContent.Request<Texture2D>("Combinations/Items/NebulaCharm/NebulaCharm_HandsOn_Glow");
            }
            Helpers.AddAsymmetricEquipHidden(this, EquipType.HandsOn);
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
            Item.lifeRegen = BandOfToughness.BandOfToughness.base_regen_value;
            Item.defense = BandOfToughness.BandOfToughness.base_defense_value;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FragmentNebula, 3);
            recipe.AddIngredient<CharmOfWizards.CharmOfWizards>();
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.pStone = true;
            player.statManaMax2 += 20;
            player.GetDamage(DamageClass.Magic) += 0.05f;
        }

        public static int ItemType() => ModContent.ItemType<NebulaCharm>();

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
                StardustCharm.StardustCharm.ItemType(),
                MasterThrowingCharm.MasterThrowingCharm.ItemType()
            };

        public override void EquipFrameEffects(Player player, EquipType type)
        {
            player.GetModPlayer<CombinationsPlayer>().handsOnGlowMask = GlowMaskTexture;
        }
    }
}
