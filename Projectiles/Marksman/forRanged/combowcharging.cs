using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace BTLc.Projectiles.Marksman.forRanged
{
    public class combowcharging : BTLcProj
    {
        float i = 0;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.width = 0;
			Projectile.height = 0;
            Projectile.tileCollide = false;
            base.SetDefaults();
        }
        public override void AI()
        {
            //这里声音因为没有音频所以没加，就暂时放这里了
            Player player = Main.player[Projectile.owner];
            base.AI();
            if(player.channel)
            {
                if(Timer > 30 && Timer < 60)
                {
                    i = 1.25f;
                }
                else if(Timer >60 && Timer < 120)
                {
                    i = 2f;
                }
                else if(Timer > 120)
                {
                    i = 3f;
                }
                Timer++;
            }
            else if(!player.channel)
            {
                Vector2 shootvel = Vector2.Normalize(Main.MouseWorld - player.Center);
                Projectile.NewProjectile(Entity.GetSource_FromAI(), player.Center, shootvel, AmmoID.Arrow, (int)(40 * i), 4 *(i / 2), Projectile.owner);//正在写，这里应该填一个特殊的箭
            } 
        }
        public override void PostDraw(Color lightColor)
        {
            base.PostDraw(lightColor);
            Player player = Main.player[Projectile.owner];
			Texture2D texture = ModContent.Request<Texture2D>("BTLc/Items/weapons/Melee/MoSword").Value;
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