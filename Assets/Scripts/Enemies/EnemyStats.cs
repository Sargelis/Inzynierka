using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] float damage;
    public float moveSpeed; //musi public
    [SerializeField] float maxHealth;
    [SerializeField] GameObject drop;
    float currentHealth;

    [SerializeField] float despawnDistance = 20f;
    Transform player;

    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        currentHealth = maxHealth;
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) >= despawnDistance) ReturnEnemy();
    }

    public void OnDestroy() //jak zniszczony to stwórz obiekt
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        es.OnEnemyKilled();
        Instantiate(drop, transform.position, Quaternion.identity);
    }
    public void TakeDanage(float amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0) Destroy(gameObject);
    }
    void ReturnEnemy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)].position;
    }
}