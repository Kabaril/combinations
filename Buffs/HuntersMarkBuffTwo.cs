using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Combinations.Buffs
{
    public class HuntersMarkBuffTwo : ModBuff
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hunters Mark");
			Description.SetDefault("The hunt begins");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            int def = player.statDefense;
            def -= 10;
            if (def < 0)
            {
                def = 0;
            }
            player.statDefense = def;
            base.Update(player, ref buffIndex);
        }
    }
}
