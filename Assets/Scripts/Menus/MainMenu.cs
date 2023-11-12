using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Listens for the OnClick events for the main menu buttons
/// </summary>
public class MainMenu : MonoBehaviour
{
	/// <summary>
	/// Handles the on click event from the play button
	/// </summary>
    public void HandlePlayButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.Swoosh);
        MenuManager.GoToMenu(MenuName.Play);
	}

	/// <summary>
	/// Handles the on click event from the high score button
	/// </summary>
	public void HandleHighScoreButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.Swoosh);
        MenuManager.GoToMenu(MenuName.HighScore);
    }
} 
