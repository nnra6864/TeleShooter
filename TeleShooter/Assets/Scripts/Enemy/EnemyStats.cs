using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Health")]


    [Tooltip("Determines if a Player can take Damage.")]
    public bool CanTakeDamage;

    [Tooltip("Enemy's Health.")]
    public float Health;

    [Tooltip("Multiplies all Damage Taken.\n(Damage * ResistanceMultiplier)")]
    public float ResistanceMultiplier;


    [Header("Movement")]


    [Tooltip("Enemy won't be able to Move if this is Disabled..")]
    public bool CanMove = false;

    [Tooltip("Enemy's Speed.")]
    public float Speed;

    [Tooltip("Minimum Distance from Point that Enemy has to reach before stopping.")]
    public float RandomDistanceFromPointLowerLimit;

    [Tooltip("Maximum Distance from Point that Enemy has to reach before stopping.")]
    public float RandomDistanceFromPointUpperLimit;

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

    [Tooltip("Ammount of Damage an Enemy will do when Attacking.\nThis doesn't affect Weapon's Damage.")]
    public float Damage;

    [Tooltip("Multiplies Damage.\n(Damage * DamageMultiplier)")]
    public float DamageMultiplier;


    [Header("General")]


    [Tooltip("Length of Player Death Animation in Seconds.")]
    public int DieAnimationLength;
   
    [Tooltip("If Disabled, Enemy won't be able to Teleport.")]
    public bool CanTeleport;

    #region Hidden Variables
    //Health
    [HideInInspector] public bool IsTakingDamageFromLevel;
    [HideInInspector] public bool IsTakingDamageFromPlayer;

    //Movement
    [HideInInspector] public bool IsBackwards;
    [HideInInspector] public bool IsMoving = false;

    //Jump
    [HideInInspector] public bool IsGrounded;
    [HideInInspector] public int JumpsLeft;
    
    //Other
    [HideInInspector] public string LastPortalName = "";

    //Enemy Move
    [HideInInspector] public int CurrentRoom;
    [HideInInspector] public bool HasTeleported = false;
    [HideInInspector] public float RandomDistanceFromPoint = 1;
    [HideInInspector] public bool ShouldChoosePoint = true;
    [HideInInspector] public int PointIndex = 1;
    #endregion
}
