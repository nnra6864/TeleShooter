using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundCheck : MonoBehaviour
{
    EnemyStats enemyStats;

    private void Awake()
    {
        enemyStats = GetComponentInParent<EnemyStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Enemy") 
        {
            enemyStats.isGrounded = true;
            enemyStats.jumpsLeft = enemyStats.numberOfJumps;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        enegyStats.isGrounded = (other.tag != "Player" && other.tag != "Enemy");
    }

    private void OnTriggerExit(Collider other)
    {
        enemyState.isGrounded = !(other.tag != "Player" && other.tag != "Enemy");
    }
}
