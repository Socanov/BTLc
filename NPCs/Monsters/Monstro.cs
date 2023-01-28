using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.Localization;
using Terraria.GameContent.ItemDropRules;
namespace BTLc.NPCs.Monsters
{
    public class Monstro : BTLcNPC
    {
        private bool _isFriendly = true;
        private float Xspeed = 0;
        private float Yspeed = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Monstro");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "萌死戳");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 32;
            NPC.height = 26;
            NPC.damage = 20;
            NPC.defense = 0;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 100;
            NPC.rotation = 0;
            NPC.lifeMax = 600;
            Main.npcFrameCount[NPC.type] = 2;//NPC帧图数量为2
        }
        enum NPCState
        {
            Init,//初始化
            Normal,//常态
            shoot,//射击
            Fall,//掉落
            Jump,//索敌跳跃
            Jump2//随机跳跃
        }
        public override void AI()
        {
            base.AI();
            NPC.direction = (int)NPC.ai[2]; 
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                _isFriendly = true;
            }
            switch((NPCState)State)
            {
                case NPCState.Init://初始化
                {
                    NPC.ai[2] = Main.rand.NextBool() ? -1 : 1;//随机选择方向
                    SwitchState((int)NPCState.Normal);
                    break;
                }
                case NPCState.Normal:
                    Timer++;
                    if (!_isFriendly)
                    {
                        NPC.TargetClosest();
                        Player player = Main.player[NPC.target];
                        NPC.ai[2] = player.Center.X > NPC.Center.X ? 1 : -1;
                    }
                    if (Timer > 120)
                    {
                        Player player = Main.player[NPC.target];
                        Timer = 0;
                        float Dis = (player.position - NPC.position).Length();
                        if (!_isFriendly && Main.rand.NextBool() && Dis<350)
                            {
                                SwitchState((int)NPCState.shoot);
                            }
                            else
                                {
                                if(!_isFriendly && Main.rand.NextBool())
                                {
                                    Xspeed = Math.Min(Math.Abs(NPC.Center.X - player.Center.X)/30f, 15) * NPC.ai[2];
                                    Yspeed = Math.Max(Math.Min((NPC.position.Y - player.position.Y+90) / 15f, 40),10);
                                //Main.NewText($"{NPC.position.Y - player.position.Y/-30f},{Yspeed}");
                                SwitchState((int)NPCState.Jump);
                                }
                                else
                                {
                                    NPC.velocity.X = NPC.ai[2] * 3;
                                    NPC.velocity.Y = Main.rand.NextBool()?-5:-9;
                                    SwitchState((int)NPCState.Jump2);
                                }
                            }
                    }
                    
                    break;
                case NPCState.Jump:
                {
                    NPC.velocity.X= Xspeed;
                    NPC.velocity.Y=-Yspeed*(30-Timer)/30f;
                    Player player = Main.player[NPC.target];
                    //y碰撞 或者索敌超过0.5秒时进入下落状态
                    if((Timer > 3 && NPC.collideX)) NPC.velocity.X = 0;
                    if (Timer>30 || (Timer>3 && NPC.collideY))
                    {
                            //Main.NewText("fall");
                            NPC.velocity.X = 0;
                            Timer = 0;
                            SwitchState((int)NPCState.Fall);
                    }
                        Timer++;
                        break;
                }
                case NPCState.Jump2:
                    {
                        NPC.velocity.X = NPC.ai[2] * 4;
                        if (NPC.collideX) NPC.velocity.X = -NPC.velocity.X;
                        if ((Timer != 0 && NPC.collideY))
                        {
                            NPC.velocity.X = 0;
                            Timer = 0;
                            Vector2 Velocity;
                            Velocity.X = -5;
                            Velocity.Y = 0;
                            Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, Velocity, 83, 20, 10);
                            Velocity.X = 5;
                            Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, Velocity, 83, 20, 10);
                            NPC.ai[2] = Main.rand.NextBool() ? -1 : 1;
                            SwitchState((int)NPCState.Normal);
                        }
                        Timer++;
                        break;
                    }
                case NPCState.Fall:
                    {
                        NPC.velocity.Y += 0.1f;
                        if (NPC.collideX) NPC.velocity.X = 0;
                        else if (NPC.velocity.X != 0) NPC.velocity.X *= 0.9f;
                        Player player = Main.player[NPC.target];
                        if (NPC.collideY&&NPC.position.Y>player.position.Y-60)
                        {
                            Timer = 0;
                            Vector2 Velocity;
                            Velocity.X = -5;
                            Velocity.Y = 0;
                            Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center,Velocity,83, 20, 10);
                            Velocity.X = 5;
                            Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, Velocity,83, 20, 10);
                            SwitchState((int)NPCState.Normal);
                            NPC.velocity.Y = 0;
                        }
                        else if (NPC.collideY)
                        {
                            NPC.position.Y += NPC.velocity.Y;
                        }
                        break;
                    }
                case NPCState.shoot:
                {
                        if (Timer > 30)
                        {
                            Timer = 0;
                            SwitchState((int)NPCState.Normal);
                            Player player = Main.player[NPC.target];
                            Vector2 ShootVelocity = (player.Center - NPC.Center);
                            ShootVelocity.Normalize();
                            ShootVelocity *= 5;
                            for (int i = 0; i < 6; i++)
                            {
                                Vector2 ShootVelocity2=ShootVelocity.RotatedBy((Main.rand.NextFloat(2)-1)*Math.PI/4);
                                Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center,ShootVelocity2,82, 20, 10);
                            }
                        }
                    Timer++;
                    break;
                }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            if (Timer%14<7 && (NPCState)State==NPCState.Normal)
            {
               NPC.frame.Y = 0;
            }   
            else
            {
                NPC.frame.Y = frameHeight;
            }              
        }
        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {       
            _isFriendly = false;
            base.OnHitByItem(player, item, damage, knockback, crit);
        }
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            _isFriendly = false;
            base.OnHitByProjectile(projectile, damage, knockback, crit);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.LifeCrystal));
            npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 2, 5));
            npcLoot.Add(ItemDropRule.Common(1309, 1000));
        }
    }
}