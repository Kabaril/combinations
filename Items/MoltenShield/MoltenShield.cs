using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System.Collections.Generic;
using Terraria.GameContent.Creative;
using System;

namespace Combinations.Items.MoltenShield
{
    [AutoloadEquip(EquipType.Shield)]
    public class MoltenShield : CombinationsBaseModItem
    {
        internal static int base_defense_value = 2;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Melee attacks inflict fire damage\n" +
                "Grants immunity to fire blocks\n" +
                "Reduces damage from touching lava\n" +
                "Grants immunity to knockback\n"+
                "Nearby enemies are ignited\n" +
                "'Any Hellknight needs one'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Item skull_rose = Helpers.GetInitilizedDummyItem(ItemID.MoltenSkullRose);
            if(skull_rose is not null)
            {
                base_defense_value = skull_rose.defense;
            }
            Item obsidian_shield = Helpers.GetInitilizedDummyItem(ItemID.ObsidianShield);
            if(obsidian_shield is not null)
            {
                base_defense_value = Math.Max(base_defense_value, obsidian_shield.defense);
            }
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 28;
            Item.height = 24;
            Item.value = Item.sellPrice(0, 7, 0);
            Item.rare = ItemRarityID.LightPurple;
            Item.stack = 1;
            Item.defense = base_defense_value;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MoltenSkullRose);
            recipe.AddIngredient(ItemID.CobaltShield);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.MoltenSkullRose);
            recipe2.AddIngredient(ItemID.ObsidianShield);
            recipe2.AddTile(TileID.TinkerersWorkbench);
            recipe2.Register();
        }

        public static int ItemType() => ModContent.ItemType<MoltenShield>();

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noKnockback = true;
            player.fireWalk = true;
            player.lavaRose = true;
            player.magmaStone = true;
            player.inferno = !CombinationsConfig.Instance.HideMoltenShieldRing && !hideVisual;
            player.hasRaisableShield = true;
            Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0.65f, 0.4f, 0.1f);
            if (player.whoAmI != Main.myPlayer)
            {
                return;
            }
            int buff_immune_index = 24;
            float buff_radius = 200f;
            bool flag = player.infernoCounter % 60 == 0;
            int damage = 10;
            for (int k = 0; k < 200; k++)
            {
                NPC nPC = Main.npc[k];
                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && !nPC.buffImmune[buff_immune_index] && player.CanNPCBeHitByPlayerOrPlayerProjectile(nPC) && Vector2.Distance(player.Center, nPC.Center) <= buff_radius)
                {
                    if (nPC.FindBuffIndex(buff_immune_index) == -1)
                    {
                        nPC.AddBuff(buff_immune_index, 120);
                    }
                    if (flag)
                    {
                        player.ApplyDamageToNPC(nPC, damage, 0f, 0, crit: false);
                    }
                }
            }
            if (!player.hostile)
            {
                return;
            }
            for (int l = 0; l < 255; l++)
            {
                Player player2 = Main.player[l];
                if (player2 == player || !player2.active || player2.dead || !player2.hostile || player2.buffImmune[buff_immune_index] || (player2.team == player.team && player2.team != 0) || !(Vector2.Distance(player.Center, player2.Center) <= buff_radius))
                {
                    continue;
                }
                if (player2.FindBuffIndex(buff_immune_index) == -1)
                {
                    player2.AddBuff(buff_immune_index, 120);
                }
                if (flag)
                {
                    player2.Hurt(PlayerDeathReason.LegacyEmpty(), damage, 0, pvp: true);
                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {
                        PlayerDeathReason reason = PlayerDeathReason.ByOther(16);
                        NetMessage.SendPlayerHurt(l, reason, damage, 0, critical: false, pvp: true, -1);
                    }
                }
            }
        }

        public override List<int> IncompatibleAccessories() =>
            new List<int>()
            {
                ItemType(),
                ItemID.ObsidianShield,
                ItemID.CobaltShield,
                ItemID.ObsidianSkull,
            };
    }
}
