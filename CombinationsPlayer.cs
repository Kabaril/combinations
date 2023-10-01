using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Combinations.Buffs;
using Combinations.Items.MagicArrow;
using Combinations.Items.OvergrownTreads;
using Combinations.Items.JungleBoots;
using Combinations.Items.TubularMagiluminescence;
using Combinations.Items.MirrorNecklace;
using Combinations.Items.HuntersMark;
using Combinations.Items.DeadlyEnviromentGear;
using Combinations.Items.SolarCharm;
using Combinations.Items.VortexCharm;
using Combinations.Items.NebulaCharm;
using Combinations.Items.StardustCharm;
using System.Collections.Generic;
using Combinations.Items.WildernessGuide;
using Combinations.Items.UnholyAbomination;
using Combinations.Items.CloudOutOfBottle;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;
using Combinations.Items.MOPPGear;

namespace Combinations
{
    public sealed class CombinationsPlayer : ModPlayer
    {
        public DateTime lastHurt = DateTime.Now;

        public int NebulaCharmCharge = 0;

        public Asset<Texture2D> handsOnGlowMask;

        private static int[] RecoveryBuffAccessories => new int[] { OvergrownTreads.ItemType(), JungleBoots.ItemType() };

        public override void ResetEffects()
        {
            handsOnGlowMask = null;
        }

        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            if(Helpers.HasPlayerAccessoryEquipped<TubularMagiluminescence>(Player)) {
                drawInfo.drawFloatingTube = false;
            }
            base.ModifyDrawInfo(ref drawInfo);
        }

        public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
        {
            if(Helpers.HasPlayerOneOfAccessoryEquipped(Player, RecoveryBuffAccessories))
            {
                healValue = (int)(healValue * 1.15f);
            }
            base.GetHealLife(item, quickHeal, ref healValue);
        }

