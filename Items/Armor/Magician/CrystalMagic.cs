using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace BTLc.Items.Armor.Magician
{
    [AutoloadEquip(EquipType.Head)]
    public class CrystalMagic : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Crystal Glasses");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "魔法棱镜");
            Tooltip.SetDefault("我们至今不知道玩家为什么要把棱镜套在头上,甚至还很好用（？）");
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
            player.setBonus = ("你虽然无法更好的掌握魔法粒子的形态，但是这里面蕴藏的粒子简直太丰富了\n" +
                                "魔法暴击率 + 15%");
            player.GetCritChance(DamageClass.Magic) += 15;
            Lighting.AddLight(player.position, 0.0f, 1.0f, 0.0f);
        }
    }
}