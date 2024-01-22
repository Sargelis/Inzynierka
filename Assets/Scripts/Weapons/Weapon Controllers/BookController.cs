using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : WeaponController
{
    [Header("Book Weapon")]
    [SerializeField] GameObject bookPrefab;
    float cooldownBook = 0f;
    [SerializeField] float fireRateBook = 3f; // sekundy
    [SerializeField] public float damage;

    void Start()
    {
        firePoint = FindObjectOfType<PlayerStats>().transform;
        cooldownBook = fireRateBook;
    }
    void Update()
    {
        if (cooldownBook < 0f)  //Book trochê inaczej 
        {
            Shoot();
            cooldownBook = fireRateBook;
        }
        cooldownBook -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject book = (GameObject)Instantiate(bookPrefab, firePoint.position, firePoint.rotation);
        book.transform.SetParent(transform);
        AxeWeapon bookWeapon = book.GetComponent<AxeWeapon>();
    }
}