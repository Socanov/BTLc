using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace BTLc.Buffs
{
    public class Curse : ModBuff
    {
        int i = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("诅咒");
            Description.SetDefault("受到诅咒，重病缠身");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            base.Update(npc, ref buffIndex);
            npc.lifeRegen -= 30;
            for(int i = 0; i < 5; i ++)
            {
                Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.Blood, 0, 0, 0, default, 0.6f);
            }
        }
    }
}