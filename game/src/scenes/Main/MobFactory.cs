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
	
		// Set the mob's direction perpendicular to the path direction.
		var direction = mobSpawnLocation.Rotation + PI / 2;
	
		if (type == "asteroid") {
			
			var mobInstance = (Asteroid) Asteroid.Instance();
			AddChild(mobInstance);
			
			// Set the mob's position to a random location.
			mobInstance.Position = mobSpawnLocation.Position;
		
			// Add some randomness to the direction.
			direction += RandRand(-PI / 4, PI / 4);
			mobInstance.Rotation = direction;
		
			// Choose the velocity.
			var velocity = (Vector2) mobInstance.getVelocity(direction);
			mobInstance.SetLinearVelocity(velocity);
		}
		else if (type == "meteor") {
			
			var mobInstance = (Meteor) Meteor.Instance();
			AddChild(mobInstance);
			
			// Set the mob's position to a random location.
			mobInstance.Position = mobSpawnLocation.Position;
		
			// Add some randomness to the direction.
			direction += RandRand(-PI / 4, PI / 4);
			mobInstance.Rotation = direction + PI/6;
		
			// Choose the velocity.
			var velocity = (Vector2) mobInstance.getVelocity(direction);
			mobInstance.SetLinearVelocity(velocity);
		}
		else if (type == "comet") {
			
			var mobInstance = (Comet) Comet.Instance();
			AddChild(mobInstance);
			
			// Set the mob's position to a random location.
			mobInstance.Position = mobSpawnLocation.Position;
		
			// Add some randomness to the direction.
			direction += RandRand(-PI / 4, PI / 4);
			mobInstance.Rotation = direction - PI/2;
		
			// Choose the velocity.
			var velocity = (Vector2) mobInstance.getVelocity(direction);
			mobInstance.SetLinearVelocity(velocity);
		}
	}
}
