using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;

namespace BTLc.Items.weapons.Summon
{
    public class PeachwoodSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            DisplayName.SetDefault("peachwood sword");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "桃木剑");
            Tooltip.SetDefault("神秘的东方力量");
        }
        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Summon;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 60;
            Item.shootSpeed = 7f;
            Item.useAnimation = 60;
            Item.useStyle = 1;
            Item.knockBack = 2;
            Item.value = 10;
            Item.rare = 2;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Summon.PeachwoodSword>();
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Items.Materials.ImpurityMagicCrystal>(), 1)
                .AddIngredient(ItemID.Wood, 50)
                .AddTile(TileID.WorkBenches)
                .Register();                           
        }
        public override Vector2? HoldoutOffset() 
        {
		    return new Vector2(-5f, -7f);
		}
    }
}