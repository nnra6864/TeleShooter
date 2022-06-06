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
        enemyStats.shouldJump = !(other.CompareTag("Obstacle"))
    }

    private void OnTriggerStay(Collider other)
    {
        enemyStats.shouldJump = !(other.CompareTag("Obstacle"))
    }

    private void OnTriggerExit(Collider other)
    {
        enemyStats.shouldJump = other.CompareTag("Obstacle")
    }
}
