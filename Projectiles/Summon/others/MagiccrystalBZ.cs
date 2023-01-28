using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace BTLc.Projectiles.Summon.others
{
	public class MagiccrystalBZ : ModProjectile
	{
		public override void SetStaticDefaults() {
			// 这使得弹丸使用鞭碰撞检测，并允许瓶被应用到它
			ProjectileID.Sets.IsAWhip[Type] = true;
		}

		public override void SetDefaults() {
			// 这个方法可以快速设置鞭子的属性
			Projectile.DefaultToWhip();

			// 使用这些来改变默认设置
			// Projectile.WhipSettings.Segments = 20; //片段
			// Projectile.WhipSettings.RangeMultiplier = 1f; //范围
		}

		private float Timer {
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		private float ChargeTime {
			get => Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}

		// 本例使用PreAI实现了一个收费机制。
		// 如果删除此，也要删除Item。channel = true来自项目的SetDefaults。
		public override bool PreAI() {
			Player owner = Main.player[Projectile.owner];

			// 与其他鞭子一样，这个鞭子每帧更新两次(投射。extraUpdates = 1)，所以120等于1秒
			if (!owner.channel || ChargeTime >= 120) {
				return true; // 让香草鞭子AI跑吧。
			}

			if (++ChargeTime % 12 == 0) // 1段每12滴答电荷
				Projectile.WhipSettings.Segments++;

			// 充满电后续航里程增加2倍。
			Projectile.WhipSettings.RangeMultiplier += 1 / 120f;

			// 充电时重置动画和道具计时器
			owner.itemAnimation = owner.itemAnimationMax;
			owner.itemTime = owner.itemTimeMax;

			return false; // 防止AI运行香草鞭
		}

		// 这个方法在所有的点之间画一条线，以防精灵之间有空白
		private void DrawLine(List<Vector2> list) {
			Texture2D texture = TextureAssets.FishingLine.Value;
			Rectangle frame = texture.Frame();
			Vector2 origin = new Vector2(frame.Width / 2, 2);

			Vector2 pos = list[0];
			for (int i = 0; i < list.Count - 1; i++) {
				Vector2 element = list[i];
				Vector2 diff = list[i + 1] - element;

				float rotation = diff.ToRotation() - MathHelper.PiOver2;
				Color color = Lighting.GetColor(element.ToTileCoordinates(), new Color(117, 4, 255));
				Vector2 scale = new Vector2(1, (diff.Length() + 2) / frame.Height);

				Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, SpriteEffects.None, 0);

				pos += diff;
			}
		}

		public override bool PreDraw(ref Color lightColor) {
			List<Vector2> list = new List<Vector2>();
			Projectile.FillWhipControlPoints(Projectile, list);

			DrawLine(list);

			//Main.DrawWhip_WhipBland(Projectile, list); //画平淡的鞭子
			// 下面的代码是用于定制绘图的
			// 如果你不想这样，你可以把它全部删除，然后调用一个香草的DrawWhip方法，就像上面那样
			// 然而，如果你这样做，你必须坚持他们的绘画方式
			SpriteEffects flip = Projectile.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

			Main.instance.LoadProjectile(Type);
			Texture2D texture = TextureAssets.Projectile[Type].Value;

			Vector2 pos = list[0];

			for (int i = 0; i < list.Count - 1; i++) {
				// .这两个值是为了适应投射物的精灵而设置的，但不一定适用于你自己的精灵
				// 如果他们不喜欢，你可以改变他们!
				Rectangle frame = new Rectangle(0, 0, 12, 30);//鞭子头(0,0,宽,高)
				Vector2 origin = new Vector2(5, 8);
				float scale = 1;

				// .这些语句决定为当前部分绘制精灵表的哪个部分
				// 它们也可以被改变以适应你的精灵。
				if (i == list.Count - 2) {
					frame.Y = 90;//鞭子片断
					frame.Height = 30;

					// 为了获得更有冲击力的外观，当鞭子完全伸展时，这将使鞭子的尖端向上，卷曲时向下
					Projectile.GetWhipSettings(Projectile, out float timeToFlyOut, out int _, out float _);
					float t = Timer / timeToFlyOut;
					scale = MathHelper.Lerp(0.5f, 1.5f, Utils.GetLerpValue(0.1f, 0.7f, t, true) * Utils.GetLerpValue(0.9f, 0.7f, t, true));
				}
				else if (i > 0) {
					frame.Y = 68;//鞭子片断
					frame.Height = 30;
				}
				//else if (i > 0) {
					//frame.Y = 68;//鞭子片断
					//frame.Height = 30;
				//}
				//else if (i > 5) {
					//frame.Y = 46;//鞭子片断
					//frame.Height = 32;
				//}
				//else if (i > 0) {
					//frame.Y = 28;//鞭子片断
					//frame.Height = 26;
				//}

				Vector2 element = list[i];
				Vector2 diff = list[i + 1] - element;

				float rotation = diff.ToRotation() - MathHelper.PiOver2; // .这个弹丸的精灵面朝下，所以PiOver2被用来修正旋转
				Color color = Lighting.GetColor(element.ToTileCoordinates());

				Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, flip, 0);

				pos += diff;
			}
			return false;
		}
	}
}
