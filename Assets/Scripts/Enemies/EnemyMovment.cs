using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyMovment : MonoBehaviour
{
    Transform player;
    [SerializeField] EnemyStats enemyStats;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform; //znajdŸ player
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyStats.moveSpeed * Time.deltaTime); //poruszaj siê w stronê player
    }

    private void OnCollisionEnter2D(Collision2D collision) //jak kolizja to player hp--
    {
        if(collision.collider.CompareTag("Player")) FindAnyObjectByType<PlayerStats>().currentHealth--; //playerStats.health--;
    }
}