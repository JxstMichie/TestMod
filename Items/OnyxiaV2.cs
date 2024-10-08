using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;
using System;
using Microsoft.Xna.Framework;

namespace TestMod.Items //Directory : Modname.Foldername
{
	public class OnyxiaV2 : ModItem //Name must be file name! this does not appear in-game!
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Onyxia"); //This is the Ingame Item name!
			Tooltip.SetDefault("Like the original!"); //this is the Item Description!
		}

		public override void SetDefaults()
		{
			Item.damage = 130; //Item damage (Includes the projectile damage!)
			Item.DamageType = DamageClass.Ranged;

			Item.width = 118; //Width of the Image!
			Item.height = 56; //Height of the Image!

			Item.noMelee = true; //Should it do damage on swing? Do not set to false for ranger weapons!

			Item.useTime = 10; //Fire rate
			Item.shootSpeed = 15f;	//Shoot Speed in tooltip Description
			Item.useAnimation = 10;	//Animation duration

			// Item use style (Should it swing , hold or swing it like a flail?) ID 5 is used for ranged weapons!
			Item.useStyle = 5;
			
			// Knockback modifier
			Item.knockBack = 5;

			// Base sell price
			Item.value = Item.buyPrice(gold: 10);

			// Changes the item rarity color
			Item.rare = ItemRarityID.Master; 

			//Auto shooting enabled
			Item.autoReuse = true; 	

			// Play shotgun sound when shot
			Item.UseSound = SoundID.Item1; 

			// Ammo it should consume
			Item.useAmmo = AmmoID.Bullet;
			
			// Note for ProjectileID.Bullet , This uses the bullet type in your inventory
			
			// The projectile it should shoot
			Item.shoot = ProjectileID.Bullet; 
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int x = 3; // The amount of projectiles that should be shot

			Vector2 offset = velocity * 2;
			position += offset;

			int Bullet = type;

			Vector2 middleOffset = new Vector2(0, 0);
			Vector2 upperOffset = new Vector2(0, -2); // Adjusted to spawn 2 pixels above the blackbolt
			Vector2 lowerOffset = new Vector2(0, 2); // Adjusted to spawn 2 pixels below the blackbolt

			for (var i = 0; i < x; i++)
			{
				Vector2 perturbedSpeed;

				if (i == 0) // First projectile, bullet 2 pixels above the blackbolt
				{
					perturbedSpeed = velocity;
					perturbedSpeed += upperOffset;
				}

				else if (i == 1) // Second projectile, blackbolt in the middle
				{
					perturbedSpeed = velocity;
					type = ProjectileID.BlackBolt;
				}
				
				else // Third projectile, bullet 2 pixels below the blackbolt
				{
					perturbedSpeed = velocity;
					perturbedSpeed += lowerOffset;

					type = Bullet;
				}

				

				int newProjectile = Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), position, perturbedSpeed, type, damage, knockback, player.whoAmI);
			}

			return false;
		}


		public override void AddRecipes()			//Crafting recipe!
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddIngredient(ItemID.Minishark, 1);		//the 1 and 10 represents the ammount


			recipe.AddTile(TileID.Anvils);
			recipe.Register(); // Adds the Recipe to the game
		}
	}
}
