using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Shown Variables
    [Header("Health")]


    [Tooltip("Determines if a Player can take Damage.")]
    public bool CanTakeDamage;

    [Tooltip("Player's Health.")]
    public float Health;

    [Tooltip("Multiplies all Damage Taken.\n(Damage * ResistanceMultiplier)")]
    public float ResistanceMultiplier;


    [Header("Movement")]


    [Tooltip("Determines if a Player can Move")]
    public bool CanMove;

    [Tooltip("Determines if a Player can Crouch")]
    public bool CanCrouch;

    [Tooltip("Determines if a Player can Sprint")]
    public bool CanSprint;

    [Tooltip("Speed of the Horizontal Movement.")]
    public float Speed;

    [Tooltip("Multiplies the Speed when Crouch Key is held.\n(Speed * CrouchSpeedMultiplier)")]
    public float CrouchSpeedMultiplier;

    [Tooltip("Multiplies the Speed when Sprint Key is held.\n(Speed * SprintSpeedMultiplier)")]
    public float SprintSpeedMultiplier;

    [Tooltip("Smoothing of the Horizontal Movement.")]
    public float MovementSmoothTime;


    [Header("Jumping")]


    [Tooltip("Determines if a Player can Jump.")]
    public bool CanJump;

    [Tooltip("Height of a Player Jump.")]
    public float JumpHeight;

    [Tooltip("Multiplies the Jump Height when Crouch Key is held.\n(JumpHeight * CrouchJumpMultiplier)")]
    public float CrouchJumpMultiplier;

    [Tooltip("Multiplies the Jump Height when Sprint Key is held.\n(JumpHeight * SprintJumpMultiplier)")]
    public float SprintJumpMultiplier;

    [Tooltip("Smoothes Jumping.")]
    public float JumpSmoothTime;

    [Tooltip("Number of Jumps a Player can do before having to land on something so he can Jump again.")]
    public int NumberOfJumps;


    [Header("Wall Jumping")]


    [Tooltip("Determines if a Player can Wall Jump.")]
    public bool CanWallJump;

    [Tooltip("Determines how far the Player will be launched on the opposite side of the wall he was doing a Wall Jump on.")]
    public float WallJumpForce;

    [Tooltip("Multiplies the Wall Jump Force when Crouch Key is held.\n(WallJumpForce * CrouchWallJumpForceMultiplier)")]
    public float CrouchWallJumpForceMultiplier;

    [Tooltip("Multiplies the Wall Jump Force when Sprint Key is held.\n(WallJumpForce * SprintWallJumpForceMultiplier)")]
    public float SprintWallJumpForceMultiplier;

    [Tooltip("Determines how high the Player will be launched when doing a Wall Jump.")]
    public float WallJumpHeight;

    [Tooltip("Multiplies the Wall Jump Height when Crouch Key is held.\n(WallJumpHeight * CrouchWallJumpHeightMultiplier)")]
    public float CrouchWallJumpHeightMultiplier;

    [Tooltip("Multiplies the Wall Jump Height when Sprint Key is held.\n(WallJumpHeight * SprintWallJumpHeightMultiplier)")]
    public float SprintWallJumpHeightMultiplier;

    [Tooltip("Smoothes Wall Jumping.")]
    public float WallJumpSmoothTime;


    [Header("Combat")]

    [Tooltip("Determines if a Player can perform a Melee Attack.")]
    public bool CanAttack;

    [Tooltip("Determines if a Player can Shoot.")]
    public bool CanShoot;

    [Tooltip("Ammount of Damage a Player will do when Attacking.\nThis doesn't affect Weapon's Damage.")]
    public float Damage;

    [Tooltip("Multiplies Damage.\n(Damage * DamageMultiplier)")]
    public float DamageMultiplier;


    [Header("General")]


    [Tooltip("Length of Player Death Animation in Seconds.")]
    public int DieAnimationLength;

    [Tooltip("If Disabled, Player won't be able to Teleport.")]
    public bool CanTeleport;
    #endregion

    #region Hidden Variables
    //Movement
    [HideInInspector]
    public bool IsBackwards;

    [HideInInspector]
    public bool IsMovingBackwards;

    [HideInInspector]
    public bool IsCrouching;

    [HideInInspector]
    public bool IsSprinting;

    [HideInInspector]
    public float ModifiedSpeed;


    //Jumping
    [HideInInspector]
    public int JumpsLeft;

    [HideInInspector]
    public float ModifiedJumpHeight;


    //Wall Jumping
    [HideInInspector]
    public float ModifiedWallJumpForce;

    [HideInInspector]
    public float ModifiedWallJumpHeight;


    //Progress
    [HideInInspector]
    public int TotalKills;

    [HideInInspector]
    public float TotalDamageGiven;

    [HideInInspector]
    public float TotalDamageTaken;

    [HideInInspector]
    public int TotalBulletsFired;

    [HideInInspector]
    public int TotalMeleeAttacks;

    [HideInInspector]
    public int TotalMoney;

    [HideInInspector]
    public int TotalJumps;

    [HideInInspector]
    public int TotalWallJumps;

    [HideInInspector]
    public int TotalTeleportations;


    //Other
    [HideInInspector]
    public string LastPortalName;

    [HideInInspector]
    public bool IsTakingDamageFromLevel;

    [HideInInspector]
    public bool IsTakingDamageFromEnemy;

    [HideInInspector]
    public string CauseOfDeath;

    [HideInInspector]
    public int CurrentRoom;
    #endregion
}