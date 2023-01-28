using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.Localization;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BTLc.ModeItem
{
	public class Disorder : ModItem
	{
		public bool disorder = false;
		int i = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Disorder Core");
			DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "混乱之核");
			Tooltip.SetDefault("现在，你有能力控制这个世界，但前提是你有能力活下去……");
		}
		public override void SetDefaults()
		{
			Item.width = 12;
			Item.height = 12;
			Item.maxStack = 1;
			Item.useTurn = false;
			Item.autoReuse = false;
			Item.useAnimation = 60;
			Item.useTime = 60;
			Item.consumable = false;
			Item.useStyle = ItemUseStyleID.Shoot;
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 6));
		}
		public override bool CanUseItem(Player player)
		{
			return Main.expertMode||Main.masterMode;
		}
		public override bool? UseItem(Player player)
		{
			if (i == 0)
			{
				disorder = true;
				string key = "世界不再和平稳定";
				Color messageColor = Color.BlueViolet;
				Main.NewText(Language.GetTextValue(key), messageColor);
			}
            if (disorder && i == 1)
            {
                disorder = false;
                string key2 = "世界变得和平稳定";
                Color messageColor2 = Color.BlueViolet;
                Main.NewText(Language.GetTextValue(key2), messageColor2);
            }
            if (!disorder && i == 2)
			{
				disorder = true;
				string key = "世界不再和平稳定";
				Color messageColor = Color.BlueViolet;
				Main.NewText(Language.GetTextValue(key), messageColor);
				i = 0;
			}
            i++;
            return disorder;
        }
	}
}