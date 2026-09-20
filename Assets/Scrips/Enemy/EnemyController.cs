using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ObjectPool objectPool;

    public float MaxHealth;
    private float speed = 0;
    public float currHealth;
    private bool luckOfLife = false;
    Rigidbody2D rb2D;
    private Transform player;
    private PlayerHealth playerHealth;
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        currHealth = MaxHealth;
    }
    void OnEnable()
    {
        GameObject jugador = GameObject.FindWithTag("Player");
        playerHealth = jugador.GetComponent<PlayerHealth>();

        if (jugador != null)
        {
            player = jugador.transform;
        }
        else
        {
            player = null;
        }

        float probabilidad = Random.value;

        switch (probabilidad)
        {
            case < 0.60f:
                transform.localScale = new Vector2(1f, 1f);
                MaxHealth = 3;
                speed = 8f;
                break;

            case < 0.85f:
                transform.localScale = new Vector2(0.5f, 0.5f);
                MaxHealth = 1;
                speed = 10f;
                break;

            case < 0.95f:
                transform.localScale = new Vector2(2f, 2f);
                MaxHealth = 6;
                speed = 6f;
                break;

            default:
                transform.localScale = new Vector2(3f, 3f);
                MaxHealth = 9;
                speed = 3f;
                luckOfLife=true;
                break;
        }
        currHealth = MaxHealth;
    }
    void FixedUpdate()
    {
        // Solo se mueve si encontró al jugador con éxito
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb2D.linearVelocity = direction * speed;
        }
        else
        {
            // Si el jugador no existe (por ejemplo, si el jugador también murió), se detiene
            rb2D.linearVelocity = Vector2.zero;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            currHealth--;
            if (currHealth <= 0 && luckOfLife)
            {
                playerHealth.AddHealth();
                Die();
                GameManager.Instance.AddpointCarGame();
                luckOfLife = false;
            }
            else if(currHealth <= 0 && !luckOfLife)
            {
                Die();
                GameManager.Instance.AddpointCarGame();
            }
        }
    }
    void Die()
    {
        AudioManager.Instance.PlaySFX("Hurt_EnemyP");
        currHealth = MaxHealth;
        objectPool.ReturnToPool(gameObject);
    }
}
