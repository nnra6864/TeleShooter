using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    #region Visible Variables
    [Header("Health")]


    [Tooltip("Determines if a Player can take Damage.")]
    public bool CanTakeDamage;

    [Tooltip("Enemy's Health.")]
    public float Health;

    [Tooltip("Multiplies all Damage Taken.\n(Damage * ResistanceMultiplier)")]
    public float ResistanceMultiplier;


    [Header("Movement")]


    [Tooltip("Enemy won't be able to Move if this is Disabled.")]
    public bool CanMove;

    [Tooltip("Enemy's Speed.")]
    public float Speed;

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

    [Tooltip("If Enemy Collides with an Obstacle his Speed will be Multiplied with ObstacleSpeedMutliplier.\n(Speed * ObstacleSpeedMultiplier)")]
    public float ObstacleSpeedMultiplier;

    [Tooltip("This variable determines for how long ObstacleSpeedMultiplier will last.\n(Time in Seconds)")]
    public float ObstacleSpeedMultiplierTime;


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

    #region Hidden Variables
    //Health
    [HideInInspector] public bool IsTakingDamageFromLevel;
    
    [HideInInspector] public bool IsTakingDamageFromPlayer;

    //Movement
    [HideInInspector] public bool IsMoving = false;

    [HideInInspector] public bool IsBackwards;
    
    [HideInInspector] public bool IsMovingBackwards;

    [HideInInspector] public float ModifiedSpeed;

    [HideInInspector] public float UnmodifiedSpeed;

    //Jump
    [HideInInspector] public bool IsGrounded;
    
    [HideInInspector] public int JumpsLeft;

    [Tooltip("Determines if Enemy can Jump.")]
    [HideInInspector]public bool ShouldJump;

    //Other
    [HideInInspector] public string LastPortalName = "";

    //Enemy Move
    [HideInInspector] public int CurrentRoom;
    
    [HideInInspector] public bool HasTeleported = false;
    
    [HideInInspector] public float RandomDistanceFromPoint = 1;
    
    [HideInInspector] public bool ShouldChoosePoint = true;
    
    [HideInInspector] public int PointIndex = 1;
    #endregion

    #region Functions
    [ExecuteAlways]
    private void Awake()
    {
        UnmodifiedSpeed = Speed;
    }
    #endregion
}
