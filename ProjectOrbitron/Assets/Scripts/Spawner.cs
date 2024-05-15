using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public List<Transform> spawnPoints;
    public int count;
    public float spawnRate = 0.3f;
    public bool autoSpawn = false;
    private List<GameObject> list;
    private List<GameObject> spawned;
    int limit = 10;

    private void Start()
    {
        if(autoSpawn) Refill();
        spawned = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        if (!autoSpawn) return;
        if (list.Count <= 0 || IsEmpty()) Refill();
    }

    private bool IsEmpty()
    {
        bool empty = true;
        foreach (GameObject go in list)
        {
            if (go != null) empty = false; 
        }
        return empty;
    }

    private void Refill()
    {
        StartCoroutine(RefillIE());
    }
    IEnumerator RefillIE()
    {
        list = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            GameObject clone = Instantiate(prefab, GetPosition(), Quaternion.identity, transform);
            list.Add(clone);
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private Vector3 GetPosition()
    {
        if (spawnPoints == null) return transform.position;
        if (spawnPoints.Count == 0) return transform.position;
        return spawnPoints[Random.Range(0, spawnPoints.Count)].position;
    }


    public void Spawn()
    {
        StartCoroutine(SpawnIE());
    }
    IEnumerator SpawnIE()
    {
        for(int i =  0; i < spawned.Count; i++)
        {
            Destroy(spawned[i]);
        }
        spawned = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            GameObject clone = Instantiate(prefab, GetPosition(), Quaternion.identity, transform);
            spawned.Add(clone);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
