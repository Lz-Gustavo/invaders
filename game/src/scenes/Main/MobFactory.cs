/*	Invaders 2D game using Godot							*/
/*											*/
/*	"MobFactory.cs" as the name implies, implements the Factory design pattern in 	*/
/*	order to facilitate the mobs creation during the course of the game. It sets	*/
/*	some specific trajectory rotation for each mob and creates them on a random	*/
/*	point following the 2D Path node that it is attached.				*/
/*											*/
/*	developed by: LzGustavo						July/2018	*/

using Godot;
using System;

public class MobFactory : Path2D {
	
    	[Export]
	public PackedScene Asteroid;
	
	[Export]
	public PackedScene Comet;
	
	[Export]
	public PackedScene Meteor;
	
	private Random rand = new Random();
	private float PI = 3.1415F;

	private float RandRand(float min, float max) {
		return (float) (rand.NextDouble() * (max - min) + min);
	}
	
	public void Spawn(string type) {
		
		var mobSpawnLocation = (PathFollow2D) GetNode("MobSpawn");
		mobSpawnLocation.SetOffset(rand.Next());
	
		// Set the mob's direction perpendicular to the path direction
		var direction = mobSpawnLocation.Rotation + PI / 2;
	
		if (type == "asteroid") {
			
			var mobInstance = (Asteroid) Asteroid.Instance();
			AddChild(mobInstance);
			
			mobInstance.Position = mobSpawnLocation.Position;
			direction += RandRand((0.4F * PI), (0.6F * PI));
			mobInstance.Rotation = direction;
			
			var velocity = (Vector2) mobInstance.getVelocity(direction);
			mobInstance.SetLinearVelocity(velocity);
		}
		else if (type == "meteor") {
			
			var mobInstance = (Meteor) Meteor.Instance();
			AddChild(mobInstance);
			
			mobInstance.Position = mobSpawnLocation.Position;
			direction += RandRand((0.4F * PI), (0.6F * PI));
			
			//diferent rotation due to its sprite orientation
			mobInstance.Rotation = direction + PI/6;
			
			var velocity = (Vector2) mobInstance.getVelocity(direction);
			mobInstance.SetLinearVelocity(velocity);
		}
		else if (type == "comet") {
			
			var mobInstance = (Comet) Comet.Instance();
			AddChild(mobInstance);
			
			mobInstance.Position = mobSpawnLocation.Position;
			direction += RandRand((0.4F * PI), (0.6F * PI));
			
			//diferent rotation due to its sprite orientation
			mobInstance.Rotation = direction - PI/2;
			
			var velocity = (Vector2) mobInstance.getVelocity(direction);
			mobInstance.SetLinearVelocity(velocity);
		}
	}
}