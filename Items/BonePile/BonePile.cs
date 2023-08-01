using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System.Reflection;
using Terraria.GameContent.Creative;
using System;

namespace Combinations.Items.BonePile
{
    public sealed class BonePile : CombinationsBaseModItem
    {
        //not really great, but the alternative is rewriting a large chunk of decompiled code
        private static readonly MethodInfo _unsafe_spawn_hallucination_info =
             typeof(Player).GetMethod("SpawnHallucination", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly Action<Player, Item> _unsafe_spawn_hallucination_delegate =
            (Action<Player, Item>)Delegate.CreateDelegate(typeof(Action<Player, Item>), _unsafe_spawn_hallucination_info);

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public static readonly string InlineWikiLibValue = @"## Bone Pile ![Combinations/Items/BonePile/BonePile]t-4

The Bone Pile is an Expert Mode-exclusive item.

The Bone Pile combines the effects of the Bone Helm and Bone Glove, without losing or adding a effect.

### Crafting

![Combinations/Items/BonePile/BonePile] = ![Terraria/Images/Item_5100] + ![Terraria/Images/Item_3245]
";

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 22;
            Item.height = 22;
            Item.value = Item.sellPrice(0, 4);
            Item.rare = ItemRarityID.Expert;
            Item.stack = 1;
            Item.expert = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BoneHelm);
            recipe.AddIngredient(ItemID.BoneGlove);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.boneGloveItem = Item;
            _unsafe_spawn_hallucination_delegate(player, Item);
        }

        public static int ItemType() => ModContent.ItemType<BonePile>();

        public override int[] IncompatibleAccessories() =>
            new int[]
            {
                ItemType(),
                ItemID.BoneHelm,
                ItemID.BoneGlove
            };
    }
}
