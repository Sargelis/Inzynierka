using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : WeaponController
{
    private Transform target;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float range = 10f;
    [SerializeField] float fireRate = 2f; // sekundy
    [SerializeField] public float damage;
    float cooldown = 0f;

    void Start()
    {
        firePoint = FindObjectOfType<PlayerStats>().transform;
        InvokeRepeating("UpdateTarget", 0f, 0.5f); //wywo�aj funkcje w "" po czasie 0sek co 0,5sek
    }
    void Update()
    {
        if (target == null) return;
        if (cooldown <= 0f ) //cooldown <=0 to strzelaj i reset cooldown
        {
            Shoot();
            cooldown = fireRate / 1f;
        }
        cooldown -= Time.deltaTime; //obi� cooldown
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); //stw�rz list� obiekt�w z tagiem enemyTag
        float shorthestDist = Mathf.Infinity; //ustaw dystans na nieskonczono��
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies) //dla kazdego enemy w li�cie enemies
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position); //oblicz odleg�o�� do enemy
            if (distanceToEnemy < shorthestDist ) //je�eli odleg�o�c do enenmy < najmniejszej odleg�o�ci to najmniejsza odleg�o�c = odleg�o�c do enemy i neaest enemy to ten enemy
            {
                shorthestDist = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shorthestDist <= range) target = nearestEnemy.transform; //je�eli najbli�y enemy != null i najkr�tszy dystans <= zasi�g to ustaw target na tego nearestenemy
        else target = null;
    }

    void Shoot()
    {
        GameObject bulletGO =  (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); //sw�rz klona prefab o pozycji i rotacji
        bulletGO.transform.SetParent(transform);
        Bullet bullet = bulletGO.GetComponent<Bullet>(); 

        if (bullet != null) bullet.Seek(target); //jak bullet != null to szukaj target
    }
}
