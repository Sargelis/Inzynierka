using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeWeapon : AxeController
{
    [SerializeField] float speed;
    [SerializeField] float moveSpeed;
    [SerializeField] float duration;

    float radiusOffset = 0f;
    float angle = 0f;
    float direction = -1f;
    float rotation = 0f;
    Vector3 startPosition;

    void Start()
    {
        firePoint = FindObjectOfType<PlayerStats>().transform;
        startPosition = firePoint.position;
    }
    void Update()
    {
        angle += Time.deltaTime * direction * speed; //odpowiada za ruch po kole
        radiusOffset += Time.deltaTime * moveSpeed; //radiusOffset odpowiada za zwi�kszanie promienia
        rotation += Time.deltaTime * 1;
        float x = Mathf.Cos(angle) * radiusOffset;
        float y = Mathf.Sin(angle) * radiusOffset;

        this.transform.position = startPosition + new Vector3(x, y, 0);
        this.transform.Rotate(0, 0, rotation);
        duration -= Time.deltaTime;
        if (duration <= 0f ) Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) collision.gameObject.GetComponent<EnemyStats>().TakeDanage(damage);
        if (collision.CompareTag("BOSS")) collision.gameObject.GetComponent<EnemyStats>().TakeDanage(damage);
    }
}