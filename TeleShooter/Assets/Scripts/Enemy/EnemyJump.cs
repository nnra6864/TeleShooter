using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    EnemyStats enemyStats;
    private EnemyMove enemyMove;
    private Rigidbody rigidBody;
    public EnemyRaycast enemyRaycast;

    public bool shouldJump = true;
    public bool enemyIsBelowPoint = false;
    [SerializeField] string check; //Either Level or Obstacle

    private void Awake()
    {
        enemyStats = GetComponentInParent<EnemyStats>();
        enemyMove = GetComponentInParent<EnemyMove>();
        enemyMove.choseNewPoint += IsEnemyBelowPoint;
        rigidBody = GetComponentInParent<Rigidbody>();
        enemyRaycast = GetComponentInParent<EnemyRaycast>();

        shouldJump = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (check == "Level")
        {
            if (shouldJump)
            {
                IsEnemyBelowPoint();
                if (enemyIsBelowPoint)
                {
                    if (other.tag == "Level")
                    {
                        if (enemyStats.JumpsLeft > 0)
                        {
                            rigidBody.velocity = new Vector3(rigidBody.velocity.x, enemyStats.JumpHeight, rigidBody.velocity.z);
                            enemyStats.JumpsLeft--;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (check == "Obstacle")
        {
            if (other.tag == "Obstacle")
            {
                if (enemyStats.JumpsLeft > 0)
                {
                    rigidBody.velocity = new Vector3(rigidBody.velocity.x, enemyStats.ObstacleJumpHeight, rigidBody.velocity.z);
                    enemyStats.JumpsLeft--;
                }
            }
        }

        if (check == "Level")
        {
            if (shouldJump)
            {
                IsEnemyBelowPoint();
                if (enemyIsBelowPoint)
                {
                    if (other.tag == "Level")
                    {
                        if (enemyStats.JumpsLeft > 0)
                        {
                            rigidBody.velocity = new Vector3(rigidBody.velocity.x, enemyStats.JumpHeight, rigidBody.velocity.z);
                            enemyStats.JumpsLeft--;
                        }
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (rigidBody == null) return;
        if (rigidBody.velocity.y < 0)
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y + (rigidBody.velocity.y / 20), rigidBody.velocity.z);
        }
    }

    #region Functions
    public void IsEnemyBelowPoint() 
    {
        if (enemyMove.points.Count > 0)
        {
            if (enemyMove.transform.position.y < enemyMove.points[enemyStats.PointIndex].transform.position.y)
            {
                enemyIsBelowPoint = true;
            }
            else { enemyIsBelowPoint = false; }
        }
    }

    #endregion
}
