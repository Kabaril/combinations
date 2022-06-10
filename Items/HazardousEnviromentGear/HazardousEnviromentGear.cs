using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Combinations.Items.HazardousEnviromentGear
{
    [AutoloadEquip(EquipType.Face)]
    public class HazardousEnviromentGear : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Grants the ability to swim and greatly extends underwater breathing\n" +
            "Provides extra mobility on ice\n" +
            "Generates a very subtle glow which becomes more vibrant underwater\n" +
            "Grants immunity to fire blocks, poison and fall damage\n" +
            "Releases bees and douses the user in honey when damaged\n" +
            "'H.E.V. Gear'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.sellPrice(0, 10, 50);
            Item.rare = ItemRarityID.Orange;
            Item.stack = 1;
            Item.defense = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HoneyComb);
            recipe.AddIngredient(ItemID.ObsidianHorseshoe);
            recipe.AddIngredient(ItemID.ArcticDivingGear);
            recipe.AddIngredient(ItemID.Bezoar);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.arcticDivingGear = true;
            player.accFlipper = true;
            player.accDivingHelm = true;
            player.iceSkate = true;
            player.noFallDmg = true;
            player.fireWalk = true;
            if (!hideVisual && player.armor[0].type == ItemID.None)
            {
                //prevents hair from being drawn
                player.faceHead = 1;
                //this draws natures gift flower, but is just overlayed
            }
            player.honeyCombItem = Item;
            player.buffImmune[BuffID.Poisoned] = true;
            if (!player.wet) {
                Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.05f, 0.15f, 0.225f);
            } else {
                Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.4f, 1.2f, 1.8f);
            }
        }

        public static int ItemType() => ModContent.ItemType<HazardousEnviromentGear>();

        public override List<int> IncompatibleAccessories() =>
            new List<int>()
            {
                ItemType(),
            };
    }
}
