using Godot;
using System;

public class Main : Node {
	
	public int Score = 0;
	
	public override void _Ready() { }
	
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
		
		Score = 0;
		var hud = (HUD) GetNode("HUD");
		hud.UpdateScore(Score);
		hud.ShowMessage("Ready");
		
		var player = (Player) GetNode("Player");
		var startPosition = (Position2D) GetNode("StartPosition");
		player.Start(startPosition.Position);
		
		var startTimer = (Timer) GetNode("StartTimer");
		startTimer.Start();
		
		var music = (AudioStreamPlayer) GetNode("Music");
		music.Play();
	}
	
	public void OnStartTimerTimeout() {
		
		//timers
		var mobTimer = (Timer) GetNode("MobTimer");
		mobTimer.Start();
		
		var scoreTimer = (Timer) GetNode("ScoreTimer");
		scoreTimer.Start();
	}
	
	public void OnScoreTimerTimeout() {
		Score += 1;
		var hud = (HUD) GetNode("HUD");
		hud.UpdateScore(Score);
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
}