using Godot;
using System;

public class Player : Area2D {
	
	[Signal]
	public delegate void Hit();

	public int Speed = 400;

	private Vector2 _screenSize;

	public override void _Ready() {
		_screenSize = GetViewport().GetSize();
		Hide();
	}

	public override void _Process(float delta) {
	
		var velocity = new Vector2(); // The player's movement vector.
	
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
		
		if (velocity.x != 0) {
			Sprite.Animation = "right";
			Sprite.FlipH = velocity.x < 0;
			Sprite.FlipV = false;
		} else if(velocity.y != 0) {
			Sprite.Animation = "up";
			Sprite.FlipV = velocity.y > 0;
		}
	}

	private void _on_Player_body_entered(Godot.Object body) {
	
		Hide(); // Player disappears after being hit.
		EmitSignal("Hit");
	
		// For the sake of this example, but it's better to create a class var
		// then assign the variable inside _Ready()
		var collisionShape2D = (CollisionShape2D) GetNode("CollisionShape2D");
		collisionShape2D.Disabled = true;
	}
	
	public void Start(Vector2 pos) {
		
		Position = pos;
		Show();
		var collisionShape2D = (CollisionShape2D) GetNode("CollisionShape2D");
		collisionShape2D.Disabled = false;
	}
}