using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Graphics;

namespace BTLc
{
    public abstract class BTLcProj : ModProjectile
    {
        protected int State
        {
            get {return (int)Projectile.ai[0];}
            set {Projectile.ai[0] = value;}
        }
        protected int Timer
        {
            get { return (int)Projectile.ai[1];}
            set { Projectile.ai[1] = value; }
        }
        protected virtual void SwitchState(int state)
        {
            State = state;
        }
        //注：在使用顶点拖尾的时候，要在投射物的setdefaults里加上以下代码用来传入oldpos避免崩溃,并且使用之前需要知道顶点绘制时候需要什么，要不然少东西不是出不来就是游戏直接崩掉
        //也可以在使用的时候先给弹幕加粒子拖尾来确定弹幕是不是成功放出，如果不是就检查负责投射弹幕的东西
        //ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        //ProjectileID.Sets.TrailCacheLength[Projectile.type] = 30;
        //并且projectileID.sets这个东西在做其他东西的时候也能用到，比如yoyo
        // 弹幕拖尾(弹幕，拖尾贴图，拖尾偏移，拖尾颜色1，拖尾颜色2,拖尾宽度，是否采用拖尾逐渐缩小)
        //注，这个拖尾只能叠加单层的绘制图像，如果需要多层叠加还是要用shader
        public static void ProjectileDrawTail(Projectile Projectile, Texture2D Tail, Color TailColor1, Color TailColor2, float Width, bool Lerp)
        {
            List<CustomVertexInfo> bars = new List<CustomVertexInfo>();
            for(int i = 1; i < Projectile.oldPos.Length; ++i)
            {
                if(Projectile.oldPos[i] == Vector2.Zero) break;
                var normalDir = Projectile.oldPos[i - 1] - Projectile.oldPos[i];
                normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));
                float scale = Projectile.scale * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
                float width = Width;
                if(Lerp)
                {
                    width = Width * scale;
                }
                var factor = i / (float)Projectile.oldPos.Length;
                var w = MathHelper.Lerp(1f, 0.05f, factor);
                var color = Color.Lerp(TailColor1, TailColor2, factor);
                color.A = 0;
                Vector2 offset = Vector2.Zero;
                bars.Add(new CustomVertexInfo(Projectile.oldPos[i] + normalDir * width, color, new Vector3(factor, 1, w)));
                bars.Add(new CustomVertexInfo(Projectile.oldPos[i] + normalDir * -width, color, new Vector3(factor, 0, w)));
            }
            List<CustomVertexInfo> Vx = new List<CustomVertexInfo>();
            if (bars.Count > 2)
            {
                Vx.Add(bars[0]);
                Vx.Add(bars[1]);
                Vx.Add(bars[2]);
                for (int i = 0; i < bars.Count - 2; i += 2)
                {
                    Vx.Add(bars[i]);
                    Vx.Add(bars[i + 2]);
                    Vx.Add(bars[i + 1]);

                    Vx.Add(bars[i + 1]);
                    Vx.Add(bars[i + 2]);
                    Vx.Add(bars[i + 3]);
                }
            }
            Main.graphics.GraphicsDevice.Textures[0] = Tail;
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, Vx.ToArray(), 0, Vx.Count / 3);
        }
        public struct CustomVertexInfo : IVertexType
        {
            private static VertexDeclaration _vertexDeclaration = new VertexDeclaration(new VertexElement[3]
            {
                new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),
                new VertexElement(8, VertexElementFormat.Color, VertexElementUsage.Color, 0),
                new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0)
            });
            /// <summary>
            /// 绘制位置(世界坐标)
            /// </summary>
            public Vector2 Position;
            /// <summary>
            /// 绘制的颜色
            /// </summary>
            public Color Color;
            /// <summary>
            /// 前两个是纹理坐标，最后一个是自定义的
            /// </summary>
            public Vector3 TexCoord;

            public CustomVertexInfo(Vector2 position, Color color, Vector3 texCoord)
            {
                this.Position = position;
                this.Color = color;
                this.TexCoord = texCoord;
            }

            public VertexDeclaration VertexDeclaration => _vertexDeclaration;
        }
    }
}