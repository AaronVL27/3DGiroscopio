using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IUManager : MonoBehaviour
{
    public static IUManager Instance;

    [SerializeField] private TextMeshProUGUI scoreCar;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject previousPanel;


    [SerializeField] private Toggle toggleTwoJoysticks;
    [SerializeField] private Toggle toggleOneJoystick;
    [SerializeField] private GameObject hudTwoJoistick;
    [SerializeField] private GameObject hudOneJoistick;
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
        GameManager.OptionsEvent += Options;
        SettingsManager.OnModoControlChanged += ActualizarControlesUI;
    }


    private void OnDisable()
    {
        GameManager.PauseEvent -= Pause;
        GameManager.ResumeEvent -= Resume;
        GameManager.GameOverEvent -= GameOver;
        GameManager.MainMenuEvent -= StartMenu;
        GameManager.PlayGameEvent -= Play;
        GameManager.OptionsEvent -= Options;
        SettingsManager.OnModoControlChanged -= ActualizarControlesUI;
    }

    private void Start()
    {
        ModoControl modoInicial = SettingsManager.CargarModoGuardado();

        toggleTwoJoysticks.SetIsOnWithoutNotify(modoInicial == ModoControl.DosJoysticks);
        toggleOneJoystick.SetIsOnWithoutNotify(modoInicial == ModoControl.UnJoystick);

        SettingsManager.CambiarModo(modoInicial); // avisa a quien esté escuchando (Player, joysticks UI)

        scoreCar.text = "Puntaje: " + GameManager.Instance.ChageGameShotObjetive + "/" + GameManager.Instance.ChageGameShot.ToString();
    }
    public void OnToggleDosJoysticks(bool activado)
    {
        Debug.Log("Toggle DosJoysticks: " + activado);
        SfxButton();
        if (activado) SettingsManager.CambiarModo(ModoControl.DosJoysticks);
    }

    public void OnToggleUnJoystick(bool activado)
    {
        Debug.Log("Toggle UnJoystick: " + activado);
        SfxButton();
        if (activado) SettingsManager.CambiarModo(ModoControl.UnJoystick);
    }

    private void ActualizarControlesUI(ModoControl modo)
    {
        hudTwoJoistick.SetActive(false);
        hudOneJoistick.SetActive(false);
        hudTwoJoistick.SetActive(modo == ModoControl.DosJoysticks);
        hudOneJoistick.SetActive(modo == ModoControl.UnJoystick);
    }
    public void AddScore()
    {
        score++;
        scoreCar.text = "Puntaje: " + GameManager.Instance.ChageGameShotObjetive + "/" + GameManager.Instance.ChageGameShot.ToString();
    }

    public void OnPlayButton()
    {
        GameManager.Instance.StartGame();
        SfxButton();
    }
    public void OnPauseButton()
    {
        GameManager.Instance.PauseGame();
        SfxButton();
    }
    public void OnResumeButton()
    {
        GameManager.Instance.ResumeGame();
        SfxButton();
    }
    public void OnRestartButton()
    {
        GameManager.Instance.RestartGame();
        SfxButton();
    }
    public void OnMenuButton()
    {
        GameManager.Instance.MenuGame();
        SfxButton();
    }
    public void OnSettingButton()
    {
        GameManager.Instance.OptionsGame();
        SfxButton();
    }
    public void OnExitButton()
    {
        SfxButton();
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

    public void OpenOptions(GameObject currentPanel)
    {
        previousPanel = currentPanel;

        currentPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    public void BackFronPreviusPanel()
    {
        settingsPanel.SetActive(false);
        previousPanel.SetActive(true);
    }

    public void StartMenu()
    {
        mainMenuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        hudPanel.SetActive(false);
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }
    public void Play()
    {
        mainMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        hudPanel.SetActive(true);
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }
    public void Pause()
    {
        mainMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        hudPanel.SetActive(false);
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);
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
        settingsPanel.SetActive(false);
    }
    public void Options()
    {
        mainMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        hudPanel.SetActive(false);
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    private void SfxButton()
    {
        AudioManager.Instance.PlaySFX("Button");
    }
}