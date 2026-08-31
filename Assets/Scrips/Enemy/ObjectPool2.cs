using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ObjectPool2 : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private Transform containerSpawn;
    [SerializeField] private int poolSize;
    //[SerializeField] private float cooldownTime;

    private Queue<GameObject> poolObject = new Queue<GameObject>();
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Awake()
    {
        // Creamos todos los enemigos de una vez, desactivados, y los metemos en la cola
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(prefab, containerSpawn);
            enemy.SetActive(false);

            // Le pasamos la referencia al pool apenas nace
            CarEnemy enemyScript = enemy.GetComponent<CarEnemy>();
            enemyScript.objectPool = this;

            poolObject.Enqueue(enemy);
        }
    }

    void Start()
    {
        Spawn();
    }


    private void OnEnable()
    {
        // Si hay enemigos activos en la escena, los movemos a un nuevo punto aleatorio
        foreach (GameObject enemy in activeEnemies)
        {
            if (enemy != null && enemy.activeSelf)
            {
                // Elegimos un punto nuevo al azar
                int indiceRandom = Random.Range(0, spawnPoints.Length);
                Transform spawnPoint = spawnPoints[indiceRandom].transform;

                // Teletransportamos al enemigo a su nueva posición
                enemy.transform.position = spawnPoint.position;
                enemy.transform.rotation = spawnPoint.rotation;
            }
        }
    }

    void Spawn()
    {
        // Si ya no quedan enemigos disponibles en el pool, no hacemos nada
        // (esto cubre el caso "todos están activos")
        if (poolObject.Count == 0)
            return;

        GameObject enemy = poolObject.Dequeue();

        int indiceRandom = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[indiceRandom].transform;

        enemy.transform.position = spawnPoint.position;
        enemy.transform.rotation = spawnPoint.rotation;
        enemy.SetActive(true);

        activeEnemies.Add(enemy);

        // Si todavía queda espacio disponible en el pool, seguimos spawneando
        // (quita este bloque si quieres spawnear de a uno por vez)
        //if (poolObject.Count > 0)
        //{
        //    Spawn();
        //}
    }

    // Esto lo va a llamar el Enemy cuando muera
    public void ReturnToPool(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        enemy.SetActive(false);
        poolObject.Enqueue(enemy);
        Spawn();
        //StartCoroutine(CooldownAndSpawn());
    }

    //private IEnumerator CooldownAndSpawn()
    //{
    //    yield return new WaitForSeconds(cooldownTime);
    //    Spawn();
    //}
}
