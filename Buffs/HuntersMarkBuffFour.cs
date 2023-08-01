using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Combinations.Buffs
{
    public sealed class HuntersMarkBuffFour : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 15;
            base.Update(player, ref buffIndex);
        }
    }
}
