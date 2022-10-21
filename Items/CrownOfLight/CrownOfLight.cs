using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Combinations.Items.CrownOfLight
{
    public sealed class CrownOfLight : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crown of Light");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
            Item.value = Terraria.Item.sellPrice(0, 5);
            Item.rare = ItemRarityID.Master;
            Item.stack = 1;
            Item.material = true;
        }
    }
}
