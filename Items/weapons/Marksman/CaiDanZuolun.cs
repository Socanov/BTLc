using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace BTLc.Items.weapons.Marksman
{
    public class CaidanZuolun : ModItem
    {
        int i = 0;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("彩弹左轮");
            Tooltip.SetDefault("究竟是左轮变了心，还是彩弹枪出了轨\n"+
                                "当连续攻击5次以上时可右键使用特殊功能\n"+
                                "漫游左轮表示很赞");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.shoot = 587;
            Item.shootSpeed = 15f;
            Item.reuseDelay = 20;
            Item.width = 40;
            Item.height = 40;
            Item.autoReuse = true;
            Item.useTime = 10;
            Item.useAnimation = 60;
            Item.useStyle = 5;
            Item.value = Item.sellPrice(0,0,5,0);
            Item.value = Item.buyPrice(0, 0, 15, 0);
            Item.damage = 13;
            Item.useTurn = true;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.rare = 2;
            Item.knockBack = 3;
            Item.noUseGraphic = false;
            Item.crit = 20;
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(3350, 1);
            recipe.AddIngredient(2269, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if(i >= 30)
                {
                    Item.shoot = ModContent.ProjectileType<Projectiles.Marksman.forRanged.CaidanZuolun>();
                    Item.shootSpeed = 5f;
                    Item.useTime = 60;
                    Item.useAnimation = 60;
                    Item.noUseGraphic = true;
                    Item.useStyle = 1;
                    i = 0;
                }
            }
            else
            {
                Item.shoot = 587;
                Item.shootSpeed = 15f;
                Item.useTime = 10;
                Item.useAnimation = 60;
                Item.noUseGraphic = false;
                Item.useStyle = 5;
            }
            return true;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(4));
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            i++;
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        public override Vector2? HoldoutOffset() 
        {
		    return new Vector2(-2f, -2f);
		}
    }
}