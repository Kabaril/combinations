using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Combinations.Buffs
{
    public class NebulaCharmBuff : ModBuff
    {
		internal static int _type_unsafe;

		public override void SetStaticDefaults()
		{
			_type_unsafe = Type;
			DisplayName.SetDefault("Galactic Power");
			Description.SetDefault("Magic Power is increasing");
			Main.debuff[Type] = false;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
		}
	}
}
