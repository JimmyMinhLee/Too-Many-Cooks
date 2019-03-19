using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour
{
    public GameObject zombie;
    public Transform[] spawnPositions;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnZombies());
    }


    IEnumerator SpawnZombies()
    {
        int i = 0;
        while (i < spawnPositions.Length)
        {
            Instantiate(zombie, spawnPositions[i]);
            i += 1;
            yield return new WaitForSeconds(4f);
        }
    }
}
