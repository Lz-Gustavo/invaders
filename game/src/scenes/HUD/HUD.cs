using Godot;
using System;

public class HUD : Godot.CanvasLayer {
	
	[Signal]
	public delegate void StartGame();

	private Label messageLabel;
	private Label scoreLabel;
	private Button startButton;
	private Timer messageTimer;
	private ProgressBar playerHP;

	public override void _Ready() {
		
		messageLabel = (Label) GetNode("MessageLabel");
		scoreLabel = (Label) GetNode("ScoreLabel");
		messageTimer = (Timer) GetNode("MessageTimer");
		startButton = (Button) GetNode("StartButton");
		playerHP = (ProgressBar) GetNode("PlayerHP");
		playerHP.Hide();
	}
	
	public void ShowMessage(string text) {
		
		messageLabel.Text = text;
		messageLabel.Show();
		messageTimer.Start();
	}
	
	async public void ShowGameOver() {
		
		ShowMessage("Game Over");
		await ToSignal(messageTimer, "timeout");
		messageLabel.Text = "Te vira \nnegão!";
		messageLabel.Show();
		startButton.Show();
		playerHP.Hide();
	}
	
	public void UpdateScore(int score) {
		
		scoreLabel.Text = score.ToString();
	}
	
	public void OnStartButtonPressed() {
		
		startButton.Hide();
		playerHP.Show();
		EmitSignal("StartGame");
	}

	public void OnMessageTimerTimeout() {
		
		messageLabel.Hide();
	}
	
	public void UpdateLife(int life) {
		
		playerHP.Value = life;
	}
}