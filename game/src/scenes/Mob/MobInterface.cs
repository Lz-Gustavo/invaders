using Godot;
using System;

public interface MobInterface {
	
	void ScreenExited();
	Vector2 getVelocity(float direction);
	int getDamage();
}