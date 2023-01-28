using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace BTLc.Items.Armor.Marksman
{
    [AutoloadEquip(EquipType.Body)]
    public class BambooCloak : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("A breastplate made of bamboo is not very defensive");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "用竹子制作的盔甲，防御力不强");
            DisplayName.SetDefault("Bamboo breastplate");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "竹制蓑衣");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 0, 15);
            Item.value = Item.buyPrice(0, 0, 0, 45);
            Item.defense = 1;
            Item.rare = ItemRarityID.Green;
        }
        public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.BambooBlock, 15)
				.AddTile(TileID.WorkBenches)
				.Register();
		}        
    }
}