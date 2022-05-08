using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria;
using Combinations.Items.OvergrownTreads;
using Combinations.Items.JungleBoots;
using Combinations.Items.TubularMagiluminescence;
using Combinations.Items.MirrorNecklace;
using Combinations.Items.HuntersMark;
using Combinations.Items.DeadlyEnviromentGear;
using Terraria.ID;
using Combinations.Buffs;
using Combinations.Items.MagicArrow;
using Microsoft.Xna.Framework;
using System;

namespace Combinations
{
    public class CombinationsPlayer : ModPlayer
    {
        public DateTime lastHurt = DateTime.Now;

        private static int[] RecoveryBuffAccessories => new int[] { OvergrownTreads.ItemType(), JungleBoots.ItemType() };

        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            if(Helpers.HasPlayerAccessoryEquipped<TubularMagiluminescence>(Player)) {
                drawInfo.drawFloatingTube = false;
            }
            ;
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

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            //Any whip can apply MirrorNecklaceBuff even if it does not have a tag itself
            if (Helpers.HasPlayerAccessoryEquipped<MirrorNecklace>(Player) && ProjectileID.Sets.IsAWhip[proj.type])
            {
                target.AddBuff(MirrorNecklaceBuff._type_unsafe, 240);
            }
            if(!proj.npcProj && !proj.trap && (proj.minion || ProjectileID.Sets.MinionShot[proj.type]))
            {
                for (int j = 0; j < 5; j++)
                {
                    if (target.buffTime[j] >= 1 && target.HasBuff<MirrorNecklaceBuff>())
                    {
                        damage += 10;
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
                                target.AddBuff(HuntersMarkBuffOne._type_unsafe, 600);
                                break;
                            }
                        case 1:
                            {
                                int buff_index = target.FindBuffIndex(ModContent.BuffType<HuntersMarkBuffOne>());
                                if (buff_index != -1)
                                {
                                    target.DelBuff(buff_index);
                                }
                                target.AddBuff(HuntersMarkBuffTwo._type_unsafe, 720);
                                break;
                            }
                        case 2:
                            {
                                int buff_index = target.FindBuffIndex(ModContent.BuffType<HuntersMarkBuffTwo>());
                                if (buff_index != -1)
                                {
                                    target.DelBuff(buff_index);
                                }
                                target.AddBuff(HuntersMarkBuffThree._type_unsafe, 840);
                                break;
                            }
                        case 3:
                            {
                                int buff_index = target.FindBuffIndex(ModContent.BuffType<HuntersMarkBuffThree>());
                                if (buff_index != -1)
                                {
                                    target.DelBuff(buff_index);
                                }
                                target.AddBuff(HuntersMarkBuffFour._type_unsafe, 960);
                                break;
                            }
                        case 4:
                            {
                                int buff_index = target.FindBuffIndex(ModContent.BuffType<HuntersMarkBuffFour>());
                                if (buff_index != -1)
                                {
                                    target.buffTime[buff_index] = 960;
                                }
                                damage += 5;
                                break;
                            }
                    }
                }
            }
            base.ModifyHitNPCWithProj(proj, target, ref damage, ref knockback, ref crit, ref hitDirection);
        }

        public override void PreUpdate()
        {
            if(Helpers.HasPlayerAccessoryEquipped<MagicArrow>(Player))
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

        public override void PreUpdateBuffs()
        {
            if (Helpers.HasPlayerAccessoryEquipped<DeadlyEnviromentGear>(Player))
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

        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {
            if(Main.hardMode)
            {
                if(Main.rand.Next(5, Math.Min(1000 - attempt.fishingLevel, 100)) <= 10)
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

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            lastHurt = DateTime.Now;
            base.Hurt(pvp, quiet, damage, hitDirection, crit);
        }
    }
}
