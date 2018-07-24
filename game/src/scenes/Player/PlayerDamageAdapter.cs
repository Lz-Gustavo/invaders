using Godot;
using System;

public class PlayerDamageAdapter : CollisionShape2D {

	// maybe its better declare here than get Node everytime
	private Player _player;
	
	public override void _Ready() {
		_player = (Player) GetParent();
	}
	
	public void DamagePlayer(Godot.Object body) {
		
		if (body is Mob) {
			
			var structure = (Mob) body;
			var damage = (int) structure.getDamage();

			var RealDamage = (int) damage / 10;
			_player.DecreaseLife(RealDamage);
			_player.EmitSignal("PlayerHit", _player.getPlayerLife());
		}
	}
	
//	private void PlayerBodyEntered(Godot.Object body) {
//
//		Hide(); // Player disappears after being hit.
//		EmitSignal("Hit");
//
//		// For the sake of this example, but it's better to create a class var
//		// then assign the variable inside _Ready()
//		var collisionShape2D = (CollisionShape2D) GetNode("CollisionShape2D");
//		collisionShape2D.Disabled = true;
//	}
}