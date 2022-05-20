using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //Health
    public float Health;
    public float ResistanceMultiplier;
    
    //Speed
    public float Speed;
    public float CrouchSpeedMultiplier;
    public float MovementSmoothTime;

    //Jump
    public float JumpHeight;
    public float CrouchJumpMultiplier;
    public float JumpSmoothTime;
    public int NumberOfJumps;

    //Wall Jump
    public float WallJumpForce;
    public float CrouchWallJumpForceMultiplier;
    public float WallJumpHeight;
    public float CrouchWallJumpHeightMultiplier;
    public float WallJumpSmoothTime;

    //Damage
    public float Damage;
    public float DamageMultiplier;

    //Other
    public int DieAnimationLength;
    public bool CanTeleport;

    #region Hidden Variables
    //Movement
    [HideInInspector] public bool IsBackwards;
    [HideInInspector] public bool IsMovingBackwards;

    //Jumping
    [HideInInspector] public int JumpsLeft;

    //Progress
    [HideInInspector] public int TotalKills;
    [HideInInspector] public float TotalDamageGiven;
    [HideInInspector] public float TotalDamageTaken;
    [HideInInspector] public int TotalBulletsFired;
    [HideInInspector] public int TotalMeleeAttacks;
    [HideInInspector] public int TotalMoney;
    [HideInInspector] public int TotalJumps;
    [HideInInspector] public int TotalWallJumps;
    [HideInInspector] public int TotalTeleportations;

    //Other
    [HideInInspector] public string LastPortalName;
    [HideInInspector] public bool IsTakingDamageFromLevel;
    [HideInInspector] public bool IsTakingDamageFromEnemy;
    [HideInInspector] public string CauseOfDeath;
    [HideInInspector] public int CurrentRoom;
    #endregion

}
