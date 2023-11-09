using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WavesManager : SpawnEnemies
{
    [Space(20)]
    public static WavesManager instance;
    public int remainingEnemies, initialEnemies = 3;
    public float timeBetweenWaves;
    public float currentTime;

    public bool tutorial, inWave;

    public TMP_Text remainingTimeTEXT;

    public GameObject light;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        remainingEnemies = initialEnemies;
        StartSpawnEnemies();
        remainingTimeTEXT.text = "";
    }

    public void Update()
    {
        if(remainingEnemies <= 0 && inWave)
        {
            remainingTimeTEXT.text = "";
            inWave = false;
            StartCoroutine("LoadNewWave");
            light.SetActive(true);
        }
        else if(!inWave)
        {
            currentTime = currentTime - Time.deltaTime;
            remainingTimeTEXT.text = "" + currentTime.ToString("0.0");
        }else if(inWave) 
            light.SetActive(false);
    }

    IEnumerator LoadNewWave()
    {
        currentTime = timeBetweenWaves;
        yield return new WaitForSeconds(timeBetweenWaves);
        initialEnemies++;
        inWave = true;
        remainingEnemies = initialEnemies;
        SpawnEnemiesInRandomPositions(initialEnemies);
    }

    public void StartSpawnEnemies()
    {
        SpawnEnemiesInRandomPositions(initialEnemies);
    }
}
