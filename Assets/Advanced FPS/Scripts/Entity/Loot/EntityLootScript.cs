using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLootScript : MonoBehaviour
{
    public Transform dropLocation;
    public Bucket[] buckets;

    void Start()
    {
        float test = 0;
        foreach (Bucket bucket in buckets) {
            test += bucket.probability;
        }

        if (test > 1) {
            Debug.LogError("Total probability > 1");
        }
    }

    public void DropLoot()
    {
        float cumulative = 0;
        float random = Random.value;

        foreach (Bucket bucket in buckets) {
            cumulative += bucket.probability;
            if (random < cumulative) {
                int lootIndex = Random.Range(0, bucket.loots.Length);
                Instantiate(bucket.loots[lootIndex], dropLocation.position, Quaternion.identity);
                break;
            }
        }
    }
    [System.Serializable]
    public class Bucket
    {
        public float probability;
        public GameObject[] loots;
    }
    
}

