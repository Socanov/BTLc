using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.Localization;
using Terraria.GameContent.ItemDropRules;
using System.Net.Mail;

namespace BTLc.NPCs.Monsters
{
    public class StormSlime : BTLcNPC
    {
        private int FrameTimer = 0;
        private int _JumpTime = 0;
        private int _jumpHigh = 0;
        private Vector2 PlayerPos;
        //private int Timer = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("StormSlime");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "风暴史莱姆");
        }
        public static int Life()
        {
            if (Main.masterMode) return 1190;
            else if (Main.expertMode) return 1400;
            else return 2000;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 86;
            NPC.height = 64;
            NPC.damage = 20;
            NPC.defense = 0;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 0;
            NPC.rotation = 0;
            NPC.lifeMax = Life();
            Main.npcFrameCount[NPC.type] = 2;
            NPC.scale = 2;
            NPC.knockBackResist = 0f;
        }
        enum NPCState
        {
            Normal,
            Ready,
            ReadytoJump,
            shoot,
            Jump,move
        }
        public override void AI()
        {
            base.AI();
            FrameTimer++;
            NPC.direction = (int)NPC.ai[2];
            switch ((NPCState)State)
            {
                case NPCState.Normal://初始化
                    {
                        NPC.ai[2] = Main.rand.NextBool() ? -1 : 1;//随机选择方向
                        SwitchState((int)NPCState.Ready);
                        break;
                    }
                case NPCState.Ready:
                    {
                        Player player = Main.player[NPC.target];
                        if (NPC.velocity.Y == 0)//当npc落地时做出的反应
                        {
                            NPC.velocity.X *= 0.9f;
                        }
                        Timer++;
                        if (Timer > (Main.expertMode?45:60))
                        {
                            Timer = 0;
                            if (_JumpTime == 1)
                            {
                                if(Main.rand.NextBool())SwitchState((int)NPCState.shoot);
                                else
                                {
                                    PlayerPos = player.position;
                                    SwitchState((int)NPCState.move);
                                }
                            }
                            _JumpTime += 1;
                            if (_JumpTime > 1) _JumpTime = 0;
                            else
                            {
                                _jumpHigh += 1;
                                if (_jumpHigh > 2) _jumpHigh = 0;
                                NPC.velocity.X = NPC.ai[2] * 3;// 提供初始跳跃速度
                                if (_jumpHigh == 2) NPC.velocity.Y = -8;
                                else NPC.velocity.Y = -6;
                                SwitchState((int)NPCState.Jump);
                            }
                            break;
                        }
                        if (Timer % 30 < 1)
                        {
                            SwitchState((int)NPCState.ReadytoJump);
                        }
                            NPC.TargetClosest();
                            NPC.ai[2] = player.Center.X > NPC.Center.X ? 1 : -1;
                        if (Math.Abs(NPC.velocity.Y) > 0.1f)
                        {
                            SwitchState((int)NPCState.Jump);
                        }
                        break;
                    }
                case NPCState.ReadytoJump:
                    {
                        Timer++;
                        if (Timer % 30 == 15)
                        {
                            SwitchState((int)NPCState.Ready);
                        }
                        break;
                    }
                case NPCState.Jump:
                    {
                        NPC.velocity.X = NPC.ai[2] * 3;
                        if (NPC.collideY)
                        {
                            SwitchState((int)NPCState.Ready);
                        }
                        break;
                    }
                case NPCState.shoot:
                    {
                            Player player = Main.player[NPC.target];
                            Vector2 ShootVelocity = player.Center - NPC.Center;
                            ShootVelocity /= ShootVelocity.Length();
                            ShootVelocity *= 10f;
                            float r = (float)Math.Atan2(ShootVelocity.Y, ShootVelocity.X);
                            int Count = disorder ? 2 : 1;

                            for (int i = -Count; i <= Count; i++)

                            {
                                float r2 = r + i * MathHelper.Pi / (disorder ? 24f : 18f);
                                Vector2 shootVel = r2.ToRotationVector2() * 7.5f;
                                Projectile proj=Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.position, shootVel, ModContent.ProjectileType<Projectiles.MonsterProj.FirmFossil>(), NPC.damage/4, 10);
                            proj.scale = 2;
                            }
                        SwitchState((int)NPCState.Ready);
                        break;
                    }
                case NPCState.move:
                    {
                        Timer++;
                        Player player = Main.player[NPC.target];
                        NPC.scale = 2 - (float)Timer / 50;
                        if (Timer % 2 == 0)
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                Vector2 Pos = PlayerPos + new Vector2(550, 0).RotatedBy(Math.PI / 4 * i);
                                Dust.NewDustDirect(Pos, 10, 10, ModContent.DustType<Dusts.Magic1>(), 0, 0);
                            }
                        }
                        if (Timer >= 90)
                        {
                            Timer = 0;
                            NPC.scale = 2;
                            NPC.position = PlayerPos + new Vector2(0, -150);
                            NPC.ai[2] = Main.rand.NextBool() ? -1 : 1;
                            NPC.direction = (int)NPC.ai[2];
                            SwitchState((int)NPCState.Normal);
                            if (Main.expertMode)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    Vector2 ShootVelocity2 = new Vector2(0, -8).RotatedBy(Math.PI / 1.5f * i);
                                    Projectile Proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, ShootVelocity2, ModContent.ProjectileType<Projectiles.MonsterProj.SandRock>(), NPC.damage, 10);
                                    Proj.scale = 1;
                                }
                            }
                            Timer = 0;
                        }
                        break;
                    }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            if (FrameTimer % 14 < 7)
            {
                NPC.frame.Y = frameHeight;
            }
            else
            {
                NPC.frame.Y = 0;
            }
        }
        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            base.OnHitByItem(player, item, damage, knockback, crit);
        }
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            base.OnHitByProjectile(projectile, damage, knockback, crit);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 2, 5));
        }
    }
}
