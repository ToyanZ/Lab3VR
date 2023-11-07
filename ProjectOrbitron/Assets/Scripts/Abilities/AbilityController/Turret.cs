using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Weapon turretPrefab;
    public Weapon currentTurret;
    public Transform spawnPoint;
    private Target sender;
    public void SaveSenderTarget(Trigger trigger)
    {
        List<Target> targets = trigger.GetTargets();
        foreach (Target target in targets)
        {
            if(target.gameObject.GetComponent<Character>() != null)
            {
                sender = target;
                break;
            }
        }
    }
    public void BuildTurret()
    {
        currentTurret = Instantiate(turretPrefab, spawnPoint.position, Quaternion.identity);
        currentTurret.sender = sender;
    }

    public void Shoot(Trigger trigger)
    {
        if (currentTurret != null) 
        {
            Target enemy = null;
            List<Target> targets = trigger.GetTargets();    
            foreach(Target target in targets)
            {
                //if(target != sender)
                //{
                //    enemy = target;
                //    break;
                //}
                enemy = target;
            }
            if(enemy != null)
            {
                Vector2 direction = trigger.GetTargets()[0].transform.position - spawnPoint.position;
                currentTurret.Shoot(direction);
            }
            
        }
    }


}
