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
        shouldTakeDamage = collision.gameObject.CompareTag("Obstacle")
    }

    private void OnCollisionExit(Collision collision)
    {
        var isObstacle = collision.gameObject.CompareTag("Obstacle");
        shouldTakeDamage = isObstacle;
        enemyStats.isTakingDamageFromLevel = isObstacle;
    }

    private void FixedUpdate()
    {
        if (shouldTakeDamage && !enemyStats.isTakingDamageFromLevel && enemyStats.canTakeDamage)
        {
            StartCoroutine(TakeDamageFromLevel(damage, damageCooldown));
        }
        else if (!shouldTakeDamage && enemyStats.canTakeDamage) 
        {
            StopCoroutine(TakeDamageFromLevel(damage, damageCooldown));
        }
    }
    #endregion

    #region Custom Functions
    public IEnumerator TakeDamageFromLevel(float damageTaken, float cooldown)
    {
        if (!enemyStats.canTakeDamage) yield break;

        enemyStats.isTakingDamageFromLevel = true;

        enemyStats.health -= damageTaken * enemyStats.resistanceMultiplier;
        if (enemyStats.health <= 0)
        {
            StartCoroutine(Die());
        }

        yield return new WaitForSeconds(damageCooldown);

        enemyStats.isTakingDamageFromLevel = false;
    }

    public IEnumerator TakeDamageFromPlayer(float damageTaken, float cooldown, GameObject player) 
    {
        if (!enemyStats.canTakeDamage) yield break;

        enemyStats.isTakingDamageFromPlayer = true;

        enemyStats.health -= damageTaken * enemyStats.resistanceMultiplier;
        if (enemyStats.health <= 0)
        {
            player.GetComponent<PlayerStats>().totalKills++;
            StartCoroutine(Die());
        }

        yield return new WaitForSeconds(damageCooldown);

        enemyStats.isTakingDamageFromPlayer = false;
    }

    public void TakeDamageFromProjectile(float damageTaken)
    {
        enemyStats.health -= damageTaken * enemyStats.resistanceMultiplier;
        if (enemyStats.health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die() 
    {
        yield return new WaitForEndOfFrame();
        DisableAllEnemyComponents();

        yield return new WaitForSeconds(enemyStats.dieAnimationLength);
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
