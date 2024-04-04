using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Wave
{

    public string waveName;
    public int NumbEnemies;
    public GameObject[] typeEnemies;
    public float spawnInterval; 
}

public class WaveSystem : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;

    private Wave currentWave;
    private int currentWaveNumber;
    private float nextSpawnTime;

    private bool canSpawn = true;

    private void Update()
    {

        currentWave = waves[currentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Zombie");
        if(totalEnemies.Length == 0 && !canSpawn && currentWaveNumber+1 != waves.Length)
        {
            currentWaveNumber++;
            canSpawn = true;

        }

    }

    void SpawnWave()
    {
        if(canSpawn && nextSpawnTime < Time.time){
        GameObject randomEnemy = currentWave.typeEnemies[Random.Range(0, currentWave.typeEnemies.Length)];
        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
        currentWave.NumbEnemies--;
        nextSpawnTime = Time.time + currentWave.spawnInterval;

        if(currentWave.NumbEnemies == 0){
            canSpawn = false;
        }
        }


    }

}
