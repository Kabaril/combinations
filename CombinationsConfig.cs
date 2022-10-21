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

        [Label("Hide Molten Shield Inferno")]
        [Tooltip("Hide Ring of Inferno effect of Molten Shield")]
        [DefaultValue(false)]
        public bool HideMoltenShieldRing;

        [Header("Gameplay")]

        [Label("Disable Vine Rope Collection for Boots")]
        [Tooltip("No more Vine Rope clogging up your Inventory!")]
        [DefaultValue(false)]
        public bool DisableVineRopeForBoots;
    }
}
