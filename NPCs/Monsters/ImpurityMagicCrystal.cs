using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace BTLc.NPCs.Monsters
{
    public class ImpurityMagicCrystal : BTLcNPC
    {
        float rad = Main.rand.NextFloatDirection() * 0.5f;
        bool _isFriendly = true;
        bool shoot = false;
        float Accelerate = 2;
        bool AfterUFmove = false;
        int UFmove = 0;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Impurity Magic Crystal");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "杂质魔晶");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 32;
            NPC.height = 26;
            NPC.damage = 12;
            NPC.defense = 0;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 1000;
            NPC.lifeMax = 60;

            Main.npcFrameCount[NPC.type] = 9;
        }
        enum NPCState
        {
            Normal,
            shoot
        }
        public override void AI()
        {
            base.AI();
            NPC.rotation = 0;
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                _isFriendly = true;
            }
            switch((NPCState)State)
            {
                case NPCState.Normal:
                {
                    if (_isFriendly)
                    {
                        var center = NPC.Center + NPC.velocity * 3;//确定圆心位置
                        var pos = center + rad.ToRotationVector2() * 100;//确定半径
                        NPC.velocity = Vector2.Normalize(pos - NPC.Center) * 2f;
                        if(NPC.collideX)
                        {
                            NPC.velocity.X = NPC.oldVelocity.X * -5f;
                        }
                        if(NPC.collideY)
                        {
                            NPC.velocity.Y = NPC.oldVelocity.Y * -5f;
                        }
                    }
                    else
                    {
                        Player player = Main.player[NPC.target];
                        float distance = Vector2.Distance(NPC.Center, player.Center);
                        if (distance < 150)//过近尝试远离 禁！止！贴！贴！
                        {
							AfterUFmove = true;
                            NPC.velocity = -Vector2.Normalize(player.Center - NPC.Center) * Accelerate;
                            if (Accelerate < 10) Accelerate += 0.1f;
                        }
                        else if (distance > 300)//过远尝试靠近 让！我！康！康！
                        {
							AfterUFmove = true;
                            NPC.velocity = Vector2.Normalize(player.Center - NPC.Center) * Accelerate;
                            if (Accelerate < 10) Accelerate += 0.2f;
                        }
                        else
                        {
                            if (Accelerate > 2) Accelerate -= 0.5f;
                        }
                        SwitchState((int)NPCState.shoot);
                        Timer++;
                    }
                    break;
                }
                case NPCState.shoot:
                {
					shoot = true;
                    Player player = Main.player[NPC.target];
                    NPC.rotation = 0;
                    float distance = Vector2.Distance(NPC.Center, player.Center);
                    if (Timer >= 80 || (distance <= 300 && distance >= 150 && Timer >= 40))
                    {
                        Vector2 ShootVelocity = player.Center - NPC.Center;
                        //ShootVelocity *= 0.5f;
                        for(int i = 0; i < 15; i++)
                        {
                            Dust dust = Dust.NewDustDirect(NPC.position, 40, 40, ModContent.DustType<Dusts.Magic1>(), 0, 0, 0, default, 1.3f);
                            dust.noGravity = true;                        
                        }
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.position, ShootVelocity, ModContent.ProjectileType<Projectiles.MonsterProj.CrystalProj>(), 9, 3);
                        Timer = 0;
                    }
                    SwitchState((int)NPCState.Normal);
                    break;
                }
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
        public override void OnKill()
        {
            base.OnKill();
            for(int i = 0; i < 15; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, 40, 40, 126, 0, 0, 0, default, 1.3f);
                dust.noGravity = true;                        
            }
        }
        public override void FindFrame(int frameHeight)
        {
            base.FindFrame(frameHeight);
            int Once = 30;
            if(_isFriendly == true)
            {
				NPC.frameCounter++;
				if(NPC.frameCounter >= Once)
				{
					NPC.frameCounter = 0;
					NPC.frame.Y += 112;
					if(NPC.frame.Y > 112)
					{
						NPC.frame.Y = 0;
                        NPC.frameCounter = 0;
					}
				}
            }
            else
            {
				if(shoot)
				{
					NPC.frameCounter++;
					if(NPC.frameCounter >= 0.5*Once)
					{
						NPC.frame.Y = 336;
						if(NPC.frameCounter >= 1.5*Once)
						{
							NPC.frame.Y = 392;
							if(NPC.frameCounter >= 2*Once)
							{
								NPC.frame.Y = 448;
								if(NPC.frameCounter >= 3*Once)
								{
									NPC.frame.Y = 0;
									NPC.frameCounter = 0;
								}
							}
						}
					}
				}
				else
				{
					if(AfterUFmove)
					{
						NPC.frameCounter++;
						NPC.frame.Y = 224;
						if(NPC.frameCounter >= Once)
						{
							NPC.frame.Y += 56;
							NPC.frameCounter = 0;
                            AfterUFmove = false;
						}
					}
					else 
					{
						NPC.frameCounter++;
						if(NPC.frameCounter >= Once)
						{
							NPC.frameCounter = 0;
							UFmove++;
							if(UFmove == 2)
							{
								NPC.frame.Y = NPC.frame.Y + 168;
								if(UFmove > 2)
								{
									NPC.frame.Y = 0;
									UFmove = 0;
								}
							}
						}
					}
				}	
            }
        }
        /*         _ooOoo_
                  o8888888o
                  88" . "88
                  (| -_- |)
                  O\  =  /O
               ____/`---'\____
             .'  \\|     |//  `.
            /  \\|||  :  |||//  \
           /  _||||| -:- |||||-  \
           |   | \\\  -  /// |   |
           | \_|  ''\---/''  |   |
           \  .-\__  `-`  ___/-. /
         ___`. .'  /--.--\  `. . __
      ."" '<  `.___\_<|>_/___.'  >'"".
     | | :  `- \`.;`\ _ /`;.`/ - ` : | |
     \  \ `-.   \_ __\ /__ _/   .-` /  /
======`-.____`-.___\_____/___.-`____.-'======     （愿佛祖保佑你不出bug）
                   `=---='
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        */
        public override float SpawnChance(NPCSpawnInfo spawnInfo)//自然刷新
        {
            if (Main.dayTime) 
            {
                if (Main.hardMode)
                {
                    return 0.1f; 
                }
                else
                {
                    return 0.2f;
                }
            }
            else return 0f;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npcLoot);
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Materials.ImpurityMagicCrystal>(), 4));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Materials.MagicCrystal>(), 50));
        }
    }
}