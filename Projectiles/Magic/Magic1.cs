using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace BTLc.Projectiles.Magic
{
    public class Magic1 : ModProjectile
    {
        public override void SetDefaults() 
		{
			Projectile.width = 4;
			Projectile.height = 4;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
			Projectile.alpha = 255;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.damage = 5;
			Projectile.light = 0.7f;

			AIType = ProjectileID.Bullet;
		}
        public override void AI()
        {
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, ModContent.DustType<Dusts.Magic1>(), 0, 0);
            }
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
                Projectile.velocity = (targetVel + Projectile.velocity * 6) / 7f;
            }
        }
    }
}