using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BTLc.Projectiles.Marksman
{
	public class CopperBullet : BTLcProj
	{

		public override void SetDefaults() 
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.alpha = 255;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.extraUpdates = 1;
			Projectile.damage = 2;
			Projectile.light = 0.5f;
			Projectile.scale = 1.3f;

			Projectile.aiStyle = -1;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        	ProjectileID.Sets.TrailCacheLength[Projectile.type] = 30;
		}
		public override void Kill(int timeLeft) {
			//当打到物块时消失并播放相应的音效
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
			for(int i = 0; i <= 5; i ++)
			{
				Dust.NewDust(Projectile.Center, 20, 20, ModContent.DustType<Dusts.Fire1>());
			}
			Projectile.Kill();
        }
	}
}