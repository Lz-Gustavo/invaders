using Godot;
using System;

public class PlayerController : AnimatedSprite {
	
	private Player Player1;
	private AnimatedSprite Sprite;

	public override void _Ready() {
		
		Player1 = (Player) GetParent();
		//Sprite = (AnimatedSprite) GetNode("AnimatedSprite");
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
	
		//var Sprite = (AnimatedSprite) GetNode("AnimatedSprite");
		
		if (velocity.Length() > 0) {
			velocity = velocity.Normalized() * Player1.getSpeed();
			Play();
		} else {
			Stop();
		}
		
		//Clamping a value means restricting it to a given range
		Player1.Position += velocity * delta;
		Player1.Position = new Vector2(
			Mathf.Clamp(Player1.Position.x, 0, Player1._screenSize.x),
			Mathf.Clamp(Player1.Position.y, 0, Player1._screenSize.y)
		);
		
		if (velocity.x > 0) {
			Animation = "right";
			//Sprite.FlipH = velocity.x < 0;
			//Sprite.FlipV = false;
		}
		else if (velocity.x < 0) {
			Animation = "left";
			//Sprite.FlipH = velocity.x < 0;
			//Sprite.FlipV = false;	
		}
		else if(velocity.y != 0) {
			Animation = "up";
			//Sprite.FlipV = velocity.y > 0;
		}      
	}
}
