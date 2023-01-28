using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace BTLc.Items.Armor.Melee
{
    [AutoloadEquip(EquipType.Head)]
    public class HeavyironHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("heavy iron helmet");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "重型铁头盔");
            Tooltip.SetDefault("更多的铁，对颈椎更大的压力");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 5, 15);
            Item.value = Item.buyPrice(0, 0, 15, 45);
            Item.defense = 2;
            Item.rare = ItemRarityID.Green;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
				.AddIngredient(ItemID.IronBar, 20)
				.AddTile(TileID.Anvils)
				.Register();
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<Items.Armor.BreastPlates.HeavyironBreastplate>() && legs.type == ModContent.ItemType<Items.Armor.Legs.HeavyironLeggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = ("你装备上重型盔甲，拥有了超强的防御，但代价是什么呢\n" +
                                "防御+8\n"+
                                "近战伤害+20%\n" +
                                "移动速度-10%");
            player.statDefense += 8;
            player.GetDamage(DamageClass.Melee) += 0.2f;
            player.moveSpeed = 0.9f;
        }
    }
}