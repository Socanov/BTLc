using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace BTLc.Dusts
{
    public class Magicgreen : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noLight = false;
            dust.noGravity = true;
            base.OnSpawn(dust);
        }
    }
}