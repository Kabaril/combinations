﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Combinations.Items.JungleBoots
{
    [AutoloadEquip(EquipType.Shoes)]
    public sealed class JungleBoots : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 28;
            Item.height = 24;
            Item.value = Item.sellPrice(0, 20);
            Item.rare = ItemRarityID.Expert;
            Item.stack = 1;
            Item.expert = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<OvergrownTreads.OvergrownTreads>();
            recipe.AddIngredient(ItemID.SporeSac);
            recipe.AddIngredient(ItemID.JungleSpores, 10);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.cordage = !CombinationsConfig.Instance.DisableVineRopeForBoots;
            player.accRunSpeed = 6f;
            player.rocketBoots = 2;
            player.vanityRocketBoots = 2;
            player.SporeSac(Item);
            player.sporeSac = true;
            if (player.whoAmI == Main.myPlayer)
            {
                player.DoBootsEffect(player.DoBootsEffect_PlaceFlowersOnTile);
            }
        }

        public static int ItemType() => ModContent.ItemType<JungleBoots>();

        public override int[] IncompatibleAccessories() =>
            new int[]
            {
                ItemType(),
                ItemID.SporeSac,
                GardeningKit.GardeningKit.ItemType(),
                GardeningBoots.GardeningBoots.ItemType(),
                DruidTreads.DruidTreads.ItemType(),
                OvergrownTreads.OvergrownTreads.ItemType(),
            };
    }
}
