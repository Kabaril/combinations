using Terraria;
using Terraria.ModLoader;
using Combinations.Items.DruidTreads;
using Combinations.Items.OvergrownTreads;
using Combinations.Items.JungleBoots;

namespace Combinations
{
    public sealed class CombinationsTile : GlobalTile
    {
        private static int[] AlchemyBuffAccessories => new int[] { DruidTreads.ItemType(), OvergrownTreads.ItemType(), JungleBoots.ItemType() };

        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (type == 84 || type == 83)
            {
                if (Helpers.HasPlayerOneOfAccessoryEquipped(Main.LocalPlayer, AlchemyBuffAccessories))
                {
                    Tile t = Main.tile[i, j];
                    int num = t.TileFrameX / 18;
                    int dropItem = 313 + num;
                    int secondaryItem = 307 + num;
                    if (num == 6)
                    {
                        dropItem = 2358;
                        secondaryItem = 2357;
                    }
                    int dropItemStack = Main.rand.Next(1, 3);
                    int secondaryItemStack = Main.rand.Next(1, 6);

                    int item1 = Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, dropItem, dropItemStack, noBroadcast: false, -1);
                    Main.item[item1].TryCombiningIntoNearbyItems(item1);
                    int item2 = Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, secondaryItem, secondaryItemStack, noBroadcast: false, -1);
                    Main.item[item2].TryCombiningIntoNearbyItems(item2);

                    return;
                }
            }
            base.KillTile(i, j, type, ref fail, ref effectOnly, ref noItem);
        }
    }
}
