using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public Transform playerPosition;

    public Transform[] spawnPositions;
    public int remainingEnemiesSpawn;
    public GameObject[] enemies;

    public void SpawnEnemiesInRandomPositions(int value)
    {
        remainingEnemiesSpawn = value;
        StartCoroutine("SpawnNewEnemy");
    }

    IEnumerator SpawnNewEnemy() 
    {
        GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPositions[Random.Range(0, spawnPositions.Length)].position, Quaternion.identity);
        enemy.GetComponent<IA_Enemies>().target = playerPosition;
        yield return new WaitForSeconds(0.5f);
        if(remainingEnemiesSpawn > 0) StartCoroutine("SpawnNewEnemy");
        remainingEnemiesSpawn--;
    }
}
