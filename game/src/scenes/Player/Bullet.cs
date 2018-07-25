using Godot;
using System;

public class Bullet : RigidBody2D {
	
	private float Speed = 500F;
	
	private float PI = 3.1415F;
	
	public void ScreenExited() {
		QueueFree();
	}
	
	public Vector2 getVelocity(float direction) {
		
		return (new Vector2(Speed, 0).Rotated(direction));
	}

	public void Start(Vector2 PlayerPos, float PlayerRot) {
		
		Position = PlayerPos;
		var direction = PlayerRot + (3F/2)*PI;
		Rotation = direction + PI/2;
		
		var velocity = (Vector2) getVelocity(direction);
		SetLinearVelocity(velocity);
		
		var collisionShape2D = (CollisionShape2D) GetNode("CollisionShape2D");
		collisionShape2D.Disabled = false;
		
		var sound = (AudioStreamPlayer2D) GetNode("Shot");
		sound.Play();
	} 
	
	public void DestroyMob(Godot.Object body) {
 		
		if (body is Mob) {
			var mainInstance = (Main) GetParent();
			mainInstance.IncreaseScore();
			
			var rock = (Mob) body;
			rock.Destroy();
			
			//destroy its own instance too
			ScreenExited();
		}
	}
}