using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class WaveController : InstantiateEnemies
{
    [Space(20)]
    public int remainingEnemies, initialEnemies, initialWave, maxWave;
    public float timeBetweenWaves, currentTime, timeBetweenEnableEnemies;

    public bool tutorial, startWave, inWave, enableWave, createWave;

    public TMP_Text remainingTimeTXT;

    public GameObject lightText, limitZone;
    
    public Animator[] animDoors;

    public List<GameObject> enemiesList = new List<GameObject>();

    private void Awake()
    {
        initialEnemies = initialEnemies == 0 ? 1 : initialEnemies;
        if(lightText != null)
            lightText.SetActive(true);
        if(limitZone != null)
            limitZone.SetActive(false);
        if(animDoors != null)
        {
            for(int i = 0; i <animDoors.Length; i++)
            {
                animDoors[i].SetBool("Open", false);
            }
        }
    }
    private void FixedUpdate()
    {
        /*if (!startWave && !inWave && initialWave <= maxWave)
        {
            Debug.Log("1");
            LoadWave();
            return;
        }
        if(initialWave <= maxWave)
        {
            if(remainingEnemies <= 0 && inWave && startWave)
            {
                remainingTimeTXT.text = "";
                inWave = false;
            }
            if(remainingEnemies <= 0 && inWave && startWave && currentTime >= 0)
            {
                Debug.Log("2");
                currentTime = currentTime - Time.deltaTime;
                remainingTimeTXT.text = currentTime.ToString("0.0");
                return;
            }
            if(currentTime <= 0 && inWave && startWave)
            {
                Debug.Log("3");
                LoadWave();
            }
        }*/

        if (enableWave)
        {
            enableWave = true;
            LoadWave();
        }

        if (remainingEnemies <= 0 && initialWave <= maxWave)
        {
            currentTime = currentTime - Time.deltaTime;
            remainingTimeTXT.text = currentTime.ToString("0.0");
            if(currentTime <= 0 && createWave)
            {
                LoadWave();
                createWave = false;
            }
        }

    }

    public void LoadWave()
    {
        ResetValues();
        initialEnemies++;
        initialWave++;
        for (int i = 0; i < initialEnemies; i++)
        {
            Debug.Log("Crea un enemigo invisible");
            GameObject enemy = SpawnEnemiesInRandomPositions();
            enemy.GetComponent<IA_Enemies>().waveController = this;
            enemiesList.Add(enemy);
            remainingEnemies++;
        }
        StartCoroutine("AwakeEnemies");
    }

    IEnumerator AwakeEnemies()
    {
        for(int i = 0; i< enemiesList.Count; i++)
        {
            print("Llego");
            yield return new WaitForSeconds(timeBetweenEnableEnemies);
            if (enemiesList[i] != null) 
            { 
                if (!enemiesList[i].activeSelf)
                {
                    enemiesList[i].SetActive(true);
                    StartCoroutine("AwakeEnemies");
                }
            }
        }
        createWave = true;
    }

    private void ResetValues()
    {
        inWave = true;
        currentTime = timeBetweenWaves;
        remainingEnemies = 0;
        startWave = true;
    }
}
