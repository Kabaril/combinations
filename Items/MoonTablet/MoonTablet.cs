using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace Combinations.Items.MoonTablet
{
    public sealed class MoonTablet : CombinationsBaseModItem
    {
        internal static int? NightwitherDebuffType = null;

        public override LocalizedText Tooltip {
            get {
                if(Helpers.IsCalamityActive()) {
                    return Language.GetOrRegister("Mods.Combinations.Interop.Calamity.MoonTablet.Tooltip");
                }
                return Language.GetOrRegister("Mods.Combinations.Items.MoonTablet.Tooltip");
            }
        }

        public override void SetStaticDefaults()
        {
            if (ModContent.TryFind("CalamityMod/Nightwither", out ModBuff NightwitherDebuff)) {
                NightwitherDebuffType = NightwitherDebuff.Type;
            }
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 22;
            Item.height = 22;
            Item.value = Item.sellPrice(0, 10, 50);
            Item.rare = ItemRarityID.LightPurple;
            Item.stack = 1;
            Item.hasVanityEffects = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MoonStone);
            recipe.AddIngredient(ItemID.MoonCharm);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Main.dayTime || Main.eclipse)
            {
                player.skyStoneEffects = true;
            }
            player.wolfAcc = true;
            player.hideWolf = hideVisual;
            if (NightwitherDebuffType is not null)
            {
                player.buffImmune[NightwitherDebuffType.Value] = true;
            }
            base.UpdateAccessory(player, hideVisual);
        }

        public override void UpdateVanity(Player player)
        {
            player.hideWolf = false;
            player.forceWerewolf = true;
            base.UpdateVanity(player);
        }

        public static int ItemType() => ModContent.ItemType<MoonTablet>();

        public override int[] IncompatibleAccessories() =>
            new int[]
            {
                ItemType(),
            };
    }
}
