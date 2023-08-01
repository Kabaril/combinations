using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Combinations.Buffs
{
    public sealed class MirrorNecklaceBuff : ModBuff
    {
		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
			BuffID.Sets.IsAnNPCWhipDebuff[Type] = true;
		}
	}
}
