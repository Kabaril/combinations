using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;

namespace Combinations.UI
{
    public class CloseButton : UIPanel
    {
        public static Asset<Texture2D> Texture = ModContent.Request<Texture2D>("Combinations/UI/ButtonClose");

        public CloseButton()
        {
            Width.Set(22f, 0f);
            Height.Set(22f, 0f);
            Top.Set(2f, 0f);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture.Value, new Vector2(Parent.Left.Pixels + Parent.Width.Pixels - 22f - 2f, Parent.Top.Pixels + 2f), Color.White);
        }

        public override void Recalculate()
        {
            Left.Set(Parent.Width.Pixels - 22f - 2f, 0f);
            base.Recalculate();
        }
    }
}
