using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Combinations.Items.BuildersPack
{
    [AutoloadEquip(EquipType.Back)]
    public sealed class BuildersPack : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 28;
            Item.height = 24;
            Item.value = Item.sellPrice(0, 8);
            Item.rare = ItemRarityID.LightRed;
            Item.stack = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ArchitectGizmoPack);
            recipe.AddIngredient(ItemID.Toolbelt);
            recipe.AddIngredient(ItemID.AncientChisel);
            recipe.AddIngredient(ItemID.PortableStool);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.autoPaint = true;
            player.equippedAnyWallSpeedAcc = true;
            player.equippedAnyTileSpeedAcc = true;
            player.equippedAnyTileRangeAcc = true;
            player.autoPaint = true;
            player.blockRange++;
            player.pickSpeed -= 0.25f;
            player.portableStoolInfo.SetStats(26, 26, 26);
            if(player.whoAmI == Main.myPlayer)
            {
                Player.tileRangeX += 1;
                Player.tileRangeY += 1;
            }
        }

        public static int ItemType() => ModContent.ItemType<BuildersPack>();

        public override int[] IncompatibleAccessories() =>
            new int[]
            {
                ItemType(),
            };
    }
}
