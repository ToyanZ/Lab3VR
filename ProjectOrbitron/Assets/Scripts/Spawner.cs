using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public int count;

    public void Spawn()
    {
        for(int i = 0; i < count; i++)
        {
            Instantiate(prefab, transform.position, Quaternion.identity, transform);
        }
    }
}
