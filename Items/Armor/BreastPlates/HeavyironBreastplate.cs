using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace BTLc.Items.Armor.BreastPlates
{
    [AutoloadEquip(EquipType.Body)]
    public class HeavyironBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("heavy iron breastplates");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "重型铁胸甲");
            Tooltip.SetDefault("更多的铁，更强的护甲");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 5, 15);
            Item.value = Item.buyPrice(0, 0, 15, 45);
            Item.defense = 5;
            Item.rare = ItemRarityID.Green;
        }
        public override void AddRecipes() 
        {
			CreateRecipe()
				.AddIngredient(ItemID.IronBar, 30)
				.AddTile(TileID.Anvils)
				.Register();
		}  
    }
}