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
        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if(npc.HasBuff<HuntersMarkBuffOne>())
            {
                modifiers.Defense -= 5;
            }
            if (npc.HasBuff<HuntersMarkBuffTwo>())
            {
                modifiers.Defense -= 10;
            }
            if (npc.HasBuff<HuntersMarkBuffThree>() || npc.HasBuff<HuntersMarkBuffFour>())
            {
                modifiers.Defense -= 15;
            }
            base.ModifyIncomingHit(npc, ref modifiers);
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
