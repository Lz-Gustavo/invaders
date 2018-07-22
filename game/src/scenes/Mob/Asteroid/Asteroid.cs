using Godot;
using System;

public class Asteroid : Mob {
	
	public override void _Ready() {
		MinSpeed = 180F;
		MaxSpeed = 250F;
		Damage = 1000;
		StartAnimation();
	}
}