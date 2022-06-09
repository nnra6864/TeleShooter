using UnityEngine;

public class PlayerStats : Stats
{
    //[Header("Player Class Editable Variables", order = 0)]
    #region Editable Variables



    #endregion



    [Header("Player Class Read Only Variables", order = 0)]
    #region Read Only Variables
    //[Header("Movement", order = 1)]


    //[Header("Jumping", order = 1)]


    //[Header("Wall Jumping", order = 1)]


    [Header("Progress", order = 1)]


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


    [Header("General", order = 1)]


    [Tooltip("Name of the object that Caused Death of Player.")]
    [ReadOnlyField] public string causeOfDeath;
    #endregion
}