using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace BTLc.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class FallingStarFoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FallingStar Foots");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "坠星护腿");
            Tooltip.SetDefault("你走过的地方吸引了无数萤火虫");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = ItemRarityID.Blue;
            Item.defense = 1;
            Item.value = Item.sellPrice(0, 0, 5, 0);
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
                .AddIngredient(ItemID.FallenStar, 1)
                .AddIngredient(ItemID.Lens, 2)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
