using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheController : WeaponController
{
    [Header("Scythe Weapon")]
    [SerializeField] GameObject scythePrefab;
    float cooldownScythe = 0f;
    [SerializeField] float fireRateScythe = 2f; // sekundy
    public float damage;

    void Start()
    {
        firePoint = FindObjectOfType<PlayerStats>().transform;
        cooldownScythe = fireRateScythe;
    }
    void Update()
    {
        if (cooldownScythe < 0f)  //Scythe
        {
            Shoot();
            cooldownScythe = fireRateScythe;
        }
        cooldownScythe -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject scythe = (GameObject)Instantiate(scythePrefab, firePoint.position, firePoint.rotation);
        scythe.transform.SetParent(transform);
    }
}