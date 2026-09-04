using TMPro;
using UnityEngine;

public class IUManager : MonoBehaviour
{
    public static IUManager Instance;

    [SerializeField] private TextMeshProUGUI scoreCar;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject mainMenuPanel;
    private float score = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        GameManager.PauseEvent += Pause;
        GameManager.ResumeEvent += Resume;
        GameManager.GameOverEvent += GameOver;
        GameManager.MainMenuEvent += StartMenu;
        GameManager.PlayGameEvent += Play;
    }


    private void OnDisable()
    {
        GameManager.PauseEvent -= Pause;
        GameManager.ResumeEvent -= Resume;
        GameManager.GameOverEvent -= GameOver;
        GameManager.MainMenuEvent -= StartMenu;
        GameManager.PlayGameEvent -= Play;
    }

    private void Start()
    {
        scoreCar.text = "Puntaje: " + score.ToString();
    }

    public void AddScore()
    {
        score++;
        scoreCar.text = "Puntaje: " + GameManager.Instance.ChageGameShot.ToString();
    }

    public void OnPlayButton()
    {
        GameManager.Instance.StartGame();
    }
    public void OnPauseButton()
    {
        GameManager.Instance.PauseGame();
    }
    public void OnResumeButton()
    {
        GameManager.Instance.ResumeGame();
    }
    public void OnRestartButton()
    {
        GameManager.Instance.RestartGame();
    }
    public void OnMenuButton()
    {
        GameManager.Instance.MenuGame();
    }
    public void OnExitButton()
    {
        GameManager.Instance.ExitGame();
    }

    public void ChageMiniGame()
    {
        hudPanel.SetActive(false);
    }
    public void ChageBaseGame()
    {
        hudPanel.SetActive(true);
    }

    public void StartMenu()
    {
        mainMenuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        hudPanel.SetActive(false);
        pausePanel.SetActive(false);
    }
    public void Play()
    {
        mainMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        hudPanel.SetActive(true);
        pausePanel.SetActive(false);
    }
    public void Pause()
    {
        mainMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        hudPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
    public void Resume()
    {
        Play();
    }
    public void GameOver()
    {
        mainMenuPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        hudPanel.SetActive(false);
        pausePanel.SetActive(false);
    }
}