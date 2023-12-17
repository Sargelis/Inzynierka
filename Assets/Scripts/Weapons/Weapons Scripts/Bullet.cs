using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform target;
    [SerializeField] float speed;
    [SerializeField] float damage;

    void Update()
    {
        if (target == null) //jak target null to usu� obiekt
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

        transform.Translate (dir.normalized * distancethisFrame, Space.World);// przemie�� obiekt
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
    }
}