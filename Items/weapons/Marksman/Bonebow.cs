using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace BTLc.Items.weapons.Marksman
{
    public class Bonebow : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Bone bow");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "骨弓");
            Tooltip.SetDefault("每诞生一把骨弓，就会有一个骷髅被杀害，没有买卖就没有伤害（bushi）");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.noMelee = true;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = 5;
            Item.width = 40;
            Item.height = 40;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = false;
            Item.rare = ItemRarityID.Green;
            Item.useTurn = true;
            Item.value = Item.sellPrice(0, 0, 3, 0);
            Item.value = Item.buyPrice(0, 0, 9, 0);
            Item.damage = 18;
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Arrow;
            Item.knockBack = 5;
            Item.crit = 5;
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
                .AddIngredient(ItemID.Bone, 20)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}