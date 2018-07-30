/*	Invaders 2D game using Godot							*/
/*											*/
/*	"PlayerDamageAdapter.cs" is the implementation of the adapter design pattern.	*/
/* 	Attached to the player collision sphere, it is called on every collision with	*/
/*	a Mob object, translating its life damage into the player s life scale and	*/
/*	calling the Player::DecreaseLife with the correct value.			*/
/*											*/
/*	developed by: LzGustavo						July/2018	*/

using Godot;
using System;

public class PlayerDamageAdapter : CollisionShape2D {

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
}