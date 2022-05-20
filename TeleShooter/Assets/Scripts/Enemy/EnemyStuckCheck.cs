using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStuckCheck : MonoBehaviour
{
    EnemyStats enemyStats;

    Vector3 previousPosition;
    bool shouldCheck;
    bool isChecking;
    int stuckTime;

    private void Awake()
    {
        enemyStats = GetComponentInParent<EnemyStats>();
        stuckTime = enemyStats.StuckTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Level") 
        {
            shouldCheck = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Level")
        {
            shouldCheck = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Level")
        {
            shouldCheck = false;
        }
    }

    private void FixedUpdate()
    {
        if (shouldCheck && !isChecking)
        {
            StartCoroutine(StuckTimer());
            shouldCheck = false;
        }
    }

    IEnumerator StuckTimer() 
    {
        isChecking = true;
        previousPosition = transform.position;
        yield return new WaitForSeconds(stuckTime);
        if (Vector3.Distance(new Vector3(0, transform.position.y, 0), new Vector3(0, previousPosition.y, 0)) < 1)
        {
            enemyStats.CanMove = false;
            enemyStats.ShouldChoosePoint = true;
            GetComponentInParent<EnemyMove>().ChooseRandomPoint();
        }
        shouldCheck = true;
        isChecking = false;
    }
}
