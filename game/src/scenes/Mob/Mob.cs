using Godot;
using System;

public abstract class Mob : RigidBody2D, MobInterface {
	
	protected float MinSpeed;
	protected float MaxSpeed;
	protected int Damage;
	protected Random rand = new Random();
	
	protected float RandRand(float min, float max) {
		
		return (float) (rand.NextDouble() * (max - min) + min);
	}
	
	protected void StartAnimation() {
		var Sprite = (AnimatedSprite) GetNode("AnimatedSprite");
		Sprite.Play();
	}
	
	public int getDamage() {
		return Damage;
	}
	
	public void ScreenExited() {
    	QueueFree();
	}
	
	public Vector2 getVelocity(float direction) {

		return (new Vector2(RandRand(MinSpeed, MaxSpeed), 0).Rotated(direction));
	}
}