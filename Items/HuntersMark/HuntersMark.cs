using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Combinations.Items.HuntersMark
{
    public sealed class HuntersMark : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases pickup range for items\n" +
                "Marks enemies in ranged combat\n" +
                "'Relentless he stalks his prey'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public static readonly string InlineWikiLibValue = @"
# Hunters Mark ![Combinations/Items/HuntersMark/HuntersMark]t-8

The Hunters Mark inherits the effects of the Treasure Magnet.

The Hunters Mark applies on hit a debuff to enemies, which stacks up to four times, indicated by a Crosshair on the enemy.

Only one enemy can be affected by the debuff at once.

The following effects apply:
* 1st hit: reduces defense by 5
* 2nd hit: reduces defense by 10
* 3rd hit: reduces defense by 15
* 4th hit: reduces defense by 15 and grants a damage bonus of 5

The debuffs are applied before damage calculation, which means the effects of the 1st hit always apply.

## Crafting

![Combinations/Items/HuntersMark/HuntersMark] = ![Terraria/Images/Item_5010] + ![Terraria/Images/Item_2219]";

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.sellPrice(0, 8);
            Item.rare = ItemRarityID.Orange;
            Item.stack = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CelestialMagnet);
            recipe.AddIngredient(ItemID.TreasureMagnet);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public static int ItemType() => ModContent.ItemType<HuntersMark>();

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.treasureMagnet = true;
        }

        public override int[] IncompatibleAccessories() =>
            new int[]
            {
                ItemType(),
            };
    }
}
