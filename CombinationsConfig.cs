using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Combinations
{
    public sealed class CombinationsConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        // Automatically set by tModLoader
        public static CombinationsConfig Instance;

        [Header("Visual")]

        [DefaultValue(false)]
        public bool HideMoltenShieldRing;

        [Header("Gameplay")]

        [DefaultValue(false)]
        public bool DisableVineRopeForBoots;

        [DefaultValue(false)]
        public bool EnableUnfinishedItems;
    }
}
