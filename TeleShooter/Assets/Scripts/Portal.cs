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
        if (this.transform.parent.GetChild(0).name == p1.name)
        {
            p2 = this.transform.parent.GetChild(1).gameObject;
        }
        else 
        {
            p2 = this.transform.parent.GetChild(0).gameObject;
        }

        teleportDestination = p2.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canTeleport = other.GetComponent<PlayerStats>().CanTeleport;

            if (canTeleport && other.GetComponent<PlayerStats>().LastPortalName == "")
            {
                PlayerStats playerStats = other.GetComponent<PlayerStats>();

                playerStats.LastPortalName = this.name;
                playerStats.TotalTeleportations++;
                other.transform.position = teleportDestination;
            }
        }

        if (other.tag == "Enemy")
        {
            canTeleport = other.GetComponent<EnemyStats>().CanTeleport;

            if (canTeleport && other.GetComponent<EnemyStats>().LastPortalName == "")
            {
                EnemyStats enemyStats = other.GetComponent<EnemyStats>();

                enemyStats.LastPortalName = this.name;
                enemyStats.HasTeleported = true;
                other.transform.position = teleportDestination;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerStats>().LastPortalName != this.name)
            {
                other.GetComponent<PlayerStats>().LastPortalName = "";
            }
        }
        if (other.tag == "Enemy")
        {
            if (other.GetComponent<EnemyStats>().LastPortalName != this.name)
            {
                other.GetComponent<EnemyStats>().LastPortalName = "";
            }
        }
    }
}
