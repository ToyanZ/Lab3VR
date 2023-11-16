using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWave : MonoBehaviour
{
    public WavesManager wavesManager;

    public bool starWave;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !starWave)
        {
            starWave = true;
            wavesManager.StartWaves();
        }
    }


}
