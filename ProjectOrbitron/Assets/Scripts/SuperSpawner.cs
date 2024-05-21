using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class SuperSpawner : MonoBehaviour
{
    public List<IA_Enemies> enemies;
    public Transform spawnPoint;
    public Transform target;

    [Space(20)]
    public int minEnemyPerCycle = 3;
    public int maxEnemyPerCycle = 5;
    public float cycleRate = 13f;

    private bool start = false;
    [SerializeField]
    private List<IA_Enemies> clones;
    private int enemySpawnAmount;
    [SerializeField]
    private int enemiesDead;
    private void Start()
    {
        clones = new List<IA_Enemies>();
    }

    public void StartCycle()
    {
        if(!start) { StartCoroutine(Cycle()); }
    }
    private IEnumerator Cycle()
    {
        start = true;

        while(true)
        {
            enemySpawnAmount = Random.Range(minEnemyPerCycle, maxEnemyPerCycle);
            SpawnEnemies(enemySpawnAmount);
            yield return new WaitForSeconds(cycleRate);
            
            while (true)
            {
                if(CheckingEnemies()) yield return new WaitForSeconds(cycleRate);
                else break;
            }
        }
    }

    private void SpawnEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int randomIndex = Random.Range(0, enemies.Count);
            IA_Enemies enemy = Instantiate(enemies[randomIndex], spawnPoint.position, Quaternion.identity);
            enemy.target = target;
            clones.Add(enemy);
        }
    }
    private bool CheckingEnemies()
    {
        enemiesDead = 0;
        foreach (IA_Enemies enemy in clones)
        {
            if (enemy == null) enemiesDead += 1;
        }

        if (enemiesDead == 0) return true;

        if( enemiesDead < enemySpawnAmount)
        {
            clones = clones.FindAll(e => e != null);
            SpawnEnemies(enemiesDead);
            return true;
        }
        clones.Clear();
        return false;
    }
}
