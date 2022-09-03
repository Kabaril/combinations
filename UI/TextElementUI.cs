using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameContent;
using Terraria.UI;

namespace Combinations.UI
{
    public class TextElementUI : UIElement
    {
        private Color color;
        private DynamicSpriteFont font;
        public string Text;
        public Vector2 Offset;
        public float Scale;

        public TextElementUI(string text = null, Vector2? offset = null, Color? color = null, float scale = 1f, DynamicSpriteFont font = null)
        {
            if (color.HasValue)
            {
                this.color = color.Value;
            } else
            {
                this.color = Color.White;
            }
            if(text is not null)
            {
                Text = text;
            } else
            {
                Text = "";
            }
            if(font is not null)
            {
                this.font = font;
            } else
            {
                this.font = FontAssets.MouseText.Value;
            }
            if(offset.HasValue)
            {
                Offset = offset.Value;
            } else
            {
                Offset = Vector2.Zero;
            }
            Scale = scale;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, Text, new Vector2(Parent.Left.Pixels + Offset.X, Parent.Top.Pixels + Offset.Y), color, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }
    }
}
