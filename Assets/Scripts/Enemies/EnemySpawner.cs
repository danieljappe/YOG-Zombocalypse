using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private bool canSpawn = true;

    private void Start(){
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner(){
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while(canSpawn){
            yield return wait;
            int random = Random.Range(0,enemyPrefab.Length);
            GameObject enemyToSpawn = enemyPrefab[random];

            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        }
    }
}
