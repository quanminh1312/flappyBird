using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Retrieves and displays high score and listens for
/// the OnClick events for the high score menu button
/// </summary>
public class HighScoreMenu : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI message;

	/// <summary>
	/// Start is called before the first frame update
	/// </summary>
	void Start()
    {
		// retrieve and display high score
		if (PlayerPrefs.HasKey("High Score"))
        {
			message.text = "Your High Score: " + PlayerPrefs.GetInt("High Score");
		}
        else
        {
			message.text = "High Score\nNo games played yet";
		}
	}
    private void Update()
    {
        
    }
    /// <summary>
    /// Handles the on click event from the quit button
    /// </summary>
    public void HandleQuitButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.Swoosh);
		MenuManager.GoToMenu(MenuName.Main);
	}
}
