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
			DisplayName.SetDefault("OnyxiaV2"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
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

			Item.useTime = 10;		//Fire Rate of The Weapon
			Item.useAnimation = 10; //Animation is 20 ticks long 

			Item.useStyle = 5;	//If the weapon should be held like a ranged weapon or swinged like a sword or used like a flail?

			Item.knockBack = 6; //Knockback against enemies
			Item.value = Item.buyPrice(gold: 20); //Sell Base Value


			Item.rare = ItemRarityID.Blue ;					//Changes the Item Color (The Name)
			Item.UseSound = SoundID.Item1;					//  The sound it makes when shooting 
			Item.autoReuse = true;

			Item.useAmmo = AmmoID.Bullet; 	//Consumes the Bullet 

			Item.shoot = ProjectileID.Bullet; 	//Shoots the current bullet in Player's inv (example: Chloro Bullet)
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
			recipe.Register();
		}
	}
}