using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Combinations.Buffs
{
    public class StardustCharmBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stardust Charm");
			Description.SetDefault("Tagged by Stardust Charm");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
		}
	}
}
