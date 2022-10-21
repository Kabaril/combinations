using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Combinations.Items.HellBoots
{
    [AutoloadEquip(EquipType.Shoes)]
    public sealed class HellBoots : CombinationsBaseModItem
    {
        public override void SetStaticDefaults()
        {
            string tooltip = "Allows flight\n" +
                "Provides the ability to walk on water, honey & lava\n" +
                "Grants immunity to fire blocks and 7 seconds of immunity to lava\n" +
                "Reduces damage from touching lava\n" +
                "Leaves a trail of flames in your wake\n" +
                "If worn in the Underworld, grants minor increase to damage, attack speed, critical strike chance,\n" +
                "life regeneration and defense";
            if(Helpers.IsCalamityActive())
            {
                tooltip += "\nGrants immunity to the Burning and On Fire! debuffs";
                tooltip += "\nMultiplies all fire-based debuff damage by 1.5";
                tooltip += "\nAll attacks inflict Hellfire";
            }
            Tooltip.SetDefault(tooltip);
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 28;
            Item.height = 24;
            Item.value = Item.sellPrice(0, 14);
            Item.rare = ItemRarityID.LightRed;
            Item.stack = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HellfireTreads);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddIngredient(ItemID.RocketBoots);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            object calamity_player = Helpers.GetCalamityModPlayerInstance(player);
            if (calamity_player is not null)
            {
                Helpers.SetCalamityModPlayerAttribute(calamity_player, "hellfireTreads", true);
                player.buffImmune[BuffID.OnFire] = true;
            }
            player.accRunSpeed = 6f;
            player.rocketBoots = 1;
            player.vanityRocketBoots = 1;
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaMax += 420;
            player.lavaRose = true;
            if (player.ZoneUnderworldHeight)
            {
                player.lifeRegen += 2;
                player.statDefense += 4;
                player.GetAttackSpeed(DamageClass.Generic) += 0.1f;
                player.GetDamage(DamageClass.Generic) += 0.1f;
                player.GetCritChance(DamageClass.Generic) += 2;
            }
            if (!hideVisual && player.whoAmI == Main.myPlayer)
            {
                player.DoBootsEffect(player.DoBootsEffect_PlaceFlamesOnTile);
            }
        }

        public static int ItemType() => ModContent.ItemType<HellBoots>();

        public override int[] IncompatibleAccessories() =>
            new int[]
            {
                ItemType(),
            };
    }
}
