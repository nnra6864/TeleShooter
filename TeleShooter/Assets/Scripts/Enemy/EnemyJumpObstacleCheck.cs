using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpObstacleCheck : MonoBehaviour
{
    EnemyStats enemyStats;

    private void Awake()
    {
        enemyStats = transform.parent.GetComponentInParent<EnemyStats>();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            enemyStats.shouldJump = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            enemyStats.shouldJump = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            enemyStats.shouldJump = true;
        }
    }
}
