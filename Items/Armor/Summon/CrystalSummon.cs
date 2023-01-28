using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace BTLc.Items.Armor.Summon
{
    [AutoloadEquip(EquipType.Head)]
    public class CrystalSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Crystal Headwear");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "魔晶头饰");
            Tooltip.SetDefault("魔力在头顶盘旋");
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
                .AddIngredient(ModContent.ItemType<Items.Materials.MagicCrystal>(), 5)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<Items.Armor.BreastPlates.CrystalBreastPlate>() && legs.type == ModContent.ItemType<Items.Armor.Legs.CrystalLeggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = ("盔甲里的魔法粒子附着在你的法杖上，这使你的法杖有了更强的力量\n" +
                                "召唤栏+1");
            player.maxMinions++;
            Lighting.AddLight(player.position, 0.0f, 1.0f, 0.0f);
        }
    }
}