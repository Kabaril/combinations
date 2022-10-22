using Combinations.Items.CloudOutOfBottle;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Combinations
{
    public sealed class AfterFrontAccFrontAccessoryDrawLayer : PlayerDrawLayer
    {
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            if(Helpers.HasPlayerAccessoryEquipped<CloudOutOfBottle>(drawInfo.drawPlayer) && drawInfo.drawPlayer.controlJump)
            {
                DrawData item = new DrawData(
                    CloudOutOfBottle.CloudTexture.Value,
                    new Vector2(
                        (int)(drawInfo.Position.X - Main.screenPosition.X - (float)(drawInfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawInfo.drawPlayer.width / 2)),
                        (int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawInfo.drawPlayer.height - (float)drawInfo.drawPlayer.bodyFrame.Height + 4f))
                    + drawInfo.drawPlayer.bodyPosition
                    + new Vector2(drawInfo.drawPlayer.bodyFrame.Width / 3.125f, drawInfo.drawPlayer.bodyFrame.Height * 1.125f),
                    new Rectangle(0, 0, 54, 18),
                    Lighting.GetColor((int)(drawInfo.Center.X / 16), (int)(drawInfo.Center.Y / 16), Color.White),
                    drawInfo.drawPlayer.bodyRotation,
                    drawInfo.bodyVect,
                    1.125f,
                    drawInfo.playerEffect,
                    0);
                item.shader = drawInfo.cCarpet;
                drawInfo.DrawDataCache.Add(item);
            }
        }

        public AfterFrontAccFrontAccessoryDrawLayer() { }

        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Carpet);

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return !drawInfo.drawPlayer.dead && !drawInfo.drawPlayer.invis;
        }
    }
}
