using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static event Action AddWeapon;

    public static event Action PauseEvent;
    public static event Action ResumeEvent;
    public static event Action GameOverEvent;
    public static event Action MainMenuEvent;
    public static event Action PlayGameEvent;

    [SerializeField] private CinemachineCamera playerCam;
    [SerializeField] private CinemachineCamera playerCarCam;

    [SerializeField] private float chageGameCar;
    private float indexChageGameCar = 0;
    [SerializeField] private float chageGameShot;
    private float indexChageGameShot = 0;
    public float ChageGameShot { get { return indexChageGameShot; } }
    //public float ChageGameShot => indexChageGameShot; otra manera

    [SerializeField] private GameObject baseGame;
    [SerializeField] private GameObject miniGame;
    private static bool startInGame = false;
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

    private void Start()
    {
        if (startInGame)
        {
            startInGame = false;
            StartGame();
        }
        else
        {
            Time.timeScale = 0;
            MainMenuEvent?.Invoke();
        }
    }
    public void AddpointCarGame()
    {
        indexChageGameCar++;
        if (indexChageGameCar >= chageGameCar)
        {
            Time.timeScale = 0;
            StartCoroutine(ChageMiniGame());
            chageGameCar += 5;
            indexChageGameCar = 0;
        }
    }
    public void AddpointShotGame()
    {
        indexChageGameShot++;
        IUManager.Instance.AddScore();
        if (indexChageGameShot >= chageGameShot)
        {
            Time.timeScale = 0;
            StartCoroutine(ChageBasegame());
            AddWeapon?.Invoke();
            chageGameShot += 5;
            indexChageGameShot = 0;
        }
    }

    /// //////////////////////////////////////
    public void StarMiniGame()
    {
        baseGame.SetActive(false);
        miniGame.SetActive(true);
    }
    public void StarBaseGame()
    {
        playerCam.Priority = 10;
        playerCarCam.Priority = 5;
        indexChageGameShot = 0;
        baseGame.SetActive(true);
        miniGame.SetActive(false);
    }
    IEnumerator ChageMiniGame()
    {
        playerCam.Priority = 5;
        playerCarCam.Priority = 10;
        StarMiniGame();
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1;
    }


    IEnumerator ChageBasegame()
    {
        StarBaseGame();
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;

    }
    public void LoseMiniGame()
    {
        Time.timeScale = 0;
        StartCoroutine(ChageBasegame());
    }

    //////////////////////////////////////////////////

    public void PauseGame()
    {
        Time.timeScale = 0;

        PauseEvent?.Invoke();
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;

        ResumeEvent?.Invoke();
    }
    public void LoseGame()
    {
        Time.timeScale = 0;

        GameOverEvent?.Invoke();
    }

    public void RestartGame()
    {
        startInGame = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MenuGame()
    {
        startInGame = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartGame()
    {
        Time.timeScale = 1;

        PlayGameEvent?.Invoke();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
