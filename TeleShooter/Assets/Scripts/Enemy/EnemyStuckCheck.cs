using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStuckCheck : MonoBehaviour
{
    EnemyStats enemyStats;

    bool shouldCheck;
    bool isChecking;

    private void Awake()
    {
        enemyStats = GetComponentInParent<EnemyStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        shouldCheck = other.CompareTag("Level");
    }

    private void OnTriggerStay(Collider other)
    {
        shouldCheck = other.CompareTag("Level");
    }

    private void OnTriggerExit(Collider other)
    {
        shouldCheck = !(other.CompareTag("Level"));
    }

    private void FixedUpdate()
    {
        if (shouldCheck && !isChecking && !enemyStats.shouldFollowPlayer)
        {
            StartCoroutine(StuckTimer());
            shouldCheck = false;
        }
    }

    IEnumerator StuckTimer() 
    {
        isChecking = true;
        Vector3 previousPosition = transform.position;

        yield return new WaitForSeconds(enemyStats.stuckTime);

        if (Vector3.Distance(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(previousPosition.x, previousPosition.y, 0)) < 1)
        {
            enemyStats.canMove = false;
            enemyStats.shouldChoosePoint = true;
            GetComponentInParent<EnemyMove>().ChooseRandomPoint();
        }
        shouldCheck = true;
        isChecking = false;
    }
}
