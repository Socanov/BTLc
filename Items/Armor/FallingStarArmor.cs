using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace BTLc.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class FallingStarArmor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FallingStar Armor");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "坠星装甲");
            Tooltip.SetDefault("你的身体微微发光");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.defense = 1;
            Item.rare = ItemRarityID.Blue;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FallenStar, 1)
                .AddIngredient(ItemID.Lens, 3)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}