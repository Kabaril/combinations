using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Combinations.Items.MOPPGear
{
    [AutoloadEquip(EquipType.Face)]
    public sealed class MOPPGear : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.sellPrice(0, 25);
            Item.rare = ItemRarityID.Yellow;
            Item.stack = 1;
            Item.defense = 4;
        }

        public override void AddRecipes()
        {
            //Recipe recipe = CreateRecipe();
            //recipe.AddIngredient<DeadlyEnviromentGear.DeadlyEnviromentGear>();
            //recipe.AddIngredient(ItemID.AnkhShield);
            //recipe.AddTile(TileID.TinkerersWorkbench);
            //recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.arcticDivingGear = true;
            player.accFlipper = true;
            player.accDivingHelm = true;
            player.iceSkate = true;
            player.noFallDmg = true;
            player.fireWalk = true;
            player.lavaRose = true;
            player.honeyCombItem = Item;
            player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[BuffID.Burning] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.Darkness] = true;
            player.buffImmune[BuffID.Silenced] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Stoned] = true;
            if (!hideVisual && player.armor[0].type == ItemID.None)
            {
                //prevents hair from being drawn
                player.faceHead = 1;
                //this draws natures gift flower, but is just overlayed
            }
            if (!player.wet) {
                Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.05f, 0.15f, 0.225f);
            } else {
                Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.4f, 1.2f, 1.8f);
            }
        }

        public static int ItemType() => ModContent.ItemType<MOPPGear>();

        public override int[] IncompatibleAccessories() =>
            new int[]
            {
                ItemType(),
            };
    }
}
