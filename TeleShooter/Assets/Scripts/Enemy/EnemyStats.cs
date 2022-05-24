using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Editable Variables", order = 0)]
    #region Visible Variables
    [Header("Movement", order = 1)]


    [Tooltip("Enemy won't be able to Move if this is Disabled.")]
    public bool CanMove;

    [Tooltip("Enemy's Speed.")]
    public float Speed;

    [Tooltip("If Enemy Collides with an Obstacle his Speed will be Multiplied with ObstacleSpeedMutliplier.\n(Speed * ObstacleSpeedMultiplier)")]
    public float ObstacleSpeedMultiplier;

    [Tooltip("This variable determines for how long ObstacleSpeedMultiplier will last.\n(Time in Seconds)")]
    public float ObstacleSpeedMultiplierTime;

    [Tooltip("Minimum Distance from Point that Enemy has to reach before stopping.")]
    public float RandomDistanceFromPointLowerLimit;

    [Tooltip("Maximum Distance from Point that Enemy has to reach before stopping.")]
    public float RandomDistanceFromPointUpperLimit;

    [Tooltip("Minimum Time that has to pass before Enemy can choose a new Point.\n(Time in Seconds)")]
    public float TimeBeforeChoosingPointLowerLimit;

    [Tooltip("Maximum Time that has to pass before Enemy can choose a new Point.\n(Time in Seconds)")]
    public float TimeBeforeChoosingPointUpperLimit;

    [Tooltip("If Enemy's position stays the same for StuckTime ammount of Time he will change his Destination.\n(Time in Seconds)")]
    public float StuckTime;


    [Header("Jumping")]


    [Tooltip("Determines if Enemy can Jump.")]
    public bool CanJump;

    [Tooltip("Height of a Jump.")]
    public float JumpHeight;

    [Tooltip("Height of a Jump over Obstacle.")]
    public float ObstacleJumpHeight;

    [Tooltip("Number of Jumps an Enemy can do before having to land on something so he can Jump again.")]
    public int NumberOfJumps;


    [Header("Combat")]


    [Tooltip("Determines if an Enemy can perform a Melee Attack.")]
    public bool CanAttack;

    [Tooltip("Determines if an Enemy can Shoot.")]
    public bool CanShoot;

    [Tooltip("Determines if a Player can take Damage.")]
    public bool CanTakeDamage;

    [Tooltip("Enemy's Health.")]
    public float Health;

    [Tooltip("Multiplies all Damage Taken.\n(Damage * ResistanceMultiplier)")]
    public float ResistanceMultiplier;

    [Tooltip("Ammount of Damage an Enemy will do when Attacking.\nThis doesn't affect Weapon's Damage.")]
    public float Damage;

    [Tooltip("Multiplies Damage.\n(Damage * DamageMultiplier)")]
    public float DamageMultiplier;


    [Header("General")]


    [Tooltip("Length of Player Death Animation in Seconds.")]
    public int DieAnimationLength;
   
    [Tooltip("If Disabled, Enemy won't be able to Teleport.")]
    public bool CanTeleport;
    #endregion



    [Header("Read Only Variables", order = 0)]
    #region Hidden Variables
    [Header("Movement", order = 1)]


    [Tooltip("Determines if Enemy is Moving.")]
    [ReadOnlyField] public bool IsMoving = false;

    [Tooltip("Determines if Enemy is Looking Backwards.")]
    [ReadOnlyField] public bool IsBackwards;

    [Tooltip("Determines if Enemy is Moving Backwards.")]
    [ReadOnlyField] public bool IsMovingBackwards;

    [Tooltip("Speed with all Modifiers applied to it.")]
    [ReadOnlyField] public float ModifiedSpeed;

    [Tooltip("Name of the Last Portal an Enemy interacted with.")]
    [ReadOnlyField] public string LastPortalName = "";

    
    [Header("Jump")]


    [Tooltip("Determines if an Enemy is Grounded.")]
    [ReadOnlyField] public bool IsGrounded;

    [Tooltip("Number of Jumps left for Enemy to use.")]
    [ReadOnlyField] public int JumpsLeft;

    [Tooltip("Determines if Enemy should Jump.")]
    [ReadOnlyField] public bool ShouldJump;


    [Header("Combat")]


    [Tooltip("Determines if Enemy is taking Damage from Level.")]
    [ReadOnlyField] public bool IsTakingDamageFromLevel;

    [Tooltip("Determines if Enemy is taking Damage from Player.")]
    [ReadOnlyField] public bool IsTakingDamageFromPlayer;


    [Header("Enemy Move")]


    [Tooltip("Index of the Room Enemy is located in.")]
    [ReadOnlyField] public int CurrentRoom;

    [Tooltip("Determines if an Enemy has Teleported.")]
    [ReadOnlyField] public bool HasTeleported = false;

    [Tooltip("Random Distance from Enemy Walk Point that Enemy needs to reach.")]
    [ReadOnlyField] public float RandomDistanceFromPoint = 1;

    [Tooltip("Determines if an Enemy should Choose new Walk Point.")]
    [ReadOnlyField] public bool ShouldChoosePoint = true;

    [Tooltip("Index of a Point Enemy chose.")]
    [ReadOnlyField] public int PointIndex = 1;
    #endregion
}
