using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Combinations.Buffs
{
    public sealed class NebulaCharmBuff : ModBuff
    {
		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = false;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
		}
	}
}
