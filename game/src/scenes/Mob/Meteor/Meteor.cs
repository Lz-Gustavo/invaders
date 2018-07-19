using Godot;
using System;

public class Meteor : Mob {

	private float MinSpeed = 100F;
	private float MaxSpeed = 150F;
	private int Damage = 5000;
	
	public Vector2 getVelocity(float direction) {

		return (new Vector2(RandRand(MinSpeed, MaxSpeed), 0).Rotated(direction));
	}
}
