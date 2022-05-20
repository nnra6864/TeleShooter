using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRaycast : MonoBehaviour
{
    EnemyStats enemyStats;
    Vector3 rayDirection;
    float pointDistance;
    LayerMask layerMask;

    public void Awake()
    {
        enemyStats = GetComponent<EnemyStats>();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        rayDirection = enemyStats.IsBackwards ? -transform.right : transform.right;

        RaycastHit hit;
        Ray ray = new Ray(transform.position, rayDirection);

        if (Physics.Raycast(transform.position, rayDirection, out hit, Mathf.Infinity, ~layerMask))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                pointDistance = hit.distance;
            }
            else
            {

            }
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }
    }
}
