using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace BTLc.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class FallingStarHalmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FallingStar Halmet");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "坠星头盔");
            Tooltip.SetDefault("散发出光芒");
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
                .AddIngredient(ItemID.Lens, 2)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<Items.Armor.FallingStarArmor>() && legs.type == ModContent.ItemType<Items.Armor.FallingStarFoots>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = ("因为坠星蕴含的魔力,你能够发出明亮的光芒以照亮周围\n" +
                                "法术伤害 + 7%");
            player.GetDamage(DamageClass.Magic) += 0.07f;
            Lighting.AddLight(player.position, 2.0f, 2.0f, 2.0f);
        }
    }
}
