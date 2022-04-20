using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZombieSpawner : MonoBehaviour
{

    public int numberOfZombiesToSpawn;
    public GameObject[] zombiePrefabs;
    public SpawnerVolume[] spawnVolumes;
    public TextMeshProUGUI waveUIText;
    public TextMeshProUGUI zombiesRemainingText;
    public GameObject zombiesParent;
    public SceneChanger sceneManager; 

    public float timeBetweenWaves;
    private float timeSinceLastWave;
    private int zombiesRemaining;

    public int wave = 1;
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

  
    public void SpawnWave()
    {
        waveUIText.text = wave.ToString();

        for (int i = 0; i < numberOfZombiesToSpawn * (wave); i++)
        {
            GameObject zombieToSpawn = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];
            SpawnerVolume spawnVolume = spawnVolumes[Random.Range(0, spawnVolumes.Length)];
            if (!followGameObject) return;

            GameObject zombie = Instantiate(zombieToSpawn, spawnVolume.transform.position, spawnVolume.transform.rotation, zombiesParent.transform);

            zombie.GetComponent<ZombieComponent>().Initialize(followGameObject);
        }
    }


    private void FixedUpdate()
    {
        timeSinceLastWave += Time.deltaTime;
        zombiesRemaining = zombiesParent.transform.childCount;


        if (timeSinceLastWave > timeBetweenWaves && zombiesRemaining <= 0)
        {
            wave++;

            if (wave > maxWave)
            {
                wave = maxWave;
                // ***********YOU WIN************
                sceneManager.ChangeScene(4);
            }
            SpawnWave();
            timeSinceLastWave = 0;
        }


        zombiesRemainingText.text = zombiesRemaining.ToString();
    }
}
