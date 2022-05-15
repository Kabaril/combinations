using Combinations.Items.MasterThrowingCharm;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Combinations
{
    public class CombinationsGlobalProjectile : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if(Helpers.HasPlayerAccessoryEquipped<MasterThrowingCharm>(Main.LocalPlayer) && projectile.owner == Main.myPlayer)
            {
                if(!projectile.npcProj && !projectile.trap && !projectile.minion && !ProjectileID.Sets.MinionShot[projectile.type])
                {
                    if(projectile.DamageType == DamageClass.Throwing)
                    {
                        Item item = null;
                        if (source is EntitySource_ItemUse source_item)
                        {
                            item = source_item.Item;
                        }
                        CreateGhostCopy(projectile, source, item);
                    } else
                    {
                        Item item = null;
                        if (source is EntitySource_ItemUse source_item)
                        {
                            item = source_item.Item;
                        }
                        if (projectile.DamageType == DamageClass.Ranged && item is not null && item.consumable)
                        {
                            CreateGhostCopy(projectile, source, item);
                        }
                    }

                }
            }
            base.OnSpawn(projectile, source);
        }

        private static void CreateGhostCopy(Projectile projectile, IEntitySource source, Item source_item = null)
        {
            //spawn on the server, because we dont have player here
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                EntitySource_MasterThrowingCharm clone_source = new EntitySource_MasterThrowingCharm();
                Vector2 clone_position = projectile.position;
                clone_position.Y -= (projectile.height / 2f);
                Projectile.NewProjectile(clone_source, clone_position, projectile.velocity, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0], projectile.ai[1]);
            }
        }
    }
}
