using Combinations.Items.UnholyAbomination;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Combinations
{
    [Autoload(false)]
    public static class WikiThisIntegration
    {
        public static void LoadWikiThisIntegration(Mod current)
        {
            //try
            //{
            ModLoader.TryGetMod("Wikithis", out Mod wikithis);
            if (wikithis is not null && !Main.dedServ)
            {
                wikithis.Call("AddModURL", current, "github.com/Kabaril/combinations");
            }
            //}
            //catch (Exception e)
            //{
            //    Logger.Error("Error while loading Wikithis integration:");
            //    Logger.Error(e.Message, e);
            //}
        }
    }
}
