using Godot;
using System;

public abstract class Mob : RigidBody2D, MobInterface {
	
	[Signal]
	public delegate void Hit();
	
	private int Damage = 1000;
	
	
	//private String[] _mobTypes = {"walk", "swim", "fly"};
	private Random rand = new Random();
	
	protected float RandRand(float min, float max) {
		
		return (float) (rand.NextDouble() * (max - min) + min);
	}
	
	public override void _Ready() {
		
	    var Sprite = (AnimatedSprite) GetNode("AnimatedSprite");	
	    //var randomMob = new Random();
	    //Sprite.Animation = _mobTypes[randomMob.Next(0, _mobTypes.Length)];
		Sprite.Play();
	}

	public int getDamage() {
		return Damage;
	}
	
	public void ScreenExited() {
    	QueueFree();
	}
}
