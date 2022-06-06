using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Editable Variables", order = 0)]
    #region Editable Variables
    [Header("Movement", order = 1)]


    [Tooltip("Enemy won't be able to Move if this is Disabled.")]
    public bool canMove;

    [Tooltip("Enemy will start following a player if this is enabled.")]
    public bool canFollowPlayer;

    [Tooltip("Enemy's Speed.")]
    public float speed;

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


    [Header("Jumping")]


    [Tooltip("Determines if Enemy can Jump.")]
    public bool canJump;

    [Tooltip("Height of a Jump.")]
    public float jumpHeight;

    [Tooltip("Height of a Jump over Obstacle.")]
    public float obstacleJumpHeight;

    [Tooltip("Number of Jumps an Enemy can do before having to land on something so he can Jump again.")]
    public int numberOfJumps;


    [Header("Combat")]


    [Tooltip("Determines if an Enemy can perform a Melee Attack.")]
    public bool canAttack;

    [Tooltip("Determines if an Enemy can Shoot.")]
    public bool canShoot;

    [Tooltip("Determines if a Player can take Damage.")]
    public bool canTakeDamage;

    [Tooltip("Enemy's Health.")]
    public float health;

    [Tooltip("Multiplies all Damage Taken.\n(Damage * ResistanceMultiplier)")]
    public float resistanceMultiplier;

    [Tooltip("Ammount of Damage an Enemy will do when Attacking.\nThis doesn't affect Weapon's Damage.")]
    public float damage;

    [Tooltip("Multiplies Damage.\n(Damage * DamageMultiplier)")]
    public float damageMultiplier;


    [Header("General")]


    [Tooltip("Length of Player Death Animation in Seconds.")]
    public int dieAnimationLength;
   
    [Tooltip("If Disabled, Enemy won't be able to Teleport.")]
    public bool canTeleport;
    #endregion



    [Header("Read Only Variables", order = 0)]
    #region Hidden Variables
    [Header("Movement", order = 1)]


    [Tooltip("Determines if Enemy is Moving.")]
    [ReadOnlyField] public bool isMoving = false;

    [Tooltip("Determines if Enemy is Looking Backwards.")]
    [ReadOnlyField] public bool isBackwards;

    [Tooltip("Determines if Enemy is Moving Backwards.")]
    [ReadOnlyField] public bool isMovingBackwards;

    [Tooltip("Speed with all Modifiers applied to it.")]
    [ReadOnlyField] public float modifiedSpeed;

    [Tooltip("Name of the Last Portal an Enemy interacted with.")]
    [ReadOnlyField] public string lastPortalName = "";

    
    [Header("Jump")]


    [Tooltip("Determines if an Enemy is Grounded.")]
    [ReadOnlyField] public bool isGrounded;

    [Tooltip("Number of Jumps left for Enemy to use.")]
    [ReadOnlyField] public int jumpsLeft;

    [Tooltip("Determines if Enemy should Jump.")]
    [ReadOnlyField] public bool shouldJump;


    [Header("Combat")]


    [Tooltip("Determines if Enemy is taking Damage from Level.")]
    [ReadOnlyField] public bool isTakingDamageFromLevel;

    [Tooltip("Determines if Enemy is taking Damage from Player.")]
    [ReadOnlyField] public bool isTakingDamageFromPlayer;


    [Header("Enemy Move")]


    [Tooltip("Determines if Enemy should Follow the Player.")]
    [ReadOnlyField] public bool shouldFollowPlayer;

    [Tooltip("Distance between Enemy and Player.")]
    [ReadOnlyField] public float distanceFromPlayer;

    [Tooltip("Player that Enemy is following.")]
    [ReadOnlyField] public GameObject followTargetPlayer;

    [Tooltip("Index of the Room Enemy is located in.")]
    [ReadOnlyField] public int currentRoom;

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
