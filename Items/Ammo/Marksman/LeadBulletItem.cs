using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace BTLc.Items.Ammo.Marksman
{
    public class LeadBulletItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Lead Bullet");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "铅质弹丸");
            Tooltip.SetDefault("多么古老的弹药啊");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.damage = 4; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
			Item.knockBack = 1.5f;
			Item.value = 10;
			Item.rare = ItemRarityID.White;
			Item.shoot = ModContent.ProjectileType<Projectiles.Marksman.LeadBullet>(); // The projectile that weapons fire when using this item as ammunition.
			Item.shootSpeed = 16f; // The speed of the projectile.
			Item.ammo = 5001; // The ammo class this ammo belongs to.
        }
        public override void AddRecipes()
		{
			CreateRecipe(10)
				.AddIngredient(ItemID.LeadBar, 1)
                .AddTile(TileID.Anvils)
                .Register();
		}
    }
}