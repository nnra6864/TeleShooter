using UnityEngine;

public class EnemyStats : Stats
{
    [Header("Enemy Class Editable Variables", order = 0)]
    #region Editable Variables
    [Header("Movement", order = 1)]


    [Tooltip("Enemy will start following a player if this is enabled.")]
    public bool canFollowPlayer;

    [Tooltip("If Enemy Collides with an Obstacle his Speed will be Multiplied with ObstacleSpeedMutliplier.\n(Speed * ObstacleSpeedMultiplier)")]
    public float obstacleSpeedMultiplier;

    [Tooltip("This variable determines for how long ObstacleSpeedMultiplier will last.\n(Time in Seconds)")]
    public float obstacleSpeedMultiplierTime;

    [Tooltip("Minimum Distance from Point that Enemy has to reach before stopping.")]
    public float randomDistanceFromPointLowerLimit;

    [Tooltip("Maximum Distance from Point that Enemy has to reach before stopping.")]
    public float randomDistanceFromPointUpperLimit;

    [Tooltip("Minimum Time that has to pass before Enemy can choose a new Point.\n(Time in Seconds)")]
    public float timeBeforeChoosingPointLowerLimit;

    [Tooltip("Maximum Time that has to pass before Enemy can choose a new Point.\n(Time in Seconds)")]
    public float timeBeforeChoosingPointUpperLimit;

    [Tooltip("If Enemy's position stays the same for StuckTime ammount of Time he will change his Destination.\n(Time in Seconds)")]
    public float stuckTime;

    [Tooltip("Distance at which Enemy will start following Player")]
    public float distanceFromPlayerToStartFollowing;

    [Tooltip("Distance at which Enemy will stop following Player")]
    public float distanceFromPlayerToStopFollowing;


    [Header("Jumping", order = 1)]


    [Tooltip("Height of a Jump over Obstacle.")]
    public float obstacleJumpHeight;
    #endregion



    [Header("Enemy Class Read Only Variables", order = 0)]
    #region Hidden Variables
    //[Header("Movement", order = 1)]

    
    [Header("Jumping", order = 1)]


    [Tooltip("Determines if Enemy should Jump.")]
    [ReadOnlyField] public bool shouldJump;


    //[Header("Combat", order = 1)]


    [Header("Enemy Move", order = 1)]


    [Tooltip("Determines if Enemy should Follow the Player.")]
    [ReadOnlyField] public bool shouldFollowPlayer;

    [Tooltip("Distance between Enemy and Player.")]
    [ReadOnlyField] public float distanceFromPlayer;

    [Tooltip("Player that Enemy is following.")]
    [ReadOnlyField] public GameObject followTargetPlayer;

    [Tooltip("Determines if an Enemy has Teleported.")]
    [ReadOnlyField] public bool hasTeleported = false;

    [Tooltip("Random Distance from Enemy Walk Point that Enemy needs to reach.")]
    [ReadOnlyField] public float randomDistanceFromPoint = 1;

    [Tooltip("Determines if an Enemy should Choose new Walk Point.")]
    [ReadOnlyField] public bool shouldChoosePoint = true;

    [Tooltip("Index of a Point Enemy chose.")]
    [ReadOnlyField] public int pointIndex = 1;
    #endregion
}
