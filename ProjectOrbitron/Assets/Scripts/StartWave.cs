using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWave : MonoBehaviour
{
    public WavesManager wavesManager;

    public bool starWave;

    private void OnTriggerEnter(Collider other)
    {
        Player player;

        if (other.TryGetComponent<Player>(out player) && !starWave)
        {
            starWave = true;
            wavesManager.StartWaves();
        }
    }


}
