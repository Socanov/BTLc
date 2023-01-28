using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace BTLc.GlobalItems
{
    public class CactusSwordGlobalItem : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.CactusSword;
        }

        public override void SetDefaults(Item item)
        {
            base.SetDefaults(item);
            item.damage = 18;
        }
        public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(item, player, target, damage, knockBack, crit);
            target.AddBuff(ModContent.BuffType<Buffs.Penetrate>(), 600);
        }
    }
}