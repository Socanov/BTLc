using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;

namespace BTLc.Items.Armor.Marksman
{
    [AutoloadEquip(EquipType.Head)]
    public class BambooHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("A helmet made of bamboo is not very defensive");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "用竹子制作的盔甲，防御力不强");
            DisplayName.SetDefault("Bamboo Helmet");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "竹制斗笠");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 0, 20);
            Item.value = Item.buyPrice(0, 0, 0, 60);
            Item.rare = ItemRarityID.Green;
            Item.defense = 1;
        }
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.BambooBlock, 25)
				.AddTile(TileID.WorkBenches)
				.Register();
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<Items.Armor.Marksman.BambooCloak>() && legs.type == ModContent.ItemType<Items.Armor.Marksman.BambooLeggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "移动速度增加10%";
            player.moveSpeed = 1.1f;
        }
    }
}