using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace BTLc.Dusts
{
    public class Fire1 : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            base.OnSpawn(dust);
            dust.noLight = true;
            dust.scale = 0.7f;
        }
        public override bool Update(Dust dust)
        {
            dust.scale -= 0.1f;
            if(dust.scale < 0.5f)
            {
                dust.active = false;
            }
            else
            {
                float strength = dust.scale/2f;
                Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), dust.color.R / 255f * 0.5f * strength, dust.color.G / 255f * 0.5f * strength, dust.color.B / 255f * 0.5f * strength);
            }
            return false;
        }
    }
}