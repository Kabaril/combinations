using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Combinations.Buffs
{
    public class MirrorNecklaceBuff : ModBuff
    {
		internal static int _type_unsafe;

		public override void SetStaticDefaults()
		{
			_type_unsafe = Type;
			DisplayName.SetDefault("Mirror Hit");
			Description.SetDefault("Tagged by Mirror Necklace");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
			BuffID.Sets.IsAnNPCWhipDebuff[Type] = true;
		}
	}
}
