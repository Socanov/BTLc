using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace BTLc.Items.Armor.BreastPlates
{
    [AutoloadEquip(EquipType.Body)]
    public class CrystalBreastPlate : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Crystal BreastPlates");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "魔晶胸甲");
            Tooltip.SetDefault("木甲附魔过后的产物");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 5, 15);
            Item.value = Item.buyPrice(0, 0, 15, 45);
            Item.defense = 3;
            Item.rare = ItemRarityID.Green;
        }
        public override void AddRecipes() 
        {
			CreateRecipe()
				.AddIngredient(ItemID.WoodHelmet, 1)
                .AddIngredient(ModContent.ItemType<Items.Materials.MagicCrystal>(), 8)
				.AddTile(TileID.WorkBenches)
				.Register();
		}  
    }
}