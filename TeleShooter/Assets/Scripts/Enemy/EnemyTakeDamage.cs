using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    EnemyStats enemyStats;
    [HideInInspector] public float damageCooldown;
    [HideInInspector] public float damage;
    [HideInInspector] public bool shouldTakeDamage = false;

    #region Functions
    private void Awake()
    {
        enemyStats = GetComponent<EnemyStats>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            ObstacleStats obstacleStats = collision.gameObject.GetComponentInParent<ObstacleStats>();
            shouldTakeDamage = true;
            damage = obstacleStats.Damage;
            damageCooldown = obstacleStats.Cooldown;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            shouldTakeDamage = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            shouldTakeDamage = false;
            enemyStats.IsTakingDamageFromLevel = false;
        }
    }

    private void FixedUpdate()
    {
        if (shouldTakeDamage && !enemyStats.IsTakingDamageFromLevel && enemyStats.CanTakeDamage)
        {
            StartCoroutine(TakeDamageFromLevel(damage, damageCooldown));
        }
        else if (!shouldTakeDamage && enemyStats.CanTakeDamage) 
        {
            StopCoroutine(TakeDamageFromLevel(damage, damageCooldown));
        }
    }
    #endregion

    #region Custom Functions
    public IEnumerator TakeDamageFromLevel(float damageTaken, float cooldown)
    {
        enemyStats.IsTakingDamageFromLevel = true;

        enemyStats.Health -= damageTaken * enemyStats.ResistanceMultiplier;
        if (enemyStats.Health <= 0)
        {
            StartCoroutine(Die());
        }

        yield return new WaitForSeconds(damageCooldown);

        enemyStats.IsTakingDamageFromLevel = false;
    }

    public IEnumerator TakeDamageFromPlayer(float damageTaken, float cooldown, GameObject player) 
    {
        enemyStats.IsTakingDamageFromPlayer = true;

        enemyStats.Health -= damageTaken * enemyStats.ResistanceMultiplier;
        if (enemyStats.Health <= 0)
        {
            player.GetComponent<PlayerStats>().TotalKills++;
            StartCoroutine(Die());
        }

        yield return new WaitForSeconds(damageCooldown);

        enemyStats.IsTakingDamageFromPlayer = false;
    }

    public void TakeDamageFromProjectile(float damageTaken)
    {
        enemyStats.Health -= damageTaken * enemyStats.ResistanceMultiplier;
        if (enemyStats.Health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die() 
    {
        yield return new WaitForEndOfFrame();
        DisableAllEnemyComponents();

        yield return new WaitForSeconds(enemyStats.DieAnimationLength);
        Destroy(gameObject);
    }

    public void DisableAllEnemyComponents() 
    {
        GetComponent<EnemyMove>().enabled = false;
        GetComponent<EnemyRaycast>().enabled = false;
        GetComponentInChildren<EnemyGroundCheck>().enabled = false;
        GetComponentInChildren<EnemyJump>().enabled = false;
        GetComponentInChildren<EnemyJumpObstacleCheck>().enabled = false;
        GetComponentInChildren<EnemyStuckCheck>().enabled = false;
        GetComponent<Collider>().enabled = false;
        Destroy(GetComponent<Rigidbody>());
    }
    #endregion
}
