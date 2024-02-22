using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookWeapon : BookController
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) collision.gameObject.GetComponent<EnemyStats>().TakeDanage(damage);
        if (collision.CompareTag("BOSS")) collision.gameObject.GetComponent<EnemyStats>().TakeDanage(damage);
    }
}