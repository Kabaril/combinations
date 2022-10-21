using Terraria;
using Terraria.ModLoader;
using Combinations.Buffs;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;
using Combinations.Items.CrownOfLight;

namespace Combinations
{
    public sealed class CombinationsGlobalNPC : GlobalNPC
    {
        public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if(damage == 0.0)
            {
                return false;
            }
            if(npc.HasBuff<HuntersMarkBuffOne>())
            {
                defense -= 5;
            }
            if (npc.HasBuff<HuntersMarkBuffTwo>())
            {
                defense -= 10;
            }
            if (npc.HasBuff<HuntersMarkBuffThree>() || npc.HasBuff<HuntersMarkBuffFour>())
            {
                defense -= 15;
            }
            if(defense < 0)
            {
                defense = 0;
            }
            damage = Main.CalculateDamageNPCsTake((int)damage, defense);
            if (crit)
            {
                damage *= 2.0;
            }
            if (npc.takenDamageMultiplier > 1f)
            {
                damage *= npc.takenDamageMultiplier;
            }
            return false;
        }

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (npc.HasBuff<HuntersMarkBuffOne>())
            {
                Texture2D texture = HuntersMarkBuffOne.buff_hit_texture.Value;
                Vector2 pos = GetCenterScreenPos(npc, texture);
                spriteBatch.Draw(texture, pos, new Color(100, 20, 8));
            }
            if (npc.HasBuff<HuntersMarkBuffTwo>())
            {
                Texture2D texture = HuntersMarkBuffOne.buff_hit_texture.Value;
                Vector2 pos = GetCenterScreenPos(npc, texture);
                spriteBatch.Draw(texture, pos, new Color(150, 20, 8));
            }
            if (npc.HasBuff<HuntersMarkBuffThree>())
            {
                Texture2D texture = HuntersMarkBuffOne.buff_hit_texture.Value;
                Vector2 pos = GetCenterScreenPos(npc, texture);
                spriteBatch.Draw(texture, pos, new Color(200, 20, 8));
            }
            if (npc.HasBuff<HuntersMarkBuffFour>())
            {
                Texture2D texture = HuntersMarkBuffOne.buff_hit_texture.Value;
                Vector2 pos = GetCenterScreenPos(npc, texture);
                spriteBatch.Draw(texture, pos, new Color(250, 20, 8));
            }
            base.PostDraw(npc, spriteBatch, screenPos, drawColor);
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.HallowBoss)
            {
                npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<CrownOfLight>()));
            }
            base.ModifyNPCLoot(npc, npcLoot);
        }

        private static Vector2 GetCenterScreenPos(NPC npc, Texture2D texture)
        {
            Vector2 pos = npc.VisualPosition;
            int height = npc.height;
            int width = npc.width;
            pos.X += width / 2f;
            pos.Y += height / 2f;
            pos.X -= texture.Width / 2f;
            pos.Y -= texture.Height / 2f;
            return pos - Main.screenPosition;
        }
    }
}
