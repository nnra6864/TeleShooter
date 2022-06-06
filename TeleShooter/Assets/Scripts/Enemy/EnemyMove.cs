using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    #region Events
    public delegate void ChoseNewPoint();
    public event ChoseNewPoint choseNewPoint;
    #endregion

    #region Variables
    EnemyStats enemyStats;
    Rigidbody rigidBody;
    [ReadOnlyField]GameObject target;

    Vector3 defaultScale;

    [HideInInspector] public List<GameObject> points = new List<GameObject>();
    #endregion

    #region Functions
    private void Awake()
    {
        enemyStats = GetComponent<EnemyStats>();
        rigidBody = GetComponent<Rigidbody>();

        defaultScale = this.transform.localScale;
    }

    void Start()
    {
        if (enemyStats.currentRoom < 1) { enemyStats.currentRoom = 1; }
        AddPoints();
        ChooseRandomPoint();
    }

    void Update()
    {
        if (rigidBody.velocity.x >= 0)
        {
            enemyStats.isMovingBackwards = false;
        }
        else
        {
            enemyStats.isMovingBackwards = true;
        }

        if (enemyStats.hasTeleported)
        {
            HasReachedPoint();
            enemyStats.hasTeleported = false;
        }
    }

    private void FixedUpdate()
    {
        if (enemyStats.canMove)
        {
            MoveEnemy();
        }
        else
        {
            rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, rigidBody.velocity.z);
        }
    }
    #endregion

    #region Custom Functions
    public void ChangeRoom(int room) 
    {
        enemyStats.canMove = false;
        enemyStats.shouldChoosePoint = true;
        enemyStats.currentRoom = room;
        AddPoints();
        ChooseRandomPoint();
    }

    public void AddPoints() 
    {
        points.Clear();

        foreach (GameObject p in GameObject.FindGameObjectsWithTag("EnemyWalkPoint"))
        {
            if (p.transform.parent.transform.parent.name == "Room " + enemyStats.currentRoom.ToString())
            {
                points.Add(p);
            }
        }
    }

    public void ChooseRandomPoint() 
    {
        if (enemyStats.shouldChoosePoint)
        {
            enemyStats.pointIndex = Random.Range(0, points.Count);
            target = points[enemyStats.pointIndex];
            enemyStats.randomDistanceFromPoint = Random.Range(enemyStats.randomDistanceFromPointLowerLimit, enemyStats.randomDistanceFromPointUpperLimit);
        }

        enemyStats.shouldChoosePoint = false;
        enemyStats.canMove = true;
    }

    public void ChooseTarget() 
    {
        if (enemyStats.shouldFollowPlayer)
        {
            target = enemyStats.followTargetPlayer;
        }
        else 
        {
            ChooseRandomPoint();
        }
    }

    public void MoveEnemy()
    {
        if (target == null || points[enemyStats.pointIndex] == null) return;

        if (target.transform.position.x > transform.position.x)
        {
            rigidBody.velocity = new Vector3(enemyStats.speed, rigidBody.velocity.y, rigidBody.velocity.z);
        }
        else if (target.transform.position.x < transform.position.x)
        {
            rigidBody.velocity = new Vector3(-enemyStats.speed, rigidBody.velocity.y, rigidBody.velocity.z);
        }

        if (Vector3.Distance(new Vector3(transform.position.x, 0, 0), new Vector3(target.transform.position.x, 0, 0)) < enemyStats.randomDistanceFromPoint)
        {
            StartCoroutine(HasReachedPoint());
        }
    }

    public IEnumerator HasReachedPoint() 
    {
        float cooldown = Random.Range(enemyStats.timeBeforeChoosingPointLowerLimit, enemyStats.timeBeforeChoosingPointUpperLimit);
        if (enemyStats.shouldFollowPlayer) cooldown = 0;

        enemyStats.shouldChoosePoint = true;
        enemyStats.canMove = false;
        enemyStats.canFollowPlayer = false;

        yield return new WaitForSeconds(cooldown);

        ChooseTarget();
        choseNewPoint.Invoke();
    }
    #endregion
}