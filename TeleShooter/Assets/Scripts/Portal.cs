using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    Vector3 teleportDestination;
    bool canTeleport;
    GameObject p1;
    GameObject p2;

    private void Awake()
    {
        p1 = this.gameObject;
        var childNum = this.transform.parent.GetChild(0).name == p1.name ? 1 : 0;
        p2 = this.transform.parent.GetChild(childNum).gameObject;
        teleportDestination = p2.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        // this can be replaced with a switch statement
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();

            canTeleport = playerStats.canTeleport;
            if (canTeleport && playerStats.lastPortalName == "")
            {
                playerStats.lastPortalName = this.name;
                playerStats.totalTeleportations++;
                other.transform.position = teleportDestination;
            }
        }

        if (other.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();

            canTeleport = enemyStats.canTeleport;
            if (canTeleport && enemyStats.lastPortalName == "")
            {
                enemyStats.lastPortalName = this.name;
                enemyStats.hasTeleported = true;
                other.transform.position = teleportDestination;
            }
        }

        if (other.CompareTag("Projectile"))
        {
            ProjectileStats projectileStats = other.GetComponent<ProjectileStats>();
            canTeleport = projectileStats.canTeleport;

            if (canTeleport && projectileStats.lastPortalName == "")
            {
                projectileStats.lastPortalName = this.name;
                projectileStats.hasTeleported = true;
                other.transform.position = teleportDestination;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // this too
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats.lastPortalName != this.name)
            {
                playerStats.lastPortalName = "";
            }
        }
        if (other.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats.lastPortalName != this.name)
            {
                enemyStats.lastPortalName = "";
            }
        }
        if (other.CompareTag("Projectile"))
        {
            ProjectileStats projileStats = other.GetComponent<ProjectileStats>();
            if (projileStats.lastPortalName != this.name)
            {
                projileStats.lastPortalName = "";
            }
        }
    }
}
