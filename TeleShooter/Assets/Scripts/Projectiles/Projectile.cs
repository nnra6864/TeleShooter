 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;
    ProjectileStats projectileStats;

    void Awake()
    {
        projectileStats = GetComponent<ProjectileStats>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Dissapear());
    }

    void FixedUpdate()
    {
        rb.velocity = transform.right * projectileStats.speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (projectileStats.shotByPlayer && collision.gameObject.CompareTag("Enemy")) 
        {
            EnemyTakeDamage enemyTakeDamage = collision.gameObject.GetComponent<EnemyTakeDamage>();
            enemyTakeDamage.TakeDamageFromProjectile(projectileStats.damage * projectileStats.damageMultiplier);
        }

        Destroy(gameObject);
    }

    IEnumerator Dissapear() 
    {
        yield return new WaitForSeconds(projectileStats.lifeTime);
        Destroy(gameObject);
    }
}
