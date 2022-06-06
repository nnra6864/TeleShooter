using System.Collections;
using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    EnemyStats enemyStats;
    EnemyMove enemyMove;
    Rigidbody rigidBody;
    Vector3 startingPosition;

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
        startingPosition = transform.localPosition;
        enemyStats.shouldJump = true;
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

    private void Update()
    {
        transform.localPosition = enemyStats.isMovingBackwards ? new Vector3(-startingPosition.x, startingPosition.y) : new Vector3(startingPosition.x, startingPosition.y);
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
        enemyIsBelowPoint = enemyMove.transform.position.y < enemyMove.points[enemyStats.pointIndex].transform.position.y;
    }

    public IEnumerator CollisionWithObstacle(Collider other)
    {
        if (!other.CompareTag("Obstacle")) yield break;
        if (enemyStats.jumpsLeft <= 0) yield break;

        enemyStats.speed *= enemyStats.obstacleSpeedMultiplier;
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, enemyStats.obstacleJumpHeight, rigidBody.velocity.z);
        enemyStats.jumpsLeft--;

        yield return new WaitForSeconds(enemyStats.obstacleSpeedMultiplierTime);

        enemyStats.speed /= enemyStats.obstacleSpeedMultiplier;
    }

    public void CollisionWithLevel(Collider other)
    {
        if (!enemyStats.shouldJump) return;
        IsEnemyBelowPoint();
        if (!enemyIsBelowPoint) return;
        if (!other.CompareTag("Level")) return;
        if (enemyStats.jumpsLeft <= 0) return;

        rigidBody.velocity = new Vector3(rigidBody.velocity.x, enemyStats.jumpHeight, rigidBody.velocity.z);
        enemyStats.jumpsLeft--;
    }
    #endregion
}