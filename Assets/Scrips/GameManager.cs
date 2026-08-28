using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


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


    public void StarMiniGame()
    {
        baseGame.SetActive(false);
        miniGame.SetActive(true);
    }
    public void StarBaseGame()
    {
        baseGame.SetActive(true);
        miniGame.SetActive(false);
    }
}