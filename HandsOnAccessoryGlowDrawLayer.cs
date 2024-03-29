﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Combinations
{
    public sealed class HandsOnAccessoryGlowDrawLayer : PlayerDrawLayer
    {
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Asset<Texture2D> handsOnGlowMask = drawInfo.drawPlayer.GetModPlayer<CombinationsPlayer>().handsOnGlowMask;
            if (handsOnGlowMask != null)
            {
                DrawData item = new DrawData(
                    handsOnGlowMask.Value,
                    new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X - (float)(drawInfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawInfo.drawPlayer.width / 2)), (int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawInfo.drawPlayer.height - (float)drawInfo.drawPlayer.bodyFrame.Height + 4f)) + drawInfo.drawPlayer.bodyPosition + new Vector2(drawInfo.drawPlayer.bodyFrame.Width / 2, drawInfo.drawPlayer.bodyFrame.Height / 2),
                    drawInfo.drawPlayer.bodyFrame,
                    Color.White,
                    drawInfo.drawPlayer.bodyRotation,
                    drawInfo.bodyVect,
                    1f,
                    drawInfo.playerEffect,
                    0);
                item.shader = drawInfo.cHandOn;
                drawInfo.DrawDataCache.Add(item);
            }
        }

        public HandsOnAccessoryGlowDrawLayer() { }

        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.HandOnAcc);

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return !drawInfo.drawPlayer.dead && !drawInfo.drawPlayer.invis;
        }
    }
}
