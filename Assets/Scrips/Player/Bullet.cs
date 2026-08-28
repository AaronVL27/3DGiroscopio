using UnityEngine;
using System.Collections;
public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float speedBullet = 10;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PoolManager.Instance.DevolverObjeto(gameObject);
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector3.zero;
        rb.linearVelocity = -transform.up * speedBullet;
        StartCoroutine(BulletCoodown());
    }
    IEnumerator BulletCoodown()
    {
        yield return new WaitForSeconds(1);
        PoolManager.Instance.DevolverObjeto(gameObject);
    }
}
