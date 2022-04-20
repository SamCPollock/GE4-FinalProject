using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public GameObject player;
    public GameObject enemySpawner; 

    void Start()
    {
        
    }

    void Update()
    {
      
    }

    public void SaveGame()
    {
        // Save player pos
        PlayerPrefs.SetFloat("PlayerXPos", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerZPos", player.transform.position.z);

        // Save player health
        PlayerPrefs.SetFloat("PlayerHealth", player.gameObject.GetComponent<PlayerHealthComponent>().CurrentHealth);

        // Save zombie wave
        PlayerPrefs.SetInt("ZombieWave", enemySpawner.gameObject.GetComponent<ZombieSpawner>().wave);

    }

    public void LoadGame()
    {
        // Load player pos 
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerXPos"), 12, PlayerPrefs.GetFloat("PlayerZPos"));

        // Load player health
        player.gameObject.GetComponent<PlayerHealthComponent>().currentHealth = PlayerPrefs.GetFloat("PlayerHealth");
        // Load zombie wave

        enemySpawner.gameObject.GetComponent<ZombieSpawner>().wave = PlayerPrefs.GetInt("ZombieWave");
        enemySpawner.gameObject.GetComponent<ZombieSpawner>().SpawnWave();
    }
}
