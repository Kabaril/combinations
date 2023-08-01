using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Combinations.Items.SlimeShield
{
    [AutoloadEquip(EquipType.Shield)]
    public sealed class SlimeShield : CombinationsBaseModItem
    {
        internal static int base_defense_value = 2;
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Item eoc_shield = Helpers.GetInitilizedDummyItem(ItemID.EoCShield);
            if (eoc_shield is not null)
            {
                base_defense_value = eoc_shield.defense;
            }
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 28;
            Item.height = 24;
            Item.value = Item.sellPrice(0, 12);
            Item.rare = ItemRarityID.Green;
            Item.stack = 1;
            Item.noMelee = false;
            Item.damage = 30;
            Item.defense = base_defense_value;
            Item.knockBack = 9f;
            Item.expert = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.EoCShield);
            recipe.AddIngredient(ItemID.RoyalGel);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.hasRaisableShield = true;
            player.npcTypeNoAggro[NPCID.BlueSlime] = true;
            player.npcTypeNoAggro[NPCID.MotherSlime] = true;
            player.npcTypeNoAggro[NPCID.LavaSlime] = true;
            player.npcTypeNoAggro[NPCID.DungeonSlime] = true;
            player.npcTypeNoAggro[NPCID.CorruptSlime] = true;
            player.npcTypeNoAggro[NPCID.IlluminantSlime] = true;
            player.npcTypeNoAggro[NPCID.Slimer] = true;
            player.npcTypeNoAggro[NPCID.Gastropod] = true;
            player.npcTypeNoAggro[NPCID.ToxicSludge] = true;
            player.npcTypeNoAggro[NPCID.IceSlime] = true;
            player.npcTypeNoAggro[NPCID.Crimslime] = true;
            player.npcTypeNoAggro[NPCID.SpikedIceSlime] = true;
            player.npcTypeNoAggro[NPCID.SpikedJungleSlime] = true;
            player.npcTypeNoAggro[NPCID.UmbrellaSlime] = true;
            player.npcTypeNoAggro[NPCID.RainbowSlime] = true;
            player.npcTypeNoAggro[NPCID.SlimeMasked] = true;
            player.npcTypeNoAggro[NPCID.SlimeRibbonWhite] = true;
            player.npcTypeNoAggro[NPCID.SlimeRibbonYellow] = true;
            player.npcTypeNoAggro[NPCID.SlimeRibbonGreen] = true;
            player.npcTypeNoAggro[NPCID.SlimeRibbonRed] = true;
            player.npcTypeNoAggro[NPCID.SandSlime] = true;
            player.npcTypeNoAggro[NPCID.HoppinJack] = true;
            player.npcTypeNoAggro[NPCID.GoldenSlime] = true;
            player.npcTypeNoAggro[NPCID.SlimeSpiked] = true;
            //player.npcTypeNoAggro[676] = true;
            player.dashType = 2;
        }

        public static int ItemType() => ModContent.ItemType<SlimeShield>();

        public override int[] IncompatibleAccessories() =>
            new int[]
            {
                ItemType(),
                ItemID.EoCShield
            };
    }
}
