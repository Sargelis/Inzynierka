using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballWeapon : FireballController
{
    [SerializeField] float speed;
    [SerializeField] float damage;
    [SerializeField] float duration;

    float angle;
    GameObject firePosition;
    Vector3 playerPosition;
    float x, y, a, b;

    void Start()
    {
        angle = Random.Range(0f, 360f);
        a = Mathf.Sin(angle);
        b = Mathf.Cos(angle);

        firePosition = GameObject.FindGameObjectWithTag("Player");
        playerPosition = firePosition.transform.position;
    }
    void Update()
    {
        x += speed * Time.deltaTime;
        y += speed * Time.deltaTime;
        this.transform.position = playerPosition + new Vector3(x*a, y*b, 0f);

        duration -= Time.deltaTime;
        if (duration <= 0f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) collision.gameObject.GetComponent<EnemyStats>().TakeDanage(damage);
    }
}