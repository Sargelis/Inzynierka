using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheWeapon : ScytheController
{
    [SerializeField] float speed;
    [SerializeField] float duration;

    float rotation;
    float angle;
    Vector3 playerPosition;
    Vector3 startPosition;
    float x, y, a, b;

    void Start()
    {
        angle = Random.Range(0f, 360f);
        a = Mathf.Sin(angle);
        b = Mathf.Cos(angle);

        firePoint = FindObjectOfType<PlayerStats>().transform;
        startPosition = firePoint.transform.position;
    }
    void Update()
    {
        playerPosition = firePoint.transform.position;
        x += speed * Time.deltaTime;
        y += speed * Time.deltaTime;
        rotation += Time.deltaTime * 10; 
        
        if (duration <= 0f)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
            if(transform.position == playerPosition) Destroy(gameObject);
        }
        else
        {
            this.transform.position = startPosition + new Vector3(x * a, y * b, 0f);
        }
        this.transform.Rotate(0, 0, rotation);

        duration -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) collision.gameObject.GetComponent<EnemyStats>().TakeDanage(damage);
        if (collision.CompareTag("BOSS")) collision.gameObject.GetComponent<EnemyStats>().TakeDanage(damage);
    }
}