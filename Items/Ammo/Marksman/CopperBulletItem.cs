using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace BTLc.Items.Ammo.Marksman
{
    
    public class CopperBulletItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Copper Bullet");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "铜质弹丸");
            Tooltip.SetDefault("所以为什么到了未来还要用大口径的铜质弹丸");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }
        public override void SetDefaults()
        {   
            Item.damage = 2; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
			Item.knockBack = 1.5f;
			Item.value = 10;
			Item.rare = ItemRarityID.White;
			Item.shoot = ModContent.ProjectileType<Projectiles.Marksman.CopperBullet>(); // The projectile that weapons fire when using this item as ammunition.
			Item.shootSpeed = 16f; // The speed of the projectile.
			Item.ammo = 5001; // The ammo class this ammo belongs to.
        }
		public override void AddRecipes()
		{
			CreateRecipe(10)
				.AddIngredient(ItemID.CopperBar, 1)
                .AddTile(TileID.Anvils)
                .Register();
		}
    }
}