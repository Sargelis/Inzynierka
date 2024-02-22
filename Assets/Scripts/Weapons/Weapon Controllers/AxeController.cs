using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : WeaponController
{
    [Header("Axe Weapon")]
    [SerializeField] GameObject axePrefab;
    float cooldownAxe = 0f;
    [SerializeField] float fireRateAxe = 2.5f; // sekundy
    public float damage;

    void Start()
    {
        firePoint = FindObjectOfType<PlayerStats>().transform;
        cooldownAxe = fireRateAxe;
    }
    void Update()
    {
        if (cooldownAxe < 0f)   //Axe
        {
            Shoot();
            cooldownAxe = fireRateAxe;
        }
        cooldownAxe -= Time.deltaTime;
    }

    void Shoot() 
    {
        GameObject axe = (GameObject)Instantiate(axePrefab, firePoint.position, firePoint.rotation);
        axe.transform.SetParent(transform);
    }
}