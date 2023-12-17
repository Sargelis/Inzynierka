using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookWeapon : BookController
{
    [SerializeField] float speed;
    [SerializeField] float radiusOffset;
    [SerializeField] float damage;

    float angle = 0f;
    float direction = -1f;
    Vector3 playerPosition;
    GameObject centreObject;

     void Start()
    {
        centreObject = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        playerPosition = centreObject.transform.position;
        angle += Time.deltaTime * direction * speed;
        float x = Mathf.Cos(angle) * radiusOffset; //cos 0 = 1
        float y = Mathf.Sin(angle) * radiusOffset; //sin 0 = 0

        this.transform.position = new Vector3(playerPosition.x + x, playerPosition.y + y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) collision.gameObject.GetComponent<EnemyStats>().TakeDanage(damage);
    }
}