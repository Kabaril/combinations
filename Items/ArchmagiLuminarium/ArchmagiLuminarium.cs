using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;

namespace Combinations.Items.ArchmagiLuminarium
{
    [AutoloadEquip(EquipType.Back, EquipType.Front)]
    public sealed class ArchmagiLuminarium : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("8% reduced mana usage\n" +
                "Automatically use mana potions when needed\n" +
                "Causes stars to fall after taking damage\n" +
                "Stars restore mana when collected\n" +
                "Increases movement speed and acceleration\n" +
                "Provides light when worn");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 26;
            Item.height = 36;
            Item.value = Item.sellPrice(0, 4);
            Item.rare = ItemRarityID.LightPurple;
            Item.stack = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ManaCloak);
            recipe.AddIngredient(ItemID.Magiluminescence);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaFlower = true;
            player.manaCost -= 0.08f;
            player.starCloakItem = Item;
            player.starCloakItem_manaCloakOverrideItem = Item;
            player.hasMagiluminescence = true;
            player.MountedCenter.ToTileCoordinates();
            //not entirely sure what this does but apparently it is needed for the lighting effect
            DelegateMethods.v3_1 = new Vector3(0.9f, 0.8f, 0.5f);
            Utils.PlotTileLine(player.Center, player.Center + player.velocity * 6f, 20f, DelegateMethods.CastLightOpen);
            Utils.PlotTileLine(player.Left, player.Right, 20f, DelegateMethods.CastLightOpen);
            player.moveSpeed -= 0.12f; //reduce movement boost to 8% for balance reasons
        }

        public static int ItemType() => ModContent.ItemType<ArchmagiLuminarium>();

        public override int[] IncompatibleAccessories() =>
            new int[]
            {
                ItemType(),
                ItemID.StarCloak,
                ItemID.ManaCloak,
                ItemID.Magiluminescence,
                TubularMagiluminescence.TubularMagiluminescence.ItemType(),
            };
    }
}
