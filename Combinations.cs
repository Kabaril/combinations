using Terraria.ModLoader;

namespace Combinations
{
	public class Combinations : Mod
	{
        public override void Load()
        {
            Logger.Info("Initializing Combinations");
            //WikiThisIntegration.LoadWikiThisIntegration(this);
        }

        public override void Unload()
        {
            Logger.Info("Unloading Combinations");
            Helpers.Unload();
        }
    }
}