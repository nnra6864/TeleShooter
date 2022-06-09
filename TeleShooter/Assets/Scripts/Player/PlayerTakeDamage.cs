using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    PlayerStats playerStats;
    
    [HideInInspector]
    public float damageCooldown;
    
    [HideInInspector]
    public float damage;
    
    [HideInInspector]
    public bool shouldTakeDamage = false;
    
    GameObject obstacle;

    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            ObstacleStats obstacleStats = collision.gameObject.GetComponentInParent<ObstacleStats>();
            shouldTakeDamage = true;
            playerStats.isTakingDamageFromLevel = false;
            damage = obstacleStats.Damage;
            damageCooldown = obstacleStats.Cooldown;
            obstacle = collision.gameObject;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        shouldTakeDamage = collision.gameObject.tag == "Obstacle";
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            shouldTakeDamage = false;
            playerStats.isTakingDamageFromLevel = false;
        }
    }

    private void FixedUpdate()
    {
        if (!playerStats.canTakeDamage) return;

        if (shouldTakeDamage && !playerStats.isTakingDamageFromLevel)
        {
            StartCoroutine(TakeDamageFromLevel(damage, damageCooldown, obstacle));
        }
        else if (!shouldTakeDamage)
        {
            StopCoroutine(TakeDamageFromLevel(damage, damageCooldown, obstacle));
        }
    }

    public IEnumerator TakeDamageFromLevel(float damageTaken, float cooldown, GameObject obstacle)
    {
        playerStats.isTakingDamageFromLevel = true;

        playerStats.health -= damageTaken * playerStats.resistanceMultiplier;
        playerStats.totalDamageTaken += damageTaken;
        if (playerStats.health <= 0)
        {
            playerStats.causeOfDeath = obstacle.name;
            StartCoroutine(Die());
        }

        yield return new WaitForSeconds(damageCooldown);

        playerStats.isTakingDamageFromLevel = false;
    }

    public IEnumerator TakeDamageFromEnemy(float damageTaken, float cooldown, GameObject enemy)
    {
        playerStats.isTakingDamageFromEntity = true;

        playerStats.health -= damageTaken * playerStats.resistanceMultiplier;
        playerStats.totalDamageTaken += damageTaken;
        if (playerStats.health <= 0)
        {
            StartCoroutine(Die());
        }

        yield return new WaitForSeconds(damageCooldown);

        playerStats.isTakingDamageFromEntity = false;
    }

    public IEnumerator Die()
    {
        DisableAllPlayerComponents();

        yield return new WaitForSeconds(playerStats.dieAnimationLength);
        //Destroy(gameObject);
    }

    public void DisableAllPlayerComponents() 
    {
        GetComponent<PlayerMovement>().enabled = false;
        Destroy(GetComponent<Rigidbody>());
    }
}
