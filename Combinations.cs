using Terraria.ModLoader;

namespace Combinations
{
	public sealed class Combinations : Mod
	{
        // Automatically set by tModLoader
        public static Combinations Instance;

        public override void Load()
        {
            Logger.Info("Initializing Combinations");
        }

        public override void Unload()
        {
            Logger.Info("Unloading Combinations");
            Helpers.Unload();
        }
    }
}