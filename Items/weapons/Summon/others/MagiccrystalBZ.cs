using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace BTLc.Items.weapons.Summon.others
{
	public class MagiccrystalBZ : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			DisplayName.SetDefault("magic crystal whip");
			DisplayName.AddTranslation(7, "魔晶鞭");
			Tooltip.SetDefault("用魔晶做的鞭子，他们好像能自动粘在一起\n" + 
									"左键蓄力，蓄力时间与鞭子长度成正比");
		}

		public override void SetDefaults() {
			// 这个方法可以快速设置鞭子的属性。
			// 鼠标移到上面可以看到它的参数
			Item.DefaultToWhip(ModContent.ProjectileType<Projectiles.Summon.others.MagiccrystalBZ>(), 20, 2, 4); 
			//默认到鞭子(内容弹类型

			Item.shootSpeed = 4; //速度
			Item.rare = ItemRarityID.Green; //稀有度

			Item.channel = true; //允许蓄力
		}

		// 配方
		public override void AddRecipes(){
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<Items.Materials.ImpurityMagicCrystal>(), 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
