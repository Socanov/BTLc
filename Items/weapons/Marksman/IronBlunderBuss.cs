using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using Terraria.Audio;

namespace BTLc.Items.weapons.Marksman
{
    public class IronBlunderBuss : ModItem
    {
        SoundStyle BlunderBusses = new SoundStyle("BTLc/Sounds/other/BlunderBuss");
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            DisplayName.SetDefault("Iron blunderbuss");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "三眼铳");
            Tooltip.SetDefault("铳口如鸟铳大，可容铅弹三钱\n" + 
                                "————《武备要略》");
        }
        public override void SetDefaults()
        {
            Item.width = 44; // Hitbox width of the item.
			Item.height = 18; // Hitbox height of the item.
			Item.rare = ItemRarityID.White; // The color that the item's name will be in-game.

			// Use Properties
			Item.useTime = 40; // The item's use time in ticks (60 ticks == 1 second.)
			Item.useAnimation = 40; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
			Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
			// Weapon Properties
			Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
			Item.damage = 12; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			Item.knockBack = 6f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			Item.noMelee = true; // So the item's animation doesn't do damage.

			// Gun Properties
			Item.shoot = ModContent.ProjectileType<Projectiles.Marksman.CopperBullet>(); // For some reason, all the guns in the vanilla source have this.
			Item.shootSpeed = 8f; // The speed of the projectile (measured in pixels per frame.)
            Item.useAmmo = 5001;
            Item.reuseDelay = 20;
            Item.value = Item.sellPrice(0, 0, 6, 0);
            Item.value = Item.buyPrice(0, 0, 18, 0);
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
                .AddIngredient(ItemID.IronBar, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			const int NumProjectiles = 3; // The humber of projectiles that this gun will shoot.
            SoundEngine.PlaySound(BlunderBusses);

			for (int i = 0; i < NumProjectiles; i++) {
				// Rotate the velocity randomly by 30 degrees at max.
				Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));

				// Decrease velocity randomly for nicer visuals.
				newVelocity *= 1f - Main.rand.NextFloat(0.3f);

				// Create a projectile.
				Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                
			}
			for (int i = 0; i <= 3; i++)
            {
                Dust dust = Dust.NewDustDirect(player.position, 20, 20, ModContent.DustType<Dusts.Fire1>(), -2f, 5f, 100, Color.White);//生成粒子效果，并且让粒子向人物后方喷射（什么形容词）
            }
            return false;
        }
        public override Vector2? HoldoutOffset() 
        {
		    return new Vector2(-2f, -2f);
		}
    }
}