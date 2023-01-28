using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace BTLc.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class PasteShirt : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Paste Shirt");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "浆糊衬衫");
            Tooltip.SetDefault("移动速度-10%\n"+
                "伤害减免+7%\n" +
                "唔,好粘,恶心!");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 0, 50);
            Item.defense = 0;
            Item.rare = ItemRarityID.Blue;
        }
        public override void UpdateEquip(Player player)
        {
            player.endurance += 0.07f;
            player.moveSpeed -= 0.1f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Gel, 75);
            recipe.AddIngredient(ItemID.Cobweb, 45);
            recipe.AddCondition(Recipe.Condition.NearWater);
            recipe.Register();
        }
    }
}