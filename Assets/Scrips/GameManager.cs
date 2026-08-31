using Unity.Cinemachine;
using System.Collections;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static event Action AddWeapon;
    [SerializeField] private CinemachineCamera playerCam;
    [SerializeField] private CinemachineCamera playerCarCam;

    [SerializeField] private float chageGameCar;
    private float indexChageGameCar = 0;
    [SerializeField] private float chageGameShot;
    private float indexChageGameShot = 0;
    public float ChageGameShot { get { return indexChageGameShot; } }

    [SerializeField] private GameObject baseGame;
    [SerializeField] private GameObject miniGame;

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

    IEnumerator ChageMiniGame()
    {
        playerCam.Priority = 5;
        playerCarCam.Priority = 10;
        StarMiniGame();
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1;
    }

    public void StarMiniGame()
    {
        baseGame.SetActive(false);
        miniGame.SetActive(true);
    }


    IEnumerator ChageBasegame()
    {
        StarBaseGame();
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1;

    }
    public void StarBaseGame()
    {
        indexChageGameShot = 0;
        playerCam.Priority = 10;
        playerCarCam.Priority = 5;
        baseGame.SetActive(true);
        miniGame.SetActive(false);
    }
}