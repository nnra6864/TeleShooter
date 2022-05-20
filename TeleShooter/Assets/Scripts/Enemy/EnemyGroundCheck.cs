using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundCheck : MonoBehaviour
{
    EnemyStats enemyStats;

    int numberOfJumps;

    private void Awake()
    {
        enemyStats = GetComponentInParent<EnemyStats>();
        numberOfJumps = enemyStats.NumberOfJumps;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Enemy") 
        {
            enemyStats.IsGrounded = true;
            enemyStats.JumpsLeft = numberOfJumps;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Enemy")
        {
            enemyStats.IsGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Enemy")
        {
            enemyStats.IsGrounded = false;
        }
    }
}
