using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class PlayerStats : MonoBehaviour
{
    [Header("Editable Variables", order = 0)]
    #region Editable Variables
    [Header("Movement", order = 1)]


    [Tooltip("Determines if a Player can Move")]
    public bool CanMove;
 
    [Tooltip("Speed of the Horizontal Movement.")]
    public float Speed;

    [Tooltip("Determines if a Player can Crouch")]
    public bool CanCrouch;

    [Tooltip("Multiplies the Speed when Crouch Key is held.\n(Speed * CrouchSpeedMultiplier)")]
    public float CrouchSpeedMultiplier;

    [Tooltip("Determines if a Player can Sprint")]
    public bool CanSprint;

    [Tooltip("If Disabled, Player won't be able to Teleport.")]
    public bool CanTeleport;

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


    [Tooltip("Determines if a Player can take Damage.")]
    public bool CanTakeDamage;

    [Tooltip("Determines if a Player can perform a Melee Attack.")]
    public bool CanAttack;

    [Tooltip("Determines if a Player can Shoot.")]
    public bool CanShoot;

    [Tooltip("Player's Health.")]
    public float Health;

    [Tooltip("Multiplies all Damage Taken.\n(Damage * ResistanceMultiplier)")]
    public float ResistanceMultiplier;

    [Tooltip("Ammount of Damage a Player will do when Attacking.\nThis doesn't affect Weapon's Damage.")]
    public float Damage;

    [Tooltip("Multiplies Damage.\n(Damage * DamageMultiplier)")]
    public float DamageMultiplier;

    [Tooltip("Length of Player Death Animation in Seconds.")]
    public int DieAnimationLength;
    #endregion



    [Header("Read Only Variables", order = 0)]
    #region Read Only Variables
    [Header("Movement", order = 1)]


    [Tooltip("Determines if Player is Looking Backwards.")]
    [ReadOnlyField] public bool IsBackwards;

    [Tooltip("Determines if Player is Moving Backwards.")]
    [ReadOnlyField] public bool IsMovingBackwards;

    [Tooltip("Determines if Player is Crouching.")]
    [ReadOnlyField] public bool IsCrouching;

    [Tooltip("Determines if Player is Sprintint.")]
    [ReadOnlyField] public bool IsSprinting;

    [Tooltip("Speed with all Modifiers applied to it.")]
    [ReadOnlyField] public float ModifiedSpeed;


    [Header("Jumping")]


    [Tooltip("Number of Jumps Left for Player to use.")]
    [ReadOnlyField] public int JumpsLeft;

    [Tooltip("Jump Height with all Modifiers applied to it.")]
    [ReadOnlyField] public float ModifiedJumpHeight;


    [Header("Wall Jumping")]


    [Tooltip("Wall Jump Force with all Modifiers applied to it.")]
    [ReadOnlyField] public float ModifiedWallJumpForce;

    [Tooltip("Wall Jump Height with all Modifiers applied to it.")]
    [ReadOnlyField] public float ModifiedWallJumpHeight;


    [Header("Progress")]


    [Tooltip("Amount of Kills in this session.")]
    [ReadOnlyField] public int TotalKills;

    [Tooltip("Amount of Damage Given in this session.")]
    [ReadOnlyField] public float TotalDamageGiven;

    [Tooltip("Amount of Damage Taken in this session.")]
    [ReadOnlyField] public float TotalDamageTaken;

    [Tooltip("Amount of Bullets Fired in this session.")]
    [ReadOnlyField] public int TotalBulletsFired;

    [Tooltip("Amount of Melee Attacks in this session.")]
    [ReadOnlyField] public int TotalMeleeAttacks;

    [Tooltip("Amount of Money earned in this session.")]
    [ReadOnlyField] public int TotalMoney;

    [Tooltip("Number of Jumps done in this session.")]
    [ReadOnlyField] public int TotalJumps;

    [Tooltip("Number of Wall Jumps done in this session.")]
    [ReadOnlyField] public int TotalWallJumps;

    [Tooltip("Number of Teleportations done in this session.")]
    [ReadOnlyField] public int TotalTeleportations;


    [Header("General")]


    [Tooltip("Name of the last Portal Player interacted with.")]
    [ReadOnlyField] public string LastPortalName;

    [Tooltip("Determines if Player is Taking Damage from Level.")]
    [ReadOnlyField] public bool IsTakingDamageFromLevel;

    [Tooltip("Determines if Player is Taking Damage from Enemy.")]
    [ReadOnlyField] public bool IsTakingDamageFromEnemy;

    [Tooltip("Name of the object that Caused Death of Player.")]
    [ReadOnlyField] public string CauseOfDeath;

    [Tooltip("Index of the Room Player is located in.")]
    [ReadOnlyField] public int CurrentRoom;
    #endregion
}