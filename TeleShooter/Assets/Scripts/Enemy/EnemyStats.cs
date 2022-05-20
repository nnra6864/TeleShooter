using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    //Health
    public float Health;
    public float ResistanceMultiplier;

    //Movement
    public bool CanMove = false;
    public float Speed;

    //Jump
    public float JumpHeight;
    public float ObstacleJumpHeight;
    public int NumberOfJumps;

    //Combat
    public float Damage;

    //Other
    public int StuckTime;
    public int DieAnimationLength;
    public bool CanTeleport;
    public float RandomDistanceFromPointLowerLimit;
    public float RandomDistanceFromPointUpperLimit;

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
