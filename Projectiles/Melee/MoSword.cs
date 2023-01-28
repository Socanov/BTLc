using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using System;

namespace BTLc.Projectiles.Melee
{
    public class MoSword : BTLcProj
    {
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("刀气");
			Main.projFrames[Projectile.type] = 4;//帧数
			
		}

		public override void SetDefaults() {
			Projectile.width = 270;
			Projectile.height = 170;
			Projectile.scale = 1f;//规模
			Projectile.damage = 5; //伤害
			Projectile.light = 1f; //发光?0.8f
			Projectile.friendly = true;
			Projectile.tileCollide = false; // true使仆从自由通过瓷砖
			Projectile.DamageType = DamageClass.Melee; // 伤害类型=伤害等级.近战
			Projectile.friendly = true; // 弹丸友好
			Projectile.ownerHitCheck = true; // 所有者点击检查   防止通过瓷砖击中。大多数使用射弹的近战武器都有这个
			Projectile.penetrate = -1;//穿透-1为无限
			Projectile.ignoreWater = false;//忽略水
			//Projectile.netImportant = true;
			Projectile.timeLeft = 22;//时间
			Projectile.aiStyle = -1; //
			Projectile.extraUpdates = 2; // 额外更新 每次更新 1+额外更新 次
		}

		// 在这里你可以决定你的仆从是否打碎了草或罐子之类的东西
		//public override bool? CanCutTiles() {
			//return false;
		//}
		// 如果你的随从造成接触伤害，这是强制性的(在移动区域的AI()进一步相关的东西)
		//public override bool MinionContactDamage() {
			//return true;
		//}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			
			//target.AddBuff(ModContent.BuffType<bzbuff>(), 240); //袭击npc获得buff
			Main.player[Projectile.owner].MinionAttackTargetNPC = target.whoAmI;
		}
		public override void AI() {
			Player player = Main.player[Projectile.owner];
			player.heldProj = Projectile.whoAmI;//Vector2.Normalize(Main.MouseWorld - Projectile.Center)
			if(player.direction > 0){
				Projectile.Center = player.Center+ Vector2.Normalize(Main.MouseWorld - player.Center) * 30;//+ Vector2.Normalize(Projectile.velocity);//  * (Timer - 1f);
			}
			else{
				Projectile.Center = player.Center+ Vector2.Normalize(Main.MouseWorld - player.Center) * 30;//
			}
			Projectile.spriteDirection = player.direction;
			Projectile.rotation = Projectile.velocity.ToRotation();//+ MathHelper.PiOver4 - MathHelper.PiOver4 * Projectile.spriteDirection;
			// 这是一个简单的“从上到下循环所有帧”动画
			int frameSpeed = 5;
			Projectile.frameCounter++;

			if (Projectile.frameCounter >= frameSpeed) {
				Projectile.frameCounter = 0;
				Projectile.frame++;//画面帧

				if (Projectile.frame >= Main.projFrames[Projectile.type]) {
					Projectile.frame = 4;
				}
			}
		}
    }
}