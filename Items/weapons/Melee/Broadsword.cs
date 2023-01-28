using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.Localization;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Creative;
using System;
using Microsoft.Xna.Framework.Input;
using SteelSeries.GameSense.DeviceZone;
using Terraria.GameContent;
using ReLogic.Content;

namespace BTLc.Items.weapons.Melee
{
    public class Broadsword : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            DisplayName.SetDefault("broadsword");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "大刀");
            Tooltip.SetDefault("刀把上镶嵌的水晶发着红光\n"+
                                    "右键向前冲刺并附带伤害\n" +
                                            "左键蓄力攻击");
        }
        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 120;
            Item.shootSpeed = 7f;
            Item.useAnimation = 120;
            Item.useStyle = 5;
            Item.noUseGraphic = true;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = 2;
            Item.autoReuse = true;
            Item.channel = true;
            Item.shoot = ModContent.ProjectileType<BTLcCharging>();
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
                .AddIngredient(ItemID.IronBar, 5)
                .AddIngredient(ItemID.Wood, 3)
                .AddIngredient(ModContent.ItemType<Items.Materials.ImpurityMagicCrystal>())
                .AddTile(TileID.Anvils)
                .Register();                           
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if(player.altFunctionUse == 2)
            {
                Item.shoot = ModContent.ProjectileType<Projectiles.Effects.Moswordver>();
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<BTLcCharging>();
            }
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if(player.altFunctionUse == 2 && !player.HasBuff(ModContent.BuffType<Buffs.Speedfaster>()))
            {
                player.AddBuff(ModContent.BuffType<Buffs.Speedfaster>(), 180);
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}