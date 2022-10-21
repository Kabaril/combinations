using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Combinations.Buffs
{
    public sealed class HuntersMarkBuffOne : ModBuff
    {
        public static Asset<Texture2D> buff_hit_texture;
        public const string HitTexturePath = "Combinations/Buffs/HuntersMarkOne";

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hunters Mark");
			Description.SetDefault("The hunt begins");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;

            if (Main.netMode != NetmodeID.Server)
            {
                // Do NOT load textures on the server!
                buff_hit_texture = ModContent.Request<Texture2D>(HitTexturePath);
            }
        }

        public override void Unload()
        {
            buff_hit_texture = null;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            int def = player.statDefense;
            def -= 5;
            if (def < 0)
            {
                def = 0;
            }
            player.statDefense = def;
            base.Update(player, ref buffIndex);
        }
    }
}
