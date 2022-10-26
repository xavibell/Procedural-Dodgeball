using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int spawnRate;
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private int maxEnemies;
    [SerializeField] private int spawnerCount;
    public List<Transform> spawnPositions;
    
    private int counter = 1;

    private GameObject enemySpawnerObject;

    void Start()
    {

        enemySpawnerObject = new GameObject("Enemy Spawner");
        for (int i = 0; i < spawnerCount; i++)
        {
            Instantiate(enemySpawnerObject, this.transform);
        }
        foreach (Transform child in this.transform)
        {
            spawnPositions.Add(child);
            child.position = new Vector3(Random.Range(-38f, 38f), 0, Random.Range(-100f, 100));
        }
        
        StartCoroutine(findSpawnPosition());
    }

    // Update is called once per frame
    void Update()
    {
        
        counter++;
        if (spawnRate != 0)
        {
            if (counter % spawnRate == 0 && GameObject.FindGameObjectsWithTag("Enemy").Length <= maxEnemies)
            {
                Instantiate(spawnObject, spawnPositions[Random.Range(0, spawnPositions.Count)].transform.position, Quaternion.Euler(0, 0, 0));
            }
        }
        
        if (counter > 1000)
        {
            counter = 1;
        }
    }

    IEnumerator findSpawnPosition()
    {
        int layerMask = ~LayerMask.GetMask("Ground");
        foreach (Transform spawnPos in spawnPositions)
        {
            while (Physics.OverlapSphere(spawnPos.position, 0.5f, layerMask).Length > 0)
            {
                spawnPos.position = new Vector3(Random.Range(-38f, 38f), 0, Random.Range(-100f, 100));
            }
        }

        yield return null;
    }

}




