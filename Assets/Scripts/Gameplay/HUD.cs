using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// The HUD for the game
/// </summary>
public class HUD : MonoBehaviour
{
	#region Fields

	// score support
	[SerializeField]
	TextMeshProUGUI scoreText;
    [SerializeField]
    GameObject GameOver;
    public static int score = 0;

	#endregion

	/// <summary>
	/// Start is called before the first frame update
	/// </summary>
	void Start()
    {
		// add listener for PointsAddedEvent
		EventManager.AddListener(EventName.PointsAddedEvent, HandlePointsAddedEvent);
		EventManager.AddListener(EventName.GameOverEvent, HandleGameOverEvent);
		// initialize score text
		scoreText.text = score.ToString();
		GameOver.SetActive(false); 
	}

	#region Properties

	/// <summary>
	/// Gets the score
	/// </summary>
	/// <value>the score</value>
	public int Score
    {
		get { return score; }
	}

	#endregion

	#region Private methods

	/// <summary>
	/// Handles the points added event by updating the displayed score
	/// </summary>
	/// <param name="points">points to add</param>
	private void HandlePointsAddedEvent(int points)
    {
		score += points;
		scoreText.text = score.ToString();
	}
    private void HandleGameOverEvent(int notUse) 
	{
        GameOver.SetActive(true);
    }
    #endregion
}
