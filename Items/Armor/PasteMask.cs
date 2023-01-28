using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace BTLc.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class PasteMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Paste Mask");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "浆糊面具");
            Tooltip.SetDefault("移动速度-4%\n"+
                "伤害减免+6%\n"+
                "难以呼吸...");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 0, 30);
            Item.defense = 0;
            Item.rare = ItemRarityID.Blue;
        }
        public override void UpdateEquip(Player player)
        {
            player.endurance += 0.06f;
            player.moveSpeed -= 0.04f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Gel, 40);
            recipe.AddIngredient(ItemID.Cobweb, 25);
            recipe.AddCondition(Recipe.Condition.NearWater);
            recipe.Register();
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<Items.Armor.PasteShirt>() && legs.type == ModContent.ItemType<Items.Armor.PasteBoots>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = ("召唤栏+1\n" +
                                "召唤伤害+9%");
            player.GetDamage(DamageClass.Summon) += 9;
            player.maxMinions += 1;
        }
    }
}