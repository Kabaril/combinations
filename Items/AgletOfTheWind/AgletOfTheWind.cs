using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.GameContent.Creative;

namespace Combinations.Items.AgletOfTheWind
{
    public class AgletOfTheWind : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("12% increased movement speed");
            DisplayName.SetDefault("Aglet of the Wind");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 22;
            Item.height = 22;
            Item.value = Item.sellPrice(0, 1, 50);
            Item.rare = ItemRarityID.Orange;
            Item.stack = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Aglet);
            recipe.AddIngredient(ItemID.AnkletoftheWind);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            CombinationsModSystem.Instance.LoadWikiString(@"
## Aglet Of The Wind ![Combinations/Items/AgletOfTheWind/AgletOfTheWind]t4

The Aglet Of The Wind grants 12 % increased movement speed.

The Aglet Of The Wind can be further upgraded into the Lightning Boots, like its parts.

### Crafting

![Combinations/Items/AgletOfTheWind/AgletOfTheWind]t4 = ![Terraria/Images/Item_212]t-4 + ![Terraria/Images/Item_285]t4
");
        }

        public override bool ConsumeItem(Player player)
        {
            return false;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += 0.12f;
        }

        public static int ItemType() => ModContent.ItemType<AgletOfTheWind>();

        public override List<int> IncompatibleAccessories() =>
            new List<int>()
            {
                ItemType(),
            };
    }
}
