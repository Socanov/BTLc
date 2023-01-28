using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace BTLc.Items.Armor.Legs
{
    [AutoloadEquip(EquipType.Legs)]
    public class CrystalLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Crystal Leggings");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "魔晶护胫");
            Tooltip.SetDefault("散发着魔力的光辉");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 5, 15);
            Item.value = Item.buyPrice(0, 0, 15, 45);
            Item.defense = 1;
            Item.rare = ItemRarityID.Green;
        }
        public override void AddRecipes() 
        {
			CreateRecipe()
				.AddIngredient(ItemID.WoodGreaves, 1)
                .AddIngredient(ModContent.ItemType<Items.Materials.MagicCrystal>(), 3)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
    }
}