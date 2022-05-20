using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpObstacleCheck : MonoBehaviour
{
    EnemyJump enemyJump;

    private void Awake()
    {
        enemyJump = transform.parent.GetComponentInChildren<EnemyJump>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            enemyJump.shouldJump = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            enemyJump.shouldJump = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            enemyJump.shouldJump = true;
        }
    }
}
