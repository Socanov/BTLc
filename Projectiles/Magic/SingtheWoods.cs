using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using System;
using System.Threading;

namespace BTLc.Projectiles.Magic
{
    public class SingtheWoods : ModProjectile
    {
		public int timer=0;
		public Vector2 Start;
        public override void SetDefaults() 
		{
			Projectile.width = 4;
			Projectile.height = 4;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 100;
			Projectile.alpha = 255;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.damage = 5;
			Projectile.light = 0.2f;
			AIType = ProjectileID.Bullet;
			Start = Projectile.position;
        }
        public override void AI()
        {
			Projectile.velocity.Normalize();
			Projectile.velocity *= 400;
			if (timer == 0)
			{
				Start = Projectile.position;
			}
			timer++;
			if (timer > 1)
			{
				Vector2 End = Projectile.position;
				Vector2 Goto = (End - Start);
                Main.NewText($"{Projectile.position.X},{Projectile.position.X},{Start},{End}");
                float Tm = Vector2.Distance(End,Start);
				Goto.Normalize();
				Vector2 Pos;
				for(int i = 0; i < Tm; i++)
				{
                    Pos = Start + Goto * i;
                    Dust.NewDustDirect(Pos, 10, 10, ModContent.DustType<Dusts.Magic1>(), 0, 0);
                }
                Projectile.Kill();
			}
        }
    }
}