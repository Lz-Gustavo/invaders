/*	Invaders 2D game using Godot							*/
/*											*/
/*	"PlayerController.cs" is attached to the animation child node of the player	*/
/*	scene, it represents an implementation of the MVC pattern. Being the controller,*/
/*	the class is responsible for interp. keyboard inputs that result in a state	*/
/*	change for the model (player class) and the view (animated sprite class		*/
/*	attached on the controller), providing an easy-to-modify interface and		*/
/*	modularity for the game code.							*/
/*											*/
/*	developed by: LzGustavo						July/2018	*/

using Godot;
using System;

public class PlayerController : AnimatedSprite {
	
	private Player Player1;
	//private AnimatedSprite Sprite;

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
		}
		else if (velocity.x < 0) {
			Animation = "left";	
		}
		else if(velocity.y != 0) {
			Animation = "up";
		}
		
		if (Input.IsActionJustPressed("ui_select")) {
			Player1.Fire();
		}
	}
}
