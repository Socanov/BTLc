using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace BTLc.Projectiles.Magic
{
    public class DemonHand : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("魔之手");                                                                                                                                         ;//设置帧数为12
        base.SetStaticDefaults();
    }
    public override void SetDefaults()
    {
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.height = 100;
        Projectile.width = 100;
        Projectile.light = 1f;
        Projectile.tileCollide = false;
        Projectile.extraUpdates = 2;
        Projectile.ignoreWater = true;
        base.SetDefaults();
    }
    public override void AI()
    {
            for (int i = 0; i < 3; i++)
            {
                Dust.NewDust(Projectile.TopLeft, 10, 10, 121);
            }
        Player player = Main.player[Projectile.owner];
        Projectile.Center = player.Center + Vector2.Normalize(Projectile.velocity) * 5;

            Main.projFrames[Projectile.type] = 12;
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                    Projectile.Kill();
                }
            }
            base.AI();
    }
}
}