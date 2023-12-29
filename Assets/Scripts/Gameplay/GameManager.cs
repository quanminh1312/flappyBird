using UnityEngine;
using UnityEngine.UI;

public class GameManager : IntEventInvoker
{
    public static GameManager instance = null;
    public GameObject gameOver;
    [SerializeField]
    GameObject hud;
    public static bool canResume = true;
    public int score { get; private set; }

    bool isactive = false;
    public GameObject eventSystem = null;
    public Player player = null;
    public void SetActiveEventSystem()
    {
        eventSystem.gameObject.SetActive(true);
    }
    private void Start()
    {
        instance = this;
        unityEvents.Add(EventName.GameStartedEvent, new GameStartedEvent());
        EventManager.AddInvoker(EventName.GameStartedEvent, this);

        EventManager.AddListener(EventName.GameOverEvent, HandleGameOverEvent);
    }
    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            player.enable = false;
            eventSystem.gameObject.SetActive(false);
            MenuManager.GoToMenu(MenuName.Pause);
            AudioManager.Play(AudioClipName.Swoosh);
        } 
        else if (!isactive)
            if (Input.GetKeyDown(KeyCode.Space))
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
        player.enable = false;
        eventSystem.gameObject.SetActive(false);
        AudioManager.Play(AudioClipName.Swoosh);
        MenuManager.GoToMenu(MenuName.Pause);
    }
    void EndGame()
    {
        SetHighScore();
        eventSystem.gameObject.SetActive(false);
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
