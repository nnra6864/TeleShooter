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
    public bool canMove;
 
    [Tooltip("Speed of the Horizontal Movement.")]
    public float speed;

    [Tooltip("Determines if a Player can Crouch")]
    public bool canCrouch;

    [Tooltip("Multiplies the Speed when Crouch Key is held.\n(Speed * CrouchSpeedMultiplier)")]
    public float crouchSpeedMultiplier;

    [Tooltip("Determines if a Player can Sprint")]
    public bool canSprint;

    [Tooltip("Multiplies the Speed when Sprint Key is held.\n(Speed * SprintSpeedMultiplier)")]
    public float sprintSpeedMultiplier;

    [Tooltip("Smoothing of the Horizontal Movement.")]
    public float movementSmoothTime;

    [Tooltip("If Disabled, Player won't be able to Teleport.")]
    public bool canTeleport;


    [Header("Jumping")]


    [Tooltip("Determines if a Player can Jump.")]
    public bool canJump;

    [Tooltip("Height of a Player Jump.")]
    public float jumpHeight;

    [Tooltip("Multiplies the Jump Height when Crouch Key is held.\n(JumpHeight * CrouchJumpMultiplier)")]
    public float crouchJumpMultiplier;

    [Tooltip("Multiplies the Jump Height when Sprint Key is held.\n(JumpHeight * SprintJumpMultiplier)")]
    public float sprintJumpMultiplier;

    [Tooltip("Smoothes Jumping.")]
    public float jumpSmoothTime;

    [Tooltip("Number of Jumps a Player can do before having to land on something so he can Jump again.")]
    public int numberOfJumps;


    [Header("Wall Jumping")]


    [Tooltip("Determines if a Player can Wall Jump.")]
    public bool canWallJump;

    [Tooltip("Determines how far the Player will be launched on the opposite side of the wall he was doing a Wall Jump on.")]
    public float wallJumpForce;

    [Tooltip("Multiplies the Wall Jump Force when Crouch Key is held.\n(WallJumpForce * CrouchWallJumpForceMultiplier)")]
    public float crouchWallJumpForceMultiplier;

    [Tooltip("Multiplies the Wall Jump Force when Sprint Key is held.\n(WallJumpForce * SprintWallJumpForceMultiplier)")]
    public float sprintWallJumpForceMultiplier;

    [Tooltip("Determines how high the Player will be launched when doing a Wall Jump.")]
    public float wallJumpHeight;

    [Tooltip("Multiplies the Wall Jump Height when Crouch Key is held.\n(WallJumpHeight * CrouchWallJumpHeightMultiplier)")]
    public float crouchWallJumpHeightMultiplier;

    [Tooltip("Multiplies the Wall Jump Height when Sprint Key is held.\n(WallJumpHeight * SprintWallJumpHeightMultiplier)")]
    public float sprintWallJumpHeightMultiplier;

    [Tooltip("Smoothes Wall Jumping.")]
    public float wallJumpSmoothTime;


    [Header("Combat")]


    [Tooltip("Determines if a Player can take Damage.")]
    public bool canTakeDamage;

    [Tooltip("Determines if a Player can perform a Melee Attack.")]
    public bool canAttack;

    [Tooltip("Determines if a Player can Shoot.")]
    public bool canShoot;

    [Tooltip("Player's Health.")]
    public float health;

    [Tooltip("Multiplies all Damage Taken.\n(Damage * ResistanceMultiplier)")]
    public float resistanceMultiplier;

    [Tooltip("Ammount of Damage a Player will do when Attacking.\nThis doesn't affect Weapon's Damage.")]
    public float damage;

    [Tooltip("Multiplies Damage.\n(Damage * DamageMultiplier)")]
    public float damageMultiplier;

    [Tooltip("Length of Player Death Animation in Seconds.")]
    public int dieAnimationLength;
    #endregion



    [Header("Read Only Variables", order = 0)]
    #region Read Only Variables
    [Header("Movement", order = 1)]


    [Tooltip("Determines if Player is Looking Backwards.")]
    [ReadOnlyField] public bool isBackwards;

    [Tooltip("Determines if Player is Moving Backwards.")]
    [ReadOnlyField] public bool isMovingBackwards;

    [Tooltip("Determines if Player is Crouching.")]
    [ReadOnlyField] public bool isCrouching;

    [Tooltip("Determines if Player is Sprintint.")]
    [ReadOnlyField] public bool isSprinting;

    [Tooltip("Speed with all Modifiers applied to it.")]
    [ReadOnlyField] public float modifiedSpeed;


    [Header("Jumping")]


    [Tooltip("Number of Jumps Left for Player to use.")]
    [ReadOnlyField] public int jumpsLeft;

    [Tooltip("Jump Height with all Modifiers applied to it.")]
    [ReadOnlyField] public float modifiedJumpHeight;


    [Header("Wall Jumping")]


    [Tooltip("Wall Jump Force with all Modifiers applied to it.")]
    [ReadOnlyField] public float modifiedWallJumpForce;

    [Tooltip("Wall Jump Height with all Modifiers applied to it.")]
    [ReadOnlyField] public float modifiedWallJumpHeight;


    [Header("Progress")]


    [Tooltip("Amount of Kills in this session.")]
    [ReadOnlyField] public int totalKills;

    [Tooltip("Amount of Damage Given in this session.")]
    [ReadOnlyField] public float totalDamageGiven;

    [Tooltip("Amount of Damage Taken in this session.")]
    [ReadOnlyField] public float totalDamageTaken;

    [Tooltip("Amount of Bullets Fired in this session.")]
    [ReadOnlyField] public int totalBulletsFired;

    [Tooltip("Amount of Melee Attacks in this session.")]
    [ReadOnlyField] public int totalMeleeAttacks;

    [Tooltip("Amount of Money earned in this session.")]
    [ReadOnlyField] public int totalMoney;

    [Tooltip("Number of Jumps done in this session.")]
    [ReadOnlyField] public int totalJumps;

    [Tooltip("Number of Wall Jumps done in this session.")]
    [ReadOnlyField] public int totalWallJumps;

    [Tooltip("Number of Teleportations done in this session.")]
    [ReadOnlyField] public int totalTeleportations;


    [Header("General")]


    [Tooltip("Name of the last Portal Player interacted with.")]
    [ReadOnlyField] public string lastPortalName;

    [Tooltip("Determines if Player is Taking Damage from Level.")]
    [ReadOnlyField] public bool isTakingDamageFromLevel;

    [Tooltip("Determines if Player is Taking Damage from Enemy.")]
    [ReadOnlyField] public bool isTakingDamageFromEnemy;

    [Tooltip("Name of the object that Caused Death of Player.")]
    [ReadOnlyField] public string causeOfDeath;

    [Tooltip("Index of the Room Player is located in.")]
    [ReadOnlyField] public int currentRoom;
    #endregion
}