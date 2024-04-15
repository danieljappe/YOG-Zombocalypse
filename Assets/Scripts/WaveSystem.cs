using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Text waveText;
    public Text remainingZombiesText;
    public Text alarmText;
    public Text waveCompleted;
    public Text waveBegin;
    public Text cannotStartWaveText;
    public Text winText;

    public KeyCode startWaveKey = KeyCode.E; 
    public Transform objectToInteractWith; 

    private Wave currentWave;
    private int currentWaveNumber;
    private float nextSpawnTime;
    private bool canSpawn = true;
    private int totalZombiesAlive = 0;
    private bool canStartNextWave = false;

    private void Start()
    {
        UpdateWaveText();
        UpdateRemainingZombiesText();
        alarmText.enabled = false;
        waveCompleted.enabled = false;
        waveBegin.enabled = false;
        cannotStartWaveText.enabled = false;
        winText.enabled = false;
        
    }

    private void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        UpdateRemainingZombiesText();

        int activeZombiesCount = CountActiveZombies();
        totalZombiesAlive = activeZombiesCount;

        if (canStartNextWave && Input.GetKeyDown(startWaveKey))
        {
            if (totalZombiesAlive > 0)
            {
                cannotStartWaveText.enabled = true; 
            }
            else
            {
                cannotStartWaveText.enabled = false; 
                StartNextWave();
            }
        }

        if (Input.GetKeyDown(startWaveKey) && totalZombiesAlive > 0)
        {
            cannotStartWaveText.enabled = true;

        }

        if (totalZombiesAlive == 0 && !canSpawn && currentWaveNumber + 1 != waves.Length)
        {
            canStartNextWave = true;
            alarmText.enabled = true;
            waveCompleted.enabled = true;
            cannotStartWaveText.enabled = false;


        }else{
            alarmText.enabled = false;
            waveCompleted.enabled = false;
        }

        if (currentWaveNumber == waves.Length - 1 && totalZombiesAlive == 0 && !canSpawn)
        {
            DisplayVictoryMessage();
        }
    }

    void DisplayVictoryMessage()
    {
        Debug.Log("Vous avez gagné !");
        winText.enabled = true;
    }

    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = currentWave.typeEnemies[Random.Range(0, currentWave.typeEnemies.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            currentWave.NumbEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            UpdateRemainingZombiesText();

            if (currentWave.NumbEnemies == 0)
            {
                canSpawn = false;
            }
        }
    }

    void UpdateWaveText()
    {
        if (waveText != null)
        {
            waveText.text = "Wave: " + (currentWaveNumber + 1).ToString();
        }
    }

    void UpdateRemainingZombiesText()
    {
        if(totalZombiesAlive == 0){

            remainingZombiesText.text = "Wave Clear";
        }

        else
        {
            remainingZombiesText.text = "Remaining Zombies: " + totalZombiesAlive.ToString();
        }
    }

    int CountActiveZombies()
    {
        int count = 0;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Zombie"))
        {
            if (enemy != null && enemy.activeInHierarchy)
            {
                count++;
            }
        }
        return count;
    }

    void StartNextWave()
    {
        currentWaveNumber++;
        canSpawn = true;
        UpdateWaveText();

        canStartNextWave = false;
        alarmText.enabled = false;
        waveCompleted.enabled = false;
    }
}
