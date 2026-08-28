using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
    [SerializeField] private GameObject prefab;
    [SerializeField] private int cantidadInicial = 10;
    private Queue<GameObject> pool = new Queue<GameObject>();


    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        for (int i = 0; i < cantidadInicial; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject ObtenerObjeto()
    {
        GameObject obj = pool.Count > 0 ? pool.Dequeue() : Instantiate(prefab);
        return obj;
    }

    public void DevolverObjeto(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }

}
