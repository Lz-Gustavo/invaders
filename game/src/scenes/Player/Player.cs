using Godot;
using System;

public class Player : Area2D {

	[Signal]
	public delegate void PlayerHit(int PlayerLife);
	
	[Signal]
	public delegate void Killed();
	
	private int Speed = 400;
	private int PlayerLife = 1000;

	private Vector2 _screenSize;
	private CollisionShape2D _collisionCapsule;

	public override void _Ready() {
		_screenSize = GetViewport().GetSize();
		_collisionCapsule = (CollisionShape2D) GetNode("CollisionShape2D");
		Hide();
	}

	public override void _Process(float delta) {
	
		// Player's movement vector
		var velocity = new Vector2();
	
		if (Input.IsActionPressed("ui_right")) {
			velocity.x += 1;
		}
	
		if (Input.IsActionPressed("ui_left")) {
			velocity.x -= 1;
		}
	
		if (Input.IsActionPressed("ui_down")) {
			velocity.y += 1;
		}
	
		if (Input.IsActionPressed("ui_up")) {
			velocity.y -= 1;
		}
	
		var Sprite = (AnimatedSprite) GetNode("AnimatedSprite");
		
		if (velocity.Length() > 0) {
			velocity = velocity.Normalized() * Speed;
			Sprite.Play();
		} else {
			Sprite.Stop();
		}
		
		//Clamping a value means restricting it to a given range
		Position += velocity * delta;
		Position = new Vector2(
			Mathf.Clamp(Position.x, 0, _screenSize.x),
			Mathf.Clamp(Position.y, 0, _screenSize.y)
		);
		
		if (velocity.x > 0) {
			Sprite.Animation = "right";
			//Sprite.FlipH = velocity.x < 0;
			//Sprite.FlipV = false;
		}
		else if (velocity.x < 0) {
			Sprite.Animation = "left";
			//Sprite.FlipH = velocity.x < 0;
			//Sprite.FlipV = false;	
		}
		else if(velocity.y != 0) {
			Sprite.Animation = "up";
			//Sprite.FlipV = velocity.y > 0;
		}
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
	
	public int getPlayerLife() {
		return PlayerLife;
	}
}