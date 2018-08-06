/*	Invaders 2D game using Godot							*/
/*											*/
/*	"Main.cs" is the main scene of the game. Being active on the whole course of	*/
/*	it, the class implements some core procedures of starting a new game, finishing */
/*	the state of an active game, managing of the HUD score elements and calls to	*/
/*	MobFactory::Spawn to create new mobs on every tick of the MobTimer.		*/
/*											*/
/*	developed by: LzGustavo						July/2018	*/

using Godot;
using System;

public class Main : Node {
	
	private int Time;
	private int Score = 0;
	
	public override void _Ready() {}
	
	public void GameOver() {
		
		var music = (AudioStreamPlayer) GetNode("Music");
		music.Stop();
		
		var deathSound = (AudioStreamPlayer) GetNode("DeathSound");
		deathSound.Play();
		
		var mobTimer = (Timer) GetNode("MobTimer");
		mobTimer.Stop();
		
		var scoreTimer = (Timer) GetNode("ScoreTimer");
		scoreTimer.Stop();
		
		var hud = (HUD) GetNode("HUD");
		hud.ShowGameOver();
	}
	
	public void NewGame() {
		
		Time = 0;
		var hud = (HUD) GetNode("HUD");
		hud.UpdateTime(Time);
		hud.ShowMessage("Ready");
		
		Score = 0;
		hud.UpdateEnemies(Score);
		
		var player = (Player) GetNode("Player");
		var startPosition = (Position2D) GetNode("StartPosition");
		player.Start(startPosition.Position);
		
		var startTimer = (Timer) GetNode("StartTimer");
		startTimer.Start();
		
		var music = (AudioStreamPlayer) GetNode("Music");
		music.Play();
	}
	
	public void OnStartTimerTimeout() {
		
		var mobTimer = (Timer) GetNode("MobTimer");
		mobTimer.Start();
		
		var scoreTimer = (Timer) GetNode("ScoreTimer");
		scoreTimer.Start();
	}
	
	public void OnScoreTimerTimeout() {
		Time += 1;
		var hud = (HUD) GetNode("HUD");
		hud.UpdateTime(Time);
	}
	
	public void OnMobTimerTimeout() {
		
		var factory = (MobFactory) GetNode("MobPath");
		
		var rand = new Random();
		var choice = rand.Next(100);
		
		if (choice < 60 ) {
			factory.Spawn("asteroid");
		}
		else if ((choice >= 60) && (choice < 95)){
			factory.Spawn("meteor");
		}
		else {
			factory.Spawn("comet");
		}
	}
	public void IncreaseScore() {
		Score += 1;
		var hud = (HUD) GetNode("HUD");
		hud.UpdateEnemies(Score);
	}
}