using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;

namespace Combinations.UI
{
    public class CloseButton : UIImageButton
    {
        public static Asset<Texture2D> Texture = ModContent.Request<Texture2D>("Combinations/UI/ButtonClose");

        public CloseButton(Asset<Texture2D> texture) : base(texture){}

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture.Value, new Vector2(Parent.Left.Pixels + Parent.Width.Pixels - 22f - 2f, Parent.Top.Pixels + 2f), Color.White);
        }
    }
}
