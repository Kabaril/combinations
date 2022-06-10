using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Reflection;
using Terraria.GameContent.Creative;

namespace Combinations.Items.BonePile
{
    public class BonePile : CombinationsBaseModItem
    {
        private static MethodInfo _unsafe_spawn_hallucination_delegate;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots crossbones at enemies while you are attacking\n" +
                "Summons shadow hands to attack your foes\n" +
                "'You try not to think about their origin'");
            //not really great, but the alternative is rewriting a large chunk of decompiled code
            _unsafe_spawn_hallucination_delegate = typeof(Player).GetMethod("SpawnHallucination", BindingFlags.NonPublic | BindingFlags.Instance);
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

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
            if(_unsafe_spawn_hallucination_delegate is not null)
            {
                _unsafe_spawn_hallucination_delegate.Invoke(player, new object[] { Item });
            }
        }

        public static int ItemType() => ModContent.ItemType<BonePile>();

        public override List<int> IncompatibleAccessories() =>
            new List<int>()
            {
                ItemType(),
                ItemID.BoneHelm,
                ItemID.BoneGlove
            };
    }
}
