using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("Base Class Editable Variables", order = 0)]
    #region Editable Variables
    [Header("Movement", order = 1)]


    [Tooltip("Determines if Entity can Move")]
    public bool canMove;

    [Tooltip("Speed of the Horizontal Movement.")]
    public float speed;

    [Tooltip("If Disabled, Entity won't be able to Teleport.")]
    public bool canTeleport;

    [Tooltip("Determines if Entity can Crouch")]
    public bool canCrouch;

    [Tooltip("Multiplies the Speed when Crouch Key is held.\n(Speed * CrouchSpeedMultiplier)")]
    public float crouchSpeedMultiplier;

    [Tooltip("Determines if Entity can Sprint")]
    public bool canSprint;

    [Tooltip("Multiplies the Speed when Sprint Key is held.\n(Speed * SprintSpeedMultiplier)")]
    public float sprintSpeedMultiplier;

    [Tooltip("Smoothing of the Horizontal Movement.")]
    public float movementSmoothTime;


    [Header("Jumping", order = 1)]


    [Tooltip("Determines if CustomGravity script will be used.")]
    public bool useCustomGravity;

    [Tooltip("Multiplised gravity.\nCustomGravity must be used.")]
    public float gravityMultiplier;

    [Tooltip("Adds force to a falling RigidBody.")]
    public bool useFallMultiplier;

    [Tooltip("Strength of a Fall Multiplier.")]
    public float fallMultiplier;

    [Tooltip("Determines if Entity can Jump.")]
    public bool canJump;

    [Tooltip("Jump Heigh of an Entity.")]
    public float jumpHeight;

    [Tooltip("Number of Jumps Entity can do before having to land on something so it can Jump again.")]
    public int numberOfJumps;

    [Tooltip("Multiplies the Jump Height when Crouch Key is held.\n(JumpHeight * CrouchJumpMultiplier)")]
    public float crouchJumpMultiplier;

    [Tooltip("Multiplies the Jump Height when Sprint Key is held.\n(JumpHeight * SprintJumpMultiplier)")]
    public float sprintJumpMultiplier;

    [Tooltip("Smoothes Jumping.")]
    public float jumpSmoothTime;


    [Header("Wall Jumping", order = 1)]


    [Tooltip("Determines if Entity can Wall Jump.")]
    public bool canWallJump;

    [Tooltip("Determines how far Entity will be launched on the opposite side of the wall he was doing a Wall Jump on.")]
    public float wallJumpForce;

    [Tooltip("Multiplies the Wall Jump Force when Crouch Key is held.\n(WallJumpForce * CrouchWallJumpForceMultiplier)")]
    public float crouchWallJumpForceMultiplier;

    [Tooltip("Multiplies the Wall Jump Force when Sprint Key is held.\n(WallJumpForce * SprintWallJumpForceMultiplier)")]
    public float sprintWallJumpForceMultiplier;

    [Tooltip("Determines how high Entity will be launched when doing a Wall Jump.")]
    public float wallJumpHeight;

    [Tooltip("Multiplies the Wall Jump Height when Crouch Key is held.\n(WallJumpHeight * CrouchWallJumpHeightMultiplier)")]
    public float crouchWallJumpHeightMultiplier;

    [Tooltip("Multiplies the Wall Jump Height when Sprint Key is held.\n(WallJumpHeight * SprintWallJumpHeightMultiplier)")]
    public float sprintWallJumpHeightMultiplier;

    [Tooltip("Smoothes Wall Jumping.")]
    public float wallJumpSmoothTime;


    [Header("Combat", order = 1)]


    [Tooltip("Determines if Entity can take Damage.")]
    public bool canTakeDamage;

    [Tooltip("Determines if Entity can perform a Melee Attack.")]
    public bool canAttack;

    [Tooltip("Determines if Entity can Shoot.")]
    public bool canShoot;

    [Tooltip("Entity's Health.")]
    public float health;

    [Tooltip("Multiplies all Damage Taken.\n(Damage * ResistanceMultiplier)")]
    public float resistanceMultiplier;

    [Tooltip("Ammount of Damage Entity will do when Attacking.\nThis doesn't affect Weapon's Damage.")]
    public float damage;

    [Tooltip("Multiplies Damage.\n(Damage * DamageMultiplier)")]
    public float damageMultiplier;

    [Tooltip("Length of Entity's Death Animation in Seconds.")]
    public int dieAnimationLength;
    #endregion



    [Header("Base Class Read Only Variables", order = 0)]
    #region Read Only Variables
    [Header("Movement", order = 1)]


    [Tooltip("Determines if Entity is Moving.")]
    [ReadOnlyField] public bool isMoving;

    [Tooltip("Determines if Entity is Looking Backwards.")]
    [ReadOnlyField] public bool isBackwards;

    [Tooltip("Determines if Entity is Moving Backwards.")]
    [ReadOnlyField] public bool isMovingBackwards;

    [Tooltip("Determines if Entity is Crouching.")]
    [ReadOnlyField] public bool isCrouching;

    [Tooltip("Determines if Entity is Sprintint.")]
    [ReadOnlyField] public bool isSprinting;

    [Tooltip("Determines if Entity is Grounded.")]
    [ReadOnlyField] public bool isGrounded;

    [Tooltip("Speed with all Modifiers applied to it.")]
    [ReadOnlyField] public float modifiedSpeed;


    [Header("Jumping", order = 1)]


    [Tooltip("Number of Jumps Left for Entity to use.")]
    [ReadOnlyField] public int jumpsLeft;

    [Tooltip("Jump Height with all Modifiers applied to it.")]
    [ReadOnlyField] public float modifiedJumpHeight;


    [Header("Wall Jumping", order = 1)]


    [Tooltip("Wall Jump Force with all Modifiers applied to it.")]
    [ReadOnlyField] public float modifiedWallJumpForce;

    [Tooltip("Wall Jump Height with all Modifiers applied to it.")]
    [ReadOnlyField] public float modifiedWallJumpHeight;


    [Header("General", order = 1)]


    [Tooltip("Name of the last Portal Entity interacted with.")]
    [ReadOnlyField] public string lastPortalName;

    [Tooltip("Determines if Entity is Taking Damage from Level.")]
    [ReadOnlyField] public bool isTakingDamageFromLevel;

    [Tooltip("Determines if Entity is Taking Damage from another Entity.")]
    [ReadOnlyField] public bool isTakingDamageFromEntity;

    [Tooltip("Index of the Room Entity is located in.")]
    [ReadOnlyField] public int currentRoom;
    #endregion
}
