using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace BTLc.Items.Armor.Legs
{
    [AutoloadEquip(EquipType.Legs)]
    public class HeavyironLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("heavy iron leggings");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "重型铁护腿");
            Tooltip.SetDefault("更多的铁，更大的重量");
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
				.AddIngredient(ItemID.IronBar, 15)
				.AddTile(TileID.Anvils)
				.Register();
		}
    }
}