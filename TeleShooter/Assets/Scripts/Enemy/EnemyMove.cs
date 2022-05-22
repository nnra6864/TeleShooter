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
        if (enemyStats.CurrentRoom < 1) { enemyStats.CurrentRoom = 1; }
        AddPoints();
        ChooseRandomPoint();
    }

    void Update()
    {
        if (rigidBody.velocity.x >= 0)
        {
            enemyStats.IsBackwards = false;
            this.transform.localScale = new Vector3(defaultScale.x, this.transform.localScale.y, this.transform.localScale.z);
        }
        else
        {
            enemyStats.IsBackwards = true;
            this.transform.localScale = new Vector3(-defaultScale.x, this.transform.localScale.y, this.transform.localScale.z);
        }

        if (enemyStats.HasTeleported)
        {
            HasReachedPoint();
            enemyStats.HasTeleported = false;
        }
    }

    private void FixedUpdate()
    {
        if (enemyStats.CanMove)
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
        enemyStats.CanMove = false;
        enemyStats.ShouldChoosePoint = true;
        enemyStats.CurrentRoom = room;
        AddPoints();
        ChooseRandomPoint();
    }

    public void AddPoints() 
    {
        points.Clear();

        foreach (GameObject p in GameObject.FindGameObjectsWithTag("EnemyWalkPoint"))
        {
            if (p.transform.parent.transform.parent.name == "Room " + enemyStats.CurrentRoom.ToString())
            {
                points.Add(p);
            }
        }
    }

    public void ChooseRandomPoint() 
    {
        if (enemyStats.ShouldChoosePoint)
        {
            enemyStats.PointIndex = Random.Range(0, points.Count);
            enemyStats.RandomDistanceFromPoint = Random.Range(enemyStats.RandomDistanceFromPointLowerLimit, enemyStats.RandomDistanceFromPointUpperLimit);
        }

        enemyStats.ShouldChoosePoint = false;
        enemyStats.CanMove = true;
    }

    public void MoveEnemy()
    {
        if (points.Count > 0)
        {
            if (points[enemyStats.PointIndex].transform.position.x > transform.position.x)
            {
                rigidBody.velocity = new Vector3(enemyStats.Speed, rigidBody.velocity.y, rigidBody.velocity.z);
            }
            else if (points[enemyStats.PointIndex].transform.position.x < transform.position.x)
            {
                rigidBody.velocity = new Vector3(-enemyStats.Speed, rigidBody.velocity.y, rigidBody.velocity.z);
            }

            if (Vector3.Distance(new Vector3(transform.position.x, 0, 0), new Vector3(points[enemyStats.PointIndex].transform.position.x, 0, 0)) < enemyStats.RandomDistanceFromPoint)
            {
                StartCoroutine(HasReachedPoint());
            }
        }
    }

    public IEnumerator HasReachedPoint() 
    {
        float cooldown = Random.Range(enemyStats.TimeBeforeChoosingPointLowerLimit, enemyStats.TimeBeforeChoosingPointUpperLimit);
        enemyStats.ShouldChoosePoint = true;
        enemyStats.CanMove = false;
        yield return new WaitForSeconds(cooldown);
        ChooseRandomPoint();
        choseNewPoint.Invoke();
    }
    #endregion
}