using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;

namespace BTLc.Items.weapons.Magic
{
    public class ApprenticeStaves : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Apprentice Staves");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "学徒法杖");
            Tooltip.SetDefault("通过操控魔法粒子直接进行攻击");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.noMelee = true;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 5;
            Item.width = 40;
            Item.height = 40;
            Item.DamageType = DamageClass.Magic;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.Green;
            Item.useTurn = false;
            Item.value = Item.sellPrice(0, 0, 21, 0);
            Item.value = Item.buyPrice(0, 0, 63, 0);
            Item.damage = 35;
            Item.shootSpeed = 20f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Magic.Magic1>();
            Item.UseSound = SoundID.Item20;
            Item.staff[Item.type] = true;
            Item.mana = 12;
            Item.scale = 1.3f;
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 5)
                .AddIngredient(ModContent.ItemType<Items.Materials.MagicCrystal>(), 1)
                .AddTile(TileID.WorkBenches)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.PalmWood, 5)
                .AddIngredient(ModContent.ItemType<Items.Materials.MagicCrystal>(), 1)
                .AddTile(TileID.WorkBenches)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.BorealWood, 5)
                .AddIngredient(ModContent.ItemType<Items.Materials.MagicCrystal>(), 1)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
        public override Vector2? HoldoutOffset() 
        {
		    return new Vector2(0f, 5f);
		}
    }
}