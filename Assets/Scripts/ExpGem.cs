using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExpGem : MonoBehaviour
{
    public float gemValue = 10f;

    void Start()
    {
    }
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindAnyObjectByType<PlayerStats>().exp += gemValue;
            Destroy(gameObject);
        }
    }
}