using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{

    public int numberOfZombiesToSpawn;
    public GameObject[] zombiePrefabs;
    public SpawnerVolume[] spawnVolumes;

    public float timeBetweenWaves;
    private float timeSinceLastWave; 

    private int wave = 1;
    public int maxWave = 10;

    GameObject followGameObject; 

    void Start()
    {
        followGameObject = GameObject.FindGameObjectWithTag("Player");
        //for (int i = 0; i < numberOfZombiesToSpawn; i++)
        //{
        //    SpawnWave();
        //}
        SpawnWave();
    }

  
    void SpawnWave()
    {
        for (int i = 0; i < numberOfZombiesToSpawn * (wave / 3); i++)
        {
            GameObject zombieToSpawn = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];
            SpawnerVolume spawnVolume = spawnVolumes[Random.Range(0, spawnVolumes.Length)];
            if (!followGameObject) return;

            GameObject zombie = Instantiate(zombieToSpawn, spawnVolume.transform.position, spawnVolume.transform.rotation);

            zombie.GetComponent<ZombieComponent>().Initialize(followGameObject);
        }
    }


    private void FixedUpdate()
    {
        timeSinceLastWave += Time.deltaTime;

        if (timeSinceLastWave > timeBetweenWaves)
        {
            wave++;

            if (wave > maxWave)
            {
                wave = maxWave;
            }
            SpawnWave();
            timeSinceLastWave = 0;
        }
    }
}
