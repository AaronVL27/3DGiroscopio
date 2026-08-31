using UnityEngine;

public class CarEnemy : MonoBehaviour
{
    public ObjectPool2 objectPool;

    private float speed;
    private float speedRandom;
    Rigidbody2D rb2D;
    private Transform player;
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    void OnEnable()
    {
        speedRandom = Random.Range(20f, 60f);
    }
    void FixedUpdate()
    {
        rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, -speedRandom);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            GameManager.Instance.StarBaseGame();
        else
            Die();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.AddpointShotGame();
        }
    }
    void Die()
    {
        objectPool.ReturnToPool(gameObject);
    }
}
