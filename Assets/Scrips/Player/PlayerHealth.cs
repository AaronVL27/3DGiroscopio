using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] GameObject[] lives;
    [SerializeField] float coolDown;
    public float timer;
    private bool stillCollision;

    public int currHealth;

    private void Start()
    {
        currHealth = maxHealth;
        timer = coolDown;
    }
    private void Update()
    {
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            LoseHealth();
            stillCollision = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && stillCollision)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                LoseHealth();
                timer = coolDown;
            }
        }   
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            stillCollision = false;
            timer = coolDown;
        }
    }

    void LoseHealth()
    {
        if (currHealth <= 0) return; // si ya no tiene vida, no hace nada más

        currHealth--;
        lives[currHealth].SetActive(false);
        AudioManager.Instance.PlaySFX("Hurt_Player");
        if (currHealth <= 0)
        {
            GameManager.Instance.LoseGame();
            //currHealth = maxHealth;
        }
    }
    public void AddHealth()
    {
        if (currHealth >= maxHealth) return;

        lives[currHealth].SetActive(true);
        currHealth++;
        AudioManager.Instance.PlaySFX("Hurt_Player");
        if (currHealth >= maxHealth)
        {
            currHealth = maxHealth;
        }
    }
}
