using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Pauses and unpauses the game. Listens for the OnClick 
/// events for the pause menu buttons
/// </summary>
public class PauseMenu : IntEventInvoker
{
	/// <summary>
	/// Start is called before the first frame update
	/// </summary>

	[SerializeField]
	GameObject resume;
    [SerializeField]
    Image medal;
    [SerializeField]
    Text score;
    [SerializeField]
    Text best;
    [SerializeField]
    List<Sprite> Sprite;
    int intScore;

    public GameObject eventSystem = null;
    void Start()
    {
        // pause the game when added to the scene
		Time.timeScale = 0;
        intScore = HUD.score;

		score.text = intScore.ToString();
        if (PlayerPrefs.HasKey("High Score") && (int)PlayerPrefs.GetInt("High Score") > intScore)
        {
            best.text = PlayerPrefs.GetInt("High Score").ToString();
        } 
        else best.text = intScore.ToString();
        
        if (intScore > 10  && intScore <= 20)
        {
            medal.sprite = Sprite[1];
        }
        else if (intScore > 20 && intScore <=30)
        {
            medal.sprite = Sprite[2];
        } 
        else if(intScore > 30)
        {
            medal.sprite = Sprite[3];
        }
    }

	/// <summary>
	/// Handles the on click event from the Resume button
	/// </summary>
    public void HandleResumeButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.Swoosh);
        Time.timeScale = 1;
        eventSystem.gameObject.SetActive(false);
        GameManager.instance.SetActiveEventSystem();
        GameManager.instance.player.enable = true;
        if (GameManager.canResume)
        {
            Destroy(gameObject);
        }
        else
        {
            GameManager.canResume = true;
            MenuManager.GoToMenu(MenuName.Play);
        }
        
	}

	/// <summary>
	/// Handles the on click event from the Quit button
	/// </summary>
	public void HandleQuitButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.Swoosh);

        // unpause game, destroy menu, and go to main menu
        Time.timeScale = 1;
		Destroy(gameObject);
		MenuManager.GoToMenu(MenuName.Main);
	}
}
