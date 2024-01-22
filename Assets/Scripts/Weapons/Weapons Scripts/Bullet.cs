using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : PlayerShooting
{
    private Transform target;
    [SerializeField] float speed;

    void Update()
    {
        if (target == null) //jak target null to usuñ obiekt
        {
            Destroy(gameObject);
            return;
        }

        Vector2 dir = target.position - transform.position;
        float distancethisFrame = speed * Time.deltaTime;

        if ( dir.magnitude <= distancethisFrame )
        {
            HitTarget();
            return;
        }

        transform.Translate (dir.normalized * distancethisFrame, Space.World);// przemieœæ obiekt
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }
    void HitTarget()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) collision.gameObject.GetComponent<EnemyStats>().TakeDanage(damage);
        if (collision.CompareTag("BOSS")) collision.gameObject.GetComponent<EnemyStats>().TakeDanage(damage);
    }
}