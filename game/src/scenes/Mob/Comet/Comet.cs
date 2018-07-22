using Godot;
using System;

public class Comet : Mob {
	
	public override void _Ready() {
		MinSpeed = 260F;
		MaxSpeed = 320F;
		Damage = 4000;
		StartAnimation();
	}
}