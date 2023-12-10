using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateEnemies : MonoBehaviour
{
    public Transform playerPosition;
    public Transform[] spawnPositions;
    public int remainingEnmiesSpawn;
    [Space]
    public GameObject[] enemies; 

    public GameObject SpawnEnemiesInRandomPositions()
    {
        GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Length)],
                spawnPositions[Random.Range(0, spawnPositions.Length)].position,
                Quaternion.identity);
        IA_Enemies ia_enemy = enemy.GetComponent<IA_Enemies>();
        enemy.transform.parent = gameObject.transform;
        ia_enemy.target = playerPosition;
        enemy.SetActive(false);
        return enemy;
    }
}
