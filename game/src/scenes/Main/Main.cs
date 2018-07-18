using Godot;
using System;

public class Main : Node {
	
	[Export]
	public PackedScene Mob;
	public int Score = 0;
	private Random rand = new Random();
	private float PI = 3.1415F;
	
	public override void _Ready() { }

//  public override void _Process(float delta) { }

	private float RandRand(float min, float max) {
		
		return (float) (rand.NextDouble() * (max - min) + min);
	}
	
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
		hud.ShowMessage("Get Ready!");
		
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
		
		// Choose a random location on Path2D.
		var mobSpawnLocation = (PathFollow2D) GetNode("MobPath/MobSpawn");
		mobSpawnLocation.SetOffset(rand.Next());
	
		// Create a Mob instance and add it to the scene.
		var mobInstance = (RigidBody2D) Mob.Instance();
		AddChild(mobInstance);
	
		// Set the mob's direction perpendicular to the path direction.
		var direction = mobSpawnLocation.Rotation + PI / 2;
	
		// Set the mob's position to a random location.
		mobInstance.Position = mobSpawnLocation.Position;
	
		// Add some randomness to the direction.
		direction += RandRand(-PI / 4, PI / 4);
		mobInstance.Rotation = direction;
	
		// Choose the velocity.
		mobInstance.SetLinearVelocity(new Vector2(RandRand(150f, 250f), 0).Rotated(direction));
	}
	
}