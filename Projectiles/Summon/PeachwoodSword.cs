using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Graphics;
using Microsoft.Xna.Framework;

namespace BTLc.Projectiles.Summon
{
    public class PeachwoodSword : BTLcProj
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("基础道法");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.damage = 1;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.light = 0.2f;
            Projectile.friendly = true;
            Projectile.timeLeft = 120;
        }
        public override void AI()
        {
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.Center, 2, 2, DustID.FireworkFountain_Blue, 0, 0);
            }
            if(Projectile.timeLeft < 80)
            {
                NPC target = null;
                Player player = Main.player[Projectile.owner];
                float distanceMax = 600f;
                foreach (NPC npc in Main.npc)
                {
                    if (npc.active && !npc.friendly)
                    {
                        // 计算与投射物的距离
                        float currentDistance = Vector2.Distance(npc.Center, Projectile.Center);
                        if (currentDistance < distanceMax)
                        {
                            if (Projectile.timeLeft <= 270)
                            {
                                distanceMax = currentDistance;
                                target = npc;
                            }
                        }
                    }
                }
                if(target != null)
                {
                    var targetVel = Vector2.Normalize(target.position - Projectile.Center) * 10f;
                    Projectile.velocity = (targetVel + Projectile.velocity * 12) / 13f;
                }
            }
            base.AI();
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Curse>(), 180);
            base.OnHitNPC(target, damage, knockback, crit);
        }
    }
}