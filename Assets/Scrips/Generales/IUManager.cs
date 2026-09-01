using TMPro;
using UnityEngine;

public class IUManager : MonoBehaviour
{
    public static IUManager Instance;

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

    [SerializeField] private TextMeshProUGUI scoreCar;
    private float score = 0;
    private void Start()
    {
        scoreCar.text = "Puntaje: " + score.ToString();
    }

    public void AddScore()
    {
        score++;
        scoreCar.text = "Puntaje: " + GameManager.Instance.ChageGameShot.ToString();
    }
}
