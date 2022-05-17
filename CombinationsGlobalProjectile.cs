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
                        if (source is EntitySource_ItemUse source_item)
                        {
                            CreateGhostCopy(projectile, source_item);
                        }
                    } else
                    {
                        if (source is EntitySource_ItemUse source_item)
                        {
                            Item item = source_item.Item;
                            if (projectile.DamageType == DamageClass.Ranged && item is not null && item.consumable)
                            {
                                CreateGhostCopy(projectile, source_item);
                            }
                        }
                    }

                }
            }
            base.OnSpawn(projectile, source);
        }

        private static void CreateGhostCopy(Projectile projectile, EntitySource_ItemUse source_item)
        {
            if(source_item.Entity is Player player)
            {
                if(Main.netMode != NetmodeID.Server && player.whoAmI == Main.myPlayer)
                {
                    EntitySource_MasterThrowingCharm clone_source = new EntitySource_MasterThrowingCharm();
                    Vector2 clone_position = projectile.position;
                    clone_position.Y -= (projectile.height / 2f);
                    Projectile.NewProjectile(clone_source, clone_position, projectile.velocity, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0], projectile.ai[1]);
                }
            }
        }
    }
}
