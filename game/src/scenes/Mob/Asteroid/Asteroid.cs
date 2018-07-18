using Godot;
using System;

public class Asteroid : Mob {
	
//	[Export]
//	public int MinSpeed = 150;
//
//	[Export]
//	public int MaxSpeed = 250;
	
	private float MinSpeed = 180F;
	private float MaxSpeed = 250F;
	
	public Vector2 getVelocity(float direction) {

		return (new Vector2(RandRand(MinSpeed, MaxSpeed), 0).Rotated(direction));
	}
}