        public override void GetHealMana(Item item, bool quickHeal, ref int healValue)
        {
            if (Helpers.HasPlayerOneOfAccessoryEquipped(Player, RecoveryBuffAccessories))
            {
                healValue = (int)(healValue * 1.15f);
            }
            base.GetHealMana(item, quickHeal, ref healValue);
        }

        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            if(NebulaCharmCharge > 100 && item.DamageType == DamageClass.Magic || item.DamageType == DamageClass.MagicSummonHybrid)
            {
                StatModifier damage_mod = GetNebulaChargeDamage(NebulaCharmCharge);
                damage.Flat += damage_mod.Flat;
                damage += (damage_mod.Additive - 1f);
            }
            base.ModifyWeaponDamage(item, ref damage);
        }

        public override void OnConsumeMana(Item item, int manaConsumed)
        {
            if(NebulaCharmCharge > 0)
            {
                NebulaCharmCharge -= (50 + (manaConsumed * 15));
                if(NebulaCharmCharge < 0)
                {
                    NebulaCharmCharge = 0;
                }
            }
            base.OnConsumeMana(item, manaConsumed);
        }

        private static StatModifier GetNebulaChargeDamage(int charge)
        {
            StatModifier modifier = StatModifier.Default;
            modifier.Flat = Math.Min(charge / 75f, 20f);
            //not very low charge
            if (charge > 1500)
            {
                modifier += (charge / 90000f);
            }
            return modifier;
        }

        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            // True Melee Attacks
            if (Helpers.HasPlayerAccessoryEquipped<SolarCharm>(Player))
            {
                // 100% bonus
                modifiers.SourceDamage *= 2;
            }
            if(item.ModItem is UnholyAbomination)
            {
                if (Main.rand.NextBool(5))
                {
                    target.AddBuff(24, 180);
                }
            }
            base.ModifyHitNPCWithItem(item, target, ref modifiers);
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            //Any whip can apply MirrorNecklaceBuff even if it does not have a tag itself
            if (Helpers.HasPlayerAccessoryEquipped<MirrorNecklace>(Player) && ProjectileID.Sets.IsAWhip[proj.type])
            {
                target.AddBuff(ModContent.BuffType<MirrorNecklaceBuff>(), 240);
            }
            if(!proj.npcProj && !proj.trap && (proj.minion || ProjectileID.Sets.MinionShot[proj.type]))
            {
                if (Helpers.HasPlayerAccessoryEquipped<StardustCharm>(Player))
                {
                    target.AddBuff(ModContent.BuffType<StardustCharmBuff>(), 240);
                }
                if(target.HasBuff<MirrorNecklaceBuff>())
                {
                    modifiers.FlatBonusDamage += 10;
                }
            }
            if(!proj.npcProj && !proj.trap && !proj.minion && !ProjectileID.Sets.MinionShot[proj.type])
            {
                //reverse Tag from StardustCharm
                if (target.HasBuff<StardustCharmBuff>())
                {
                    modifiers.FlatBonusDamage += 20;
                }
                //Solar Charm damage bonus
                if (proj.DamageType == DamageClass.Melee && !ProjectileID.Sets.IsAWhip[proj.type] && Helpers.HasPlayerAccessoryEquipped<SolarCharm>(Player))
                {
                    float distance = target.Distance(Player.position);
                    distance *= 0.0075f;
                    if(distance <= 1f)
                    {
                        // 50% 
                        modifiers.SourceDamage *= 1.5f;
                    } else if(distance <= 50f)
                    {
                        // 50% -> 0% bonus
                        StatModifier damage_bonus = modifiers.SourceDamage / distance;
                        modifiers.SourceDamage += damage_bonus.Base;
                    }
                }
                //Vortex Charm effects
                if(proj.DamageType == DamageClass.Ranged && Helpers.HasPlayerAccessoryEquipped<VortexCharm>(Player))
                {
                    if(Main.rand.NextDouble() <= 0.1d) {
                        double effect = Main.rand.NextDouble();
                        if(effect <= 0.2d) {
                            target.AddBuff(BuffID.OnFire3, 240);
                        } else if (effect <= 0.4d) {
                            target.AddBuff(BuffID.ShadowFlame, 240);
                        } else if (effect <= 0.6d) {
                            target.AddBuff(BuffID.CursedInferno, 240);
                        } else if (effect <= 0.8d) {
                            target.AddBuff(BuffID.Frostburn, 240);
                        } else {
                            target.AddBuff(BuffID.Ichor, 240);
                        }
                    }
                    if (Main.rand.NextDouble() <= 0.05d) {
                        target.AddBuff(BuffID.BetsysCurse, 240);
                    }
                }
            }
            if(Helpers.HasPlayerAccessoryEquipped<HuntersMark>(Player))
            {
                if (!proj.npcProj && !proj.trap && !proj.minion && !ProjectileID.Sets.MinionShot[proj.type])
                {
                    int mark_buff = 0;
                    for (int j = 0; j < 5; j++)
                    {
                        if (target.buffTime[j] >= 1)
                        {
                            if(target.HasBuff<HuntersMarkBuffOne>())
                            {
                                mark_buff = 1;
                            }
                            if (target.HasBuff<HuntersMarkBuffTwo>())
                            {
                                mark_buff = 2;
                            }
                            if (target.HasBuff<HuntersMarkBuffThree>())
                            {
                                mark_buff = 3;
                            }
                            if (target.HasBuff<HuntersMarkBuffFour>())
                            {
                                mark_buff = 4;
                            }
                        }
                    }
                    //buff can only be on one enemy -- remove when not already applied
                    if (mark_buff == 0)
                    {
                        for (int i = 0; i < 200; i++)
                        {
                            NPC nPC = Main.npc[i];
                            if (nPC.active)
                            {
                                int buff_index = nPC.FindBuffIndex(ModContent.BuffType<HuntersMarkBuffOne>());
                                if (buff_index != -1)
                                {
                                    nPC.DelBuff(buff_index);
                                }
                                buff_index = nPC.FindBuffIndex(ModContent.BuffType<HuntersMarkBuffTwo>());
                                if (buff_index != -1)
                                {
                                    nPC.DelBuff(buff_index);
                                }
                                buff_index = nPC.FindBuffIndex(ModContent.BuffType<HuntersMarkBuffThree>());
                                if (buff_index != -1)
                                {
                                    nPC.DelBuff(buff_index);
                                }
                                buff_index = nPC.FindBuffIndex(ModContent.BuffType<HuntersMarkBuffFour>());
                                if (buff_index != -1)
                                {
                                    nPC.DelBuff(buff_index);
                                }
                            }
                        }
                    }
                    switch(mark_buff)
                    {
                        default:
                        case 0:
                            {
                                target.AddBuff(ModContent.BuffType<HuntersMarkBuffOne>(), 600);
                                break;
                            }
                        case 1:
                            {
                                int buff_index = target.FindBuffIndex(ModContent.BuffType<HuntersMarkBuffOne>());
                                if (buff_index != -1)
                                {
                                    target.DelBuff(buff_index);
                                }
                                target.AddBuff(ModContent.BuffType<HuntersMarkBuffTwo>(), 720);
                                break;
                            }
                        case 2:
                            {
                                int buff_index = target.FindBuffIndex(ModContent.BuffType<HuntersMarkBuffTwo>());
                                if (buff_index != -1)
                                {
                                    target.DelBuff(buff_index);
                                }
                                target.AddBuff(ModContent.BuffType<HuntersMarkBuffThree>(), 840);
                                break;
                            }
                        case 3:
                            {
                                int buff_index = target.FindBuffIndex(ModContent.BuffType<HuntersMarkBuffThree>());
                                if (buff_index != -1)
                                {
                                    target.DelBuff(buff_index);
                                }
                                target.AddBuff(ModContent.BuffType<HuntersMarkBuffFour>(), 960);
                                break;
                            }
                        case 4:
                            {
                                int buff_index = target.FindBuffIndex(ModContent.BuffType<HuntersMarkBuffFour>());
                                if (buff_index != -1)
                                {
                                    target.buffTime[buff_index] = 960;
                                }
                                modifiers.FlatBonusDamage += 5;
                                break;
                            }
                    }
                }
            }
            base.ModifyHitNPCWithProj(proj, target, ref modifiers);
        }

        public override void PreUpdate()
        {
            if(Helpers.HasPlayerAccessoryEquipped<MagicArrow>(Player) && !(Player.mount is not null && Player.mount.Active))
            {
                if(Player.wingTime <= 1f && Player.wingTimeMax > 0f)
                {
                    if (Player.HasBuff(BuffID.ManaSickness))
                    {
                        if (Player.CheckMana(6, false, true) && Player.CheckMana(6, true))
                        {
                            Player.wingTime += 4f;
                        }
                    }
                    else
                    {
                        if (Player.CheckMana(6, true))
                        {
                            Player.wingTime += 4f;
                        }
                    }
                }
            }
            base.PreUpdate();
        }

        public override void PostUpdateMiscEffects()
        {
            if(Helpers.HasPlayerAccessoryEquipped<CloudOutOfBottle>(Player))
            {
                CloudOutOfBottleMovement(Player);
            }
            base.PostUpdateMiscEffects();
        }

        private void CloudOutOfBottleMovement(Player player)
        {
            bool no_extra_jumps = player.ExtraJumps.ToArray().All(x => !x.Available);
            if (player.grappling[0] == -1 && no_extra_jumps && player.jump == 0 && player.velocity.Y != 0f && player.rocketTime == 0 && player.wingTime == 0f && !player.mount.Active)
            {
                if (player.controlJump)
                {
                    float gravity = player.gravity;
                    float gravity_offset = gravity + 1E-06f;
                    player.fallStart = (int)(player.position.Y / 16f);
                    if (player.gravDir == 1f)
                    {
                        player.velocity.Y = -gravity_offset;
                    }
                    else if (player.gravDir == -1f)
                    {
                        player.velocity.Y = gravity_offset;
                    }
                    if (player.controlUp)
                    {
                        player.velocity.Y = player.velocity.Y - 5f;
                    }
                    if (player.controlDown)
                    {
                        player.velocity.Y = player.velocity.Y + 5f;
                    }
                }
            }
        }


        public override void PostUpdateEquips()
        {
            if(Helpers.HasPlayerItemInInventory<WildernessGuide>(Player))
            {
                Player.cordage = true;
                Player.dontHurtCritters = true;
            }
            base.PostUpdateEquips();
        }

        public override void PreUpdateBuffs()
        {
            if(Helpers.HasPlayerAccessoryEquipped<NebulaCharm>(Player))
            {
                Player.AddBuff(ModContent.BuffType<NebulaCharmBuff>(), 10);
            }
            if(Player.HasBuff<NebulaCharmBuff>())
            {
                // 60 * 60 * 5 = 18000 -> 1 minute for full charge
                // 1 minute for full buff
                if (NebulaCharmCharge >= 18000)
                {
                    NebulaCharmCharge = 18000;
                } else
                {
                    NebulaCharmCharge += 5;
                }
            } else
            {
                NebulaCharmCharge = 0;
            }
            if (Helpers.HasPlayerAccessoryEquipped<DeadlyEnviromentGear>(Player) || Helpers.HasPlayerAccessoryEquipped<MOPPGear>(Player))
            {
                if (lastHurt < DateTime.Now.Subtract(TimeSpan.FromSeconds(5d)))
                {
                    Lighting.AddLight((int)(Player.Center.X / 16f), (int)(Player.Center.Y / 16f), 0.1f, 0.2f, 0.45f);
                    Player.iceBarrier = true;
                    Player.endurance += 0.25f;
                    Player.iceBarrierFrameCounter++;
                    if (Player.iceBarrierFrameCounter > 2)
                    {
                        Player.iceBarrierFrameCounter = 0;
                        Player.iceBarrierFrame++;
                        if (Player.iceBarrierFrame >= 12)
                        {
                            Player.iceBarrierFrame = 0;
                        }
                    }
                }
            }
            base.PreUpdateBuffs();
        }

        public override void ModifyDrawLayerOrdering(IDictionary<PlayerDrawLayer, PlayerDrawLayer.Position> positions)
        {
            HandsOnAccessoryGlowDrawLayer hands_layer = new HandsOnAccessoryGlowDrawLayer();
            PlayerDrawLayer hands_parent = positions.Keys.First(x => x.Name == "HandOnAcc");
            if (hands_parent is not null)
            {
                var position = new PlayerDrawLayer.AfterParent(hands_parent);
                positions.Add(hands_layer, position);
            }

            AfterFrontAccFrontAccessoryDrawLayer cloud_layer = new AfterFrontAccFrontAccessoryDrawLayer();
            PlayerDrawLayer cloud_parent = positions.Keys.First(x => x.Name == "FrontAccFront");
            if (cloud_parent is not null)
            {
                var position = new PlayerDrawLayer.AfterParent(cloud_parent);
                positions.Add(cloud_layer, position);
            }
            base.ModifyDrawLayerOrdering(positions);
        }

        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {
            if(Main.hardMode)
            {

                if (attempt.rare && !attempt.legendary && !attempt.veryrare && Main.rand.NextBool(100))
                {
                    sonar.Text = "Magic Arrow";
                    sonar.Color = Color.Red;
                    sonar.DurationInFrames = 120;
                    sonar.Velocity = Vector2.Zero;
                    itemDrop = MagicArrow.ItemType();
                }
            }
            base.CatchFish(attempt, ref itemDrop, ref npcSpawn, ref sonar, ref sonarPosition);
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            lastHurt = DateTime.Now;
            base.OnHurt(info);
        }
    }
}
