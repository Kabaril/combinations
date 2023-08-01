using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.GameContent.Creative;

namespace Combinations.Items.UnholyAbomination
{
    public sealed class UnholyAbomination : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.CatchingTool[Type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            //From BugNet
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 21;
            Item.width = 24;
            Item.height = 28;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 5);
            Item.autoReuse = true;
            Item.rare = ItemRarityID.LightRed;
            Item.stack = 1;
            //From PwnHammer
            Item.hammer = 80;
            Item.useTime = 14;
            Item.damage = 26;
            Item.knockBack = 7.5f;
            Item.noMelee = false;
            //From Hamaxe
            Item.axe = 30;
            //Custom
            Item.scale = 1f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FireproofBugNet);
            recipe.AddIngredient(ItemID.Pwnhammer);
            recipe.AddIngredient(ItemID.MoltenHamaxe);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }

        public static int ItemType() => ModContent.ItemType<UnholyAbomination>();

        private List<int> UnholyAbominationSpecialCatch = new List<int> { NPCID.MagmaSnail, NPCID.Lavafly, NPCID.HellButterfly };

        public override bool? CanCatchNPC(NPC target, Player player)
        {
            if(UnholyAbominationSpecialCatch.Contains(target.type))
            {
                return true;
            }
            return base.CanCatchNPC(target, player);
        }
    }
}
