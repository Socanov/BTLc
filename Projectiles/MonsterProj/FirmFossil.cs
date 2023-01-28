using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using System;
using Terraria.Localization;

namespace BTLc.Projectiles.MonsterProj
{
    public class FirmFossil : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
			DisplayName.SetDefault("Firm Fossil");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "坚固化石");
        }
        public override void SetDefaults()
        {
            Projectile.width = 4;
			Projectile.height = 4;
			Projectile.aiStyle = 1;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.alpha = 255;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.extraUpdates = 1;
			Projectile.damage = 0;
			Projectile.light = 0.2f;
            Projectile.scale = 0.7f;

			AIType = ProjectileID.Bullet;
        }
        public override void Kill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            base.OnHitPlayer(target, damage, crit);
            Projectile.Kill();
        }
    }
}