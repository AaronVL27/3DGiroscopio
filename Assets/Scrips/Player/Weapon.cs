using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform bulletPoint;
    //[SerializeField] private ParticleSystem shotParticle;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Shoot()
    {
        GameObject bullet = PoolManager.Instance.ObtenerObjeto();
        bullet.transform.position = bulletPoint.position;
        bullet.transform.rotation = bulletPoint.rotation;
        //shotParticle.Play();
        bullet.SetActive(true);
    }

    public void GravityOff()
    {
        rb.isKinematic = false;
    }
    public void GravityOn()
    {
        rb.isKinematic = true;
    }
}