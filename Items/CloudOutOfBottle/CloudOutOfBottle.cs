using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Combinations.Items.CloudOutOfBottle
{
    public sealed class CloudOutOfBottle : CombinationsBaseModItem
    {
        public static Asset<Texture2D> CloudTexture;

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            if (Main.netMode != NetmodeID.Server)
            {
                CloudTexture = ModContent.Request<Texture2D>("Combinations/Items/CloudOutOfBottle/CloudOutOfBottle_Texture");
            }
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.sellPrice(0, 4);
            Item.rare = ItemRarityID.Green;
            Item.stack = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(nameof(ItemID.CloudinaBottle), 3);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public static int ItemType() => ModContent.ItemType<CloudOutOfBottle>();

        public override int[] IncompatibleAccessories() =>
            new int[]
            {
                ItemType(),
            };
    }
}
