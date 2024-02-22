using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class BookController : WeaponController
{
    [Header("Book Weapon")]
    [SerializeField] GameObject bookPrefab;
    [SerializeField] GameObject rotatorPrefab;
    GameObject rotator;
    public float cooldownBook = 0f;
    public float fireRateBook = 0.75f; // sekundy
    public float damage;
    [SerializeField] float rotationSpeed;
    [SerializeField] float radiusOffset;
    public int maxSpawnCount = 3;
    public int spawnCount = 0;

    //zmiana fire rate przy sta³ej prêdkosci(150) 0,75 = 3, 0,6=4, 0,5=5 0,4=6

    void Start()
    {
        firePoint = FindObjectOfType<PlayerStats>().transform;
        cooldownBook = fireRateBook;
        GameObject Rotator = (GameObject)Instantiate(rotatorPrefab, transform.position, transform.rotation);
        Rotator.transform.SetParent(transform);
        rotator = GameObject.FindGameObjectWithTag("Rotator");
        cooldownBook = fireRateBook;
    }
    void Update()
    {
        rotator = GameObject.FindGameObjectWithTag("Rotator");
        rotator.transform.rotation = Quaternion.Euler(0, 0, rotator.transform.rotation.eulerAngles.z + (rotationSpeed * Time.deltaTime));

        fireRateBook -= Time.deltaTime;

        if (fireRateBook <= 0)
        {
            if (spawnCount == maxSpawnCount)
            {
                fireRateBook = cooldownBook;
            }
            else
            {
                GameObject book = (GameObject)Instantiate(bookPrefab, new Vector3((firePoint.transform.position.x + radiusOffset), firePoint.transform.position.y, firePoint.transform.position.z), rotator.transform.rotation);
                book.transform.SetParent(rotator.transform);
                fireRateBook = cooldownBook;
                spawnCount++;
            }
        }
    }
}