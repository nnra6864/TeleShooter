using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class PlayerMelee : MonoBehaviour
{
    #region Variables
    PlayerStats playerStats;
    List<GameObject> enemies = new List<GameObject>();
    List<GameObject> deadEnemies = new List<GameObject>();
    EnemyTakeDamage enemyTakeDamage;
    Clock clock;

    [Tooltip("Ammount of Damage the Weapon will do.")]
    public float damage = 1;

    [Tooltip("Maximum number of Targets.\nSet to -1 for Infinite.")]
    public int targetLimit = -1;

    [Tooltip("Time in Seconds the Enemy will be Immune to Damage after getting Hit.")]
    public float enemyImmunityTime = 0;

    [Tooltip("Lower Limmmit of a Cooldown in Seconds before being able to Attack again.\nSet both Lower and Upper Limit to the same value if you'd like it to be constant.")]
    public float cooldownLowerLimit = 0;

    [Tooltip("Upper Limmmit of a Cooldown in Seconds before being able to Attack again.\nSet both Lower and Upper Limit to the same value if you'd like it to be constant.")]
    public float cooldownUpperLimit = 0;

    bool isAttacking = false;
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !enemies.Contains(other.gameObject))
        {
            enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" && !enemies.Contains(other.gameObject))
        {
            enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy" && enemies.Contains(other.gameObject))
        {
            enemies.Remove(other.gameObject);
        }
    }

    private void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();
        clock = Timekeeper.instance.Clock("Player");
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
                    playerStats.totalDamageGiven += damage;
                    playerStats.totalMeleeAttacks++;
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
        if (!playerStats.canAttack) yield break;

        isAttacking = true;

        EnemiesTakeDamage();

        float cooldown = Random.Range(cooldownLowerLimit, cooldownUpperLimit);
        yield return new WaitForSeconds(cooldown / clock.timeScale);

        isAttacking = false;
    }
}
