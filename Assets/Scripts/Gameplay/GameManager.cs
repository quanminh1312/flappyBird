using UnityEngine;
using UnityEngine.UI;

public class GameManager : IntEventInvoker
{
    public GameObject gameOver;
    [SerializeField]
    GameObject hud;
    public static bool canResume = true;
    public int score { get; private set; }

    bool isactive = false;
    private void Start()
    {
        unityEvents.Add(EventName.GameStartedEvent, new GameStartedEvent());
        EventManager.AddInvoker(EventName.GameStartedEvent, this);

        EventManager.AddListener(EventName.GameOverEvent, HandleGameOverEvent);
    }
    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            MenuManager.GoToMenu(MenuName.Pause);
            AudioManager.Play(AudioClipName.Swoosh);
        } 
        else if (!isactive)
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                AudioManager.Play(AudioClipName.Flap);
                unityEvents[EventName.GameStartedEvent].Invoke(0);
                isactive = true;
            }
    }
    void HandleGameOverEvent(int unused)
    {
        canResume = false;
        EndGame();
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        AudioManager.Play(AudioClipName.Swoosh);
        MenuManager.GoToMenu(MenuName.Pause);
    }
    void EndGame()
    {
        SetHighScore();
        MenuManager.GoToMenu(MenuName.Pause);
    }
    void SetHighScore()
    {
        HUD hudScript = hud.GetComponent<HUD>();
        int currentScore = hudScript.Score;
        if (PlayerPrefs.HasKey("High Score"))
        {
            if (currentScore > PlayerPrefs.GetInt("High Score"))
            {
                PlayerPrefs.SetInt("High Score", currentScore);
                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetInt("High Score", currentScore);
            PlayerPrefs.Save();
        }
    }

}
