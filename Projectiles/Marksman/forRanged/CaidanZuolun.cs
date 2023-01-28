using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;

namespace BTLc.Projectiles.Marksman.forRanged
{
    public class CaidanZuolun : ModProjectile
    {
        int r = 1;
        int time = 0;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("丢出的彩弹左轮");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.rotation = 0f;
            Projectile.damage = 20;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 200;
        }
        public override void AI()
        {
            base.AI();
            Player player = Main.player[Projectile.owner];
            Projectile.rotation += 0.5f;
            if(time % 3 == 0)
            {
                for (float r = 0f; r < MathHelper.TwoPi; r += MathHelper.TwoPi / 16f) 
                {
                    Vector2 velocity = new Vector2((float)Math.Cos(r), (float)Math.Sin(r)) * 10f;
                    Projectile.NewProjectile(Entity.GetSource_FromAI(), Projectile.Center, velocity, 587, Projectile.damage, Projectile.knockBack, Projectile.owner);
                    r++;
                }
            }
        }
    }
}