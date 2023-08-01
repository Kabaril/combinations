using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace Combinations.Items.CharmOfTrueFlight
{
    public sealed class CharmOfTrueFlight : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 22;
            Item.height = 22;
            Item.value = Item.sellPrice(0, 16);
            Item.rare = ItemRarityID.Master;
            Item.stack = 1;
            Item.master = true;
            Item.wingSlot = 44;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<MagicArrow.MagicArrow>();
            recipe.AddIngredient(ItemID.EmpressFlightBooster);
            recipe.AddIngredient<CrownOfLight.CrownOfLight>();
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.AddCondition(Language.GetOrRegister("Mods.Combinations.Misc.MasterModeOnly"), () => Main.masterMode);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //always sets flight to max
            player.wingTime = player.wingTimeMax;
            player.empressBrooch = true;
        }

        public static int ItemType() => ModContent.ItemType<CharmOfTrueFlight>();

        public override int[] IncompatibleAccessories() =>
            new int[]
            {
                ItemType(),
            };
    }
}
