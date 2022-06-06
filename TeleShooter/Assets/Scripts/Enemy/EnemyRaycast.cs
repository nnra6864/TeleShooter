using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRaycast : MonoBehaviour
{
    EnemyStats enemyStats;
    EnemyMove enemyMove;
    Vector3 rayDirection;
    float pointDistance;
    public LayerMask layerMask;

    public void Awake()
    {
        enemyStats = GetComponent<EnemyStats>();
        enemyMove = GetComponent<EnemyMove>();
    }

    private void FixedUpdate()
    {
        rayDirection = enemyStats.isMovingBackwards ? -transform.right : transform.right;

        RaycastHit hit;
        Ray ray = new Ray(transform.position, rayDirection);

        if (Physics.Raycast(transform.position, rayDirection, out hit, Mathf.Infinity, ~layerMask))
        {
            if (hit.collider.gameObject.tag == "Player" && enemyStats.canFollowPlayer)
            {
                if (enemyStats.distanceFromPlayerToStartFollowing >= hit.distance)
                {
                    enemyStats.followTargetPlayer = hit.collider.gameObject;
                    enemyStats.shouldFollowPlayer = true;
                    enemyMove.StopCoroutine(enemyMove.HasReachedPoint());
                    enemyMove.ChooseTarget();
                }
                
                if (enemyStats.distanceFromPlayerToStopFollowing >= hit.distance)
                {
                    enemyStats.shouldFollowPlayer = false;
                }
            }
            else
            {
                enemyStats.shouldFollowPlayer = false;
            }
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }
    }
}
