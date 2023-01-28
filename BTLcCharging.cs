using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace BTLc
{
    public class BTLcCharging : BTLcProj
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.width = 0;
			Projectile.height = 0;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 180;
            base.SetDefaults();
        }
        public override void AI()
        {
            base.AI();
            Player player1 = Main.player[Projectile.owner];
            if(Main.player[Projectile.owner].channel && player1.HeldItem.type == ModContent.ItemType<Items.weapons.Melee.Broadsword>())
            {
                Player player = Main.player[Projectile.owner];
                if(Timer == 60 || Timer == 62)
                {
                    Dust.NewDustPerfect(player.Center, DustID.Cloud);
                }
                Timer++;
            }
            else if(!Main.player[Projectile.owner].channel && Timer > 60)
            {
                Player player = Main.player[Projectile.owner];
                Timer = 0;
                Projectile.NewProjectile(Entity.GetSource_FromAI(), Projectile.Center, new Microsoft.Xna.Framework.Vector2(0,0), ModContent.ProjectileType<Projectiles.Melee.MoSword>(), 100, 5, Projectile.owner);
                player.velocity.X = 2 * player.direction;
            }
            else if(!Main.player[Projectile.owner].channel && Timer <= 60)
            {
                Timer = 0;
            }
        }
        public override void PostDraw(Color lightColor)
        {
            base.PostDraw(lightColor);
            Player player = Main.player[Projectile.owner];
			Texture2D texture = ModContent.Request<Texture2D>("BTLc/Items/weapons/Melee/Broadsword").Value;
            Vector2 pos = player.Center - Main.screenPosition;
            if(player.channel)
            {
                if(player.direction > 0)
                {
                    Main.spriteBatch.Draw(texture, new Vector2(pos.X - 10, pos.Y + 7), null, Color.White, MathHelper.Pi, texture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
                }
                else
                {
                    Main.spriteBatch.Draw(texture, new Vector2(pos.X + 10, pos.Y + 7), null, Color.White, MathHelper.Pi, texture.Size() * 0.5f, 1f, SpriteEffects.FlipHorizontally, 0f);
                }
            }
        }
    }
}