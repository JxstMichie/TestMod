using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;


namespace TestMod.Items //Directory! Modname.FolderName
{
	public class OnyxiaV2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("OnyxiaV2"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Better than the Original!");
		}

		public override void SetDefaults()
		{
			Item.damage = 50;
			Item.DamageType = DamageClass.Ranged;

			Item.width = 118; //Witdh and Height of the Image!
			Item.height = 56;

			Item.shootSpeed = 10f;	//Shoot Speed in tooltip Description

			Item.noMelee = true;

			Item.useTime = 20;		//Fire Rate of The Weapon
			Item.useAnimation = 20; //Animation is 20 ticks long 

			Item.useStyle = 5;	//Guns and Ranger Weapons use Use Style Id 5!

			Item.knockBack = 6; 
			Item.value = Item.buyPrice(gold: 20); //Now its in Gold Coins


			Item.rare = ItemRarityID.Blue ;					//Changes the Item Color (The Name)
			Item.UseSound = SoundID.Item1;					//  The sound it makes when shooting 
			Item.autoReuse = true;

			Item.useAmmo = AmmoID.Bullet; 	//Consumes the Bullet 

			Item.shoot = ProjectileID.Bullet; 	//Shoots the current bullet in Player's inv (example: Chloro Bullet)
		}






		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)		//This overides the Item.Shoot = ProjectileID.Bullet!!!
        {
			int x = 2; //The ammount of Projectiles that should be shot

            Vector2 offset = velocity * 3;
            position += offset;
            for (var i = 0; i < x; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.ToRadians(10 * i));
                type = ProjectileID.BlackBolt;
                int newProjectile = Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), position, perturbedSpeed, type, damage, knockback, player.whoAmI);

                // Apply the 33% chance to not consume ammo
                if (Main.rand.NextFloat() > 0.33f)
                {
                    Main.projectile[newProjectile].noDropItem = false;
                }
            }
            return false;
        }






		
		public override void AddRecipes()			//Crafting recipe!
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddIngredient(ItemID.Minishark, 1);		//the 1 and 10 represents the ammount


			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}