using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : WeaponController
{
    [Header("Fireball Weapon")]
    [SerializeField] GameObject fireballPrefab;
    [HideInInspector] public float cooldownFireball = 0f;
    public float fireRateFireball = 3f; // sekundy
    public float damage;

    void Start()
    {
        firePoint = FindObjectOfType<PlayerStats>().transform;
        cooldownFireball = fireRateFireball;
    }
    void Update()
    {
        if (cooldownFireball < 0f)  //Fireball
        {
            Shoot();
            cooldownFireball = fireRateFireball;
        }
        cooldownFireball -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject fireball = (GameObject)Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        fireball.transform.SetParent(transform);
    }
}