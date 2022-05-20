using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public float damage = 1;
    public int targetLimit = -1;
    public float enemyImmunityTime = 0;
    public float cooldownLowerLimit = 0;
    public float cooldownUpperLimit = 0;

    bool isAttacking = false;

    PlayerStats playerStats;
    List<GameObject> enemies = new List<GameObject>();
    List<GameObject> deadEnemies = new List<GameObject>();
    EnemyTakeDamage enemyTakeDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (!enemies.Contains(other.gameObject))
            {
                enemies.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (!enemies.Contains(other.gameObject))
            {
                enemies.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (enemies.Contains(other.gameObject))
            {
                enemies.Remove(other.gameObject);
            }
        }
    }

    private void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    public void EnemiesTakeDamage() 
    {
        int hits = 0;
        deadEnemies.Clear();

        foreach (GameObject enemy in enemies)
        {
            if (hits <= targetLimit || targetLimit == -1)
            {
                if (enemy != null)
                {
                    enemyTakeDamage = enemy.GetComponent<EnemyTakeDamage>(); enemyTakeDamage.StartCoroutine(enemyTakeDamage.TakeDamageFromPlayer(damage, enemyImmunityTime, this.transform.root.gameObject));
                    playerStats.TotalDamageGiven += damage;
                    playerStats.TotalMeleeAttacks++;
                    hits++;
                }
                else
                {
                    deadEnemies.Add(enemy);
                }
            }
        }
        foreach (GameObject enemy in deadEnemies)
        {
            enemies.Remove(enemy);
        }
    }

    public IEnumerator Attack() 
    {
        isAttacking = true;

        EnemiesTakeDamage();

        float cooldown = Random.Range(cooldownLowerLimit, cooldownUpperLimit);
        yield return new WaitForSeconds(cooldown);

        isAttacking = false;
    }
}
