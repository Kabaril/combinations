using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.GameContent.Creative;

namespace Combinations.Items.BandOfToughness
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class BandOfToughness : CombinationsBaseModItem
    {
        internal static int base_defense_value = 1;
        internal static int base_regen_value = 2;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Slowly regenerates life\n" +
                "'How tough are you?'");
            DisplayName.SetDefault("Band of Toughness");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Item shackle = Helpers.GetInitilizedDummyItem(ItemID.Shackle);
            if(shackle is not null)
            {
                base_defense_value = shackle.defense;
            }
            Item regenerationBand = Helpers.GetInitilizedDummyItem(ItemID.BandofRegeneration);
            if(regenerationBand is not null)
            {
                base_regen_value = regenerationBand.lifeRegen;
            }
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 22;
            Item.height = 22;
            Item.value = Item.sellPrice(0, 1, 50);
            Item.rare = ItemRarityID.Blue;
            Item.stack = 1;
            Item.lifeRegen = base_regen_value;
            Item.defense = base_defense_value;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Shackle);
            recipe.AddIngredient(ItemID.BandofRegeneration);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public static int ItemType() => ModContent.ItemType<BandOfToughness>();

        public override List<int> IncompatibleAccessories() =>
            new List<int>()
            {
                ItemType(),
            };
    }
}
