using Godot;
using System;

public class Player : Area2D {

	[Signal]
	public delegate void PlayerHit(int PlayerLife);
	
	[Signal]
	public delegate void Killed();
	
	[Export]
	public PackedScene Projectile;
	
	private int Speed = 400;
	private int PlayerLife = 1000;

	public Vector2 _screenSize;
	private CollisionShape2D _collisionCapsule;
	
	public override void _Ready() {
		_screenSize = GetViewport().GetSize();
		_collisionCapsule = (CollisionShape2D) GetNode("CollisionShape2D");
		Hide();
	}

	public void DecreaseLife(int damage) {
		PlayerLife = PlayerLife - damage;
		
		if (PlayerLife <= 0) {
			PlayerDied();
		}		
		//must implement here some animatated vanishing for the player, so he wont get hit again till some time
	}

	private void PlayerDied() {

		Hide(); // Player disappears after being killed
		EmitSignal("Killed");

		_collisionCapsule.Disabled = true;
		PlayerLife = 1000;
		//QueueFree();
	}
	
	public void Start(Vector2 pos) {
		
		Position = pos;
		Show();
		var collisionShape2D = (CollisionShape2D) GetNode("CollisionShape2D");
		collisionShape2D.Disabled = false;
	}
	
	public void Fire() {
		
		var bulletInstance = (Bullet) Projectile.Instance();
		GetParent().AddChild(bulletInstance);
		bulletInstance.Start(Position, Rotation);
	}
	
	public int getPlayerLife() {
		return PlayerLife;
	}
	
	public int getSpeed() {
		return Speed;
	}
}