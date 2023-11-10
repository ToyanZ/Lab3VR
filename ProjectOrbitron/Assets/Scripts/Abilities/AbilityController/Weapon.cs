using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Weapon : AbilityController
{
    enum WeaponStat { Ammo, FireRate, Reload, Spread}
    [Space(20)]
    public Transform tip;
    public Transform center;
    [SerializeField] private Vector3 aimDirection = Vector3.zero;
    public float aimAngle = 0;
    
    [Space(20)]
    public float shootForce = 100;
    public List<Stat> stats = new List<Stat>
    {
        new Stat(Stat.Type.Number, WeaponStat.Ammo.ToString(), 40, 40, true, "ammo"),
        new Stat(Stat.Type.Number, WeaponStat.FireRate.ToString(), 0, 0.02f, true, "fireRate"),
        new Stat(Stat.Type.Number, WeaponStat.Reload.ToString(), 0, 1.5f, true, "reload"),
        new Stat(Stat.Type.Number, WeaponStat.Spread.ToString(), 0, 0, false, "spread"),
        new Stat(Stat.Type.Number, "AbilityIndex", 0, 0, false, "abilityIndex")
    };
    public float recoil = 0;

    [Space(20)]
    public List<OnDataUpdated> onDataUpdatedEvents = new List<OnDataUpdated>
    {
        new OnDataUpdated(WeaponStat.Ammo.ToString(), 0, new UnityEvent<InterfaceData>()),
        new OnDataUpdated("AbilityChanged", 4, new UnityEvent<InterfaceData>())
    };

    private int lastIndex = 0;
    private void Start()
    {
        Stat ammo = GetStat(WeaponStat.Ammo);
        ammo.current = ammo.max;
        Stat reloading = GetStat(WeaponStat.Reload);
        reloading.current = 0;
        reloading.boolValue = false;
        Stat fireRate = GetStat(WeaponStat.FireRate);
        fireRate.current = 0;
        onDataUpdatedEventsPrivate = onDataUpdatedEvents;

                        //NO OLVIDAR!!

        //stats[4].max = abilities.Count - 1;
        lastIndex = currentAbilityIndex;
    }

    private void Update()
    {
        if(lastIndex != currentAbilityIndex)
        {
            lastIndex = currentAbilityIndex;
            DataUpdate(this, 1);
        }

        aimDirection = (tip.position - center.position).normalized;

    }


    public Vector3 GetAimDirection()
    {
        return aimDirection;
    }

    public void Shoot(Vector2 direction)
    {
        RealoadUpdate();
        FireRateUpdate();
        if (CanShoot()) LaunchProjectile(direction);
    }

    public void Shoot()
    {
        Vector3 direction = tip.position - center.position;
        //RealoadUpdate();
        //FireRateUpdate();
        //if (CanShoot()) LaunchProjectile(direction);
        LaunchProjectile(direction);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(center.position, tip.position);
    }

    void LaunchProjectile(Vector3 direction)
    {
        Ability clone = Instantiate(abilities[currentAbilityIndex], tip.position, Quaternion.identity);
        clone.sender = sender;
        clone.rigidBody.AddForce(direction.normalized * shootForce, ForceMode.Impulse);
        GetStat(WeaponStat.Ammo).current -= 1;
        DataUpdate(this, 0);
    }
    bool CanShoot()
    {
        Stat realoding = GetStat(WeaponStat.Reload);
        Stat fireRate = GetStat(WeaponStat.FireRate);
        return !realoding.boolValue && fireRate.boolValue;
    }
    void RealoadUpdate()
    {
        Stat ammo = GetStat(WeaponStat.Ammo);
        Stat reloading = GetStat(WeaponStat.Reload);
        
        if (ammo.current == 0 && !reloading.boolValue)
        {
            reloading.boolValue = true;
            StartCoroutine(Reload());
        }
    }
    IEnumerator Reload()
    {
        Stat reloading = GetStat(WeaponStat.Reload);
        Stat ammo = GetStat(WeaponStat.Ammo);
        while (reloading.current < reloading.max)
        {
            reloading.current += Time.deltaTime;
            ammo.current = reloading.current / reloading.max * ammo.max;
            DataUpdate(this, 0);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        ammo.current = ammo.max;
        reloading.current = 0;
        reloading.boolValue = false;
    }


    void FireRateUpdate()
    {
        Stat fireRate = GetStat(WeaponStat.FireRate);
        fireRate.current += Time.deltaTime;
        fireRate.boolValue = false;
        
        if(fireRate.current >= fireRate.max)
        {
            fireRate.current = 0;
            fireRate.boolValue = true;
        }
    }

    Stat GetStat(WeaponStat weaponStat)
    {
        return stats.Find(x => x.displayName == weaponStat.ToString());
    }

    public override float GetCurrentValue()
    {
        if(lastIndexCalled < stats.Count) return stats[lastIndexCalled].current;
        print("ups");
        return 0;
    }
    public override float GetMaxValue()
    {
        if (lastIndexCalled < stats.Count) return stats[lastIndexCalled].max;
        print("oops");
        return 1;
    }
    public override string GetStringValue()
    {
        if (lastIndexCalled < stats.Count) return stats[lastIndexCalled].displayName;
        return "-";
    }
}
