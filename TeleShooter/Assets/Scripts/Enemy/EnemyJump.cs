using System.Collections;
using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    EnemyStats enemyStats;
    EnemyMove enemyMove;
    Rigidbody rigidBody;

    bool enemyIsBelowPoint = false;

    enum CheckType { Level, Obstacle }
    [SerializeField, Tooltip("Determines if it will check for Level of Obstacles")]
    CheckType checkType;

    #region Functions
    private void Awake()
    {
        enemyStats = GetComponentInParent<EnemyStats>();
        enemyMove = GetComponentInParent<EnemyMove>();
        enemyMove.choseNewPoint += IsEnemyBelowPoint;
        rigidBody = GetComponentInParent<Rigidbody>();
        enemyStats.ShouldJump = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (checkType == CheckType.Level) CollisionWithLevel(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if(checkType == CheckType.Obstacle) StartCoroutine(CollisionWithObstacle(other));
        else if(checkType == CheckType.Level) CollisionWithLevel(other);
    }

    private void FixedUpdate()
    {
        if (rigidBody == null) return;
        if (rigidBody.velocity.y < 0)
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y + (rigidBody.velocity.y / 20), rigidBody.velocity.z);
        }
    }
    #endregion

    #region Custom Functions
    public void IsEnemyBelowPoint() 
    {
        if (enemyMove.points.Count <= 0) return;
        enemyIsBelowPoint = enemyMove.transform.position.y < enemyMove.points[enemyStats.PointIndex].transform.position.y;
    }

    public IEnumerator CollisionWithObstacle(Collider other)
    {
        if (!other.CompareTag("Obstacle")) yield break;
        if (enemyStats.JumpsLeft <= 0) yield break;

        enemyStats.Speed *= enemyStats.ObstacleSpeedMultiplier;
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, enemyStats.ObstacleJumpHeight, rigidBody.velocity.z);
        enemyStats.JumpsLeft--;

        yield return new WaitForSeconds(enemyStats.ObstacleSpeedMultiplierTime);

        enemyStats.Speed /= enemyStats.ObstacleSpeedMultiplier;
    }

    public void CollisionWithLevel(Collider other)
    {
        if (!enemyStats.ShouldJump) return;
        IsEnemyBelowPoint();
        if (!enemyIsBelowPoint) return;
        if (!other.CompareTag("Level")) return;
        if (enemyStats.JumpsLeft <= 0) return;

        rigidBody.velocity = new Vector3(rigidBody.velocity.x, enemyStats.JumpHeight, rigidBody.velocity.z);
        enemyStats.JumpsLeft--;
    }
    #endregion
}