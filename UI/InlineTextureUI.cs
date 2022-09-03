using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;
using Terraria.UI;

namespace Combinations.UI
{
    public class InlineTextureUI : UIElement
    {
        private Asset<Texture2D> texture;

        private Color color;

        private Vector2 offset;

        public float Scale;

        public InlineTextureUI(string texture_path, Color color, float left = 0f, float top = 0f, float scale = 1f)
        {
            try
            {
                texture = ModContent.Request<Texture2D>(texture_path, AssetRequestMode.ImmediateLoad);
            }
            catch
            {
                texture = ModContent.Request<Texture2D>("ModLoader/UnloadedItem", AssetRequestMode.ImmediateLoad);
            }
            this.color = color;
            offset = new Vector2(left, top);
            Scale = scale;
        }

        public float getWidth()
        {
            return texture.Value.Width;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture.Value, new Vector2(Parent.Left.Pixels + offset.X, Parent.Top.Pixels + offset.Y), null, color, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }
    }
}
