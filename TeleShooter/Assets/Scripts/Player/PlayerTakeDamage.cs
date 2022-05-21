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
            playerStats.IsTakingDamageFromLevel = false;
            damage = obstacleStats.Damage;
            damageCooldown = obstacleStats.Cooldown;
            obstacle = collision.gameObject;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            shouldTakeDamage = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            shouldTakeDamage = false;
            playerStats.IsTakingDamageFromLevel = false;
        }
    }

    private void FixedUpdate()
    {
        if (!playerStats.CanTakeDamage) return;

        if (shouldTakeDamage && !playerStats.IsTakingDamageFromLevel)
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
        playerStats.IsTakingDamageFromLevel = true;

        playerStats.Health -= damageTaken * playerStats.ResistanceMultiplier;
        playerStats.TotalDamageTaken += damageTaken;
        if (playerStats.Health <= 0)
        {
            playerStats.CauseOfDeath = obstacle.name;
            StartCoroutine(Die());
        }

        yield return new WaitForSeconds(damageCooldown);

        playerStats.IsTakingDamageFromLevel = false;
    }

    public IEnumerator TakeDamageFromEnemy(float damageTaken, float cooldown, GameObject enemy)
    {
        playerStats.IsTakingDamageFromEnemy = true;

        playerStats.Health -= damageTaken * playerStats.ResistanceMultiplier;
        playerStats.TotalDamageTaken += damageTaken;
        if (playerStats.Health <= 0)
        {
            StartCoroutine(Die());
        }

        yield return new WaitForSeconds(damageCooldown);

        playerStats.IsTakingDamageFromEnemy = false;
    }

    public IEnumerator Die()
    {
        DisableAllPlayerComponents();

        yield return new WaitForSeconds(playerStats.DieAnimationLength);
        //Destroy(gameObject);
    }

    public void DisableAllPlayerComponents() 
    {
        GetComponent<PlayerMovement>().enabled = false;
        Destroy(GetComponent<Rigidbody>());
    }
}
