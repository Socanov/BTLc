using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.GameContent.Creative;

namespace BTLc.Items.weapons.Marksman
{
    public class BambooBlunderBuss : ModItem
    {
        SoundStyle BlunderBusses = new SoundStyle("BTLc/Sounds/other/BlunderBuss");
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Bamboo blunderbuss");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "竹制火铳");
            Tooltip.SetDefault("用竹子做火铳真的不会炸膛吗");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.noMelee = true;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.reuseDelay = 45;
            Item.useStyle = 5;
            Item.width = 40;
            Item.height = 40;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.Green;
            Item.useTurn = false;
            Item.value = Item.sellPrice(0, 0, 3, 0);
            Item.value = Item.buyPrice(0, 0, 9, 0);
            Item.damage = 35;
            Item.shootSpeed = 8f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Marksman.CopperBullet>();
            Item.useAmmo = 5001;
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
                .AddIngredient(ItemID.BambooBlock, 5)
                .AddIngredient(ItemID.IronBar, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.BambooBlock, 5)
                .AddIngredient(ItemID.LeadBar, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(BlunderBusses);
            Vector2 plrToMouse = Main.MouseWorld - player.Center;
            Projectile.NewProjectileDirect(source, position, plrToMouse, type, damage, knockback, player.whoAmI);
            for (int i = 0; i <= 3; i++)
            {
                Dust dust = Dust.NewDustDirect(player.position, 20, 20, ModContent.DustType<Dusts.Fire1>(), -2f, 5f, 100, Color.White);//生成粒子效果，并且让粒子向人物后方喷射（什么形容词）
            }
            return false;
        }
        public override Vector2? HoldoutOffset() 
        {
		    return new Vector2(0f, 5f);
		}
    }
}