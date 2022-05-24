using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;

    public float damage;
    public float speed;

    [Tooltip("Set to true if shot by Player and false if shot by Enemy.")]
    [ReadOnlyField] public bool shotByPlayer;
    [Tooltip("Pass you PlayerStats/EnemyStats damage multiplier.")]
    [ReadOnlyField] public float damageMultiplier;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Dissapear());
    }

    void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (shotByPlayer && collision.gameObject.CompareTag("Enemy")) 
        {
            EnemyTakeDamage enemyTakeDamage = collision.gameObject.GetComponent<EnemyTakeDamage>();
            enemyTakeDamage.TakeDamageFromProjectile(damage * damageMultiplier);
        }

        Destroy(gameObject);
    }

    IEnumerator Dissapear() 
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
