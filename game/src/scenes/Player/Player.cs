/*	Invaders 2D game using Godot							*/
/*											*/
/*	"Player.cs" is attached to the main node on the Player.tscn scene and is	*/
/*	responsible for implementing the main state changing functions of the Player, 	*/
/*	containing its status attributes, start-up and destruction procedures.		*/
/*	Interacts with "PlayerController.cs" and "PlayerDamageAdapter.cs".		*/
/*											*/
/*	developed by: LzGustavo						July/2018	*/

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
	}

	private void PlayerDied() {
		// just hides player object and restores its hp, dont need to delete
		// and instanciate again

		Hide();
		EmitSignal("Killed");

		_collisionCapsule.Disabled = true;
		PlayerLife = 1000;
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