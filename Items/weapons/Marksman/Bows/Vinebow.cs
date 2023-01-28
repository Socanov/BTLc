using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using BTLc.Util;

namespace BTLc.Items.weapons.Marksman.Bows
{
    public class Vinebow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vine Bow");
            DisplayName.AddTranslation(7, "藤蔓弓");
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Green;
            Item.damage = 12;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = UsespeedID.VerySlow;
            Item.useAnimation = UsespeedID.VerySlow;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = KnockbackID.VeryWeak + 1f;
            Item.value = 200;
            Item.useAmmo = AmmoID.Arrow;
            base.SetDefaults();
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 20)
                .AddIngredient(ItemID.JungleSpores, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}