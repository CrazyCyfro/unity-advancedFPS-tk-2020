using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{
    public int limit;
    public int limitIncrement;
    public float spawnInterval;
    public float waveInterval;
    public GameObject enemyPrefab;
    public Transform[] spawners;
    private int count;
    private float spawnChance;
    
    void Start()
    {
        spawnChance = 1.0f/spawners.Length;

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true) {
            while (count < limit) {
                foreach (Transform spawner in spawners) {
                    if (Random.value < spawnChance) {
                        Instantiate(enemyPrefab, spawner.position, Quaternion.identity);
                        count++;
                        yield return new WaitForSeconds(spawnInterval);
                    } else {
                        yield return null;
                    }
                    
                }
            }
            
            limit += limitIncrement;
            count = 0;
            yield return new WaitForSeconds(waveInterval);
        }
    }
}
