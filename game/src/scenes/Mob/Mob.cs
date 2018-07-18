using Godot;
using System;

public class Mob : RigidBody2D {
	
	[Export]
	public int MinSpeed = 150; // min speed

	[Export]
	public int MaxSpeed = 250; // max speed

	private String[] _mobTypes = {"walk", "swim", "fly"};

	public override void _Ready() {
		
	    var Sprite = (AnimatedSprite) GetNode("AnimatedSprite");	
	    var randomMob = new Random();
		
		// substituir por factory
	    Sprite.Animation = _mobTypes[randomMob.Next(0, _mobTypes.Length)];
		Sprite.Play();

	}
	
//	public override void _Process(float delta) { }

	private void _on_Visibility_screen_exited() {
    	QueueFree();
	}

}
