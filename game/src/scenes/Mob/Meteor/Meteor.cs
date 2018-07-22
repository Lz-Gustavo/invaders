using Godot;
using System;

public class Meteor : Mob {

	public override void _Ready() {
		MinSpeed = 100F;
		MaxSpeed = 150F;
		Damage = 5000;
		StartAnimation();
	}
}
