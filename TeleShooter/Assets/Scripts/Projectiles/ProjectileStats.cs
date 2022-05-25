using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStats : MonoBehaviour
{
    #region Editable Variables
    [Tooltip("Ammount of Damage the Projectile will do on impact.")]
    public float damage;

    [Tooltip("Traveling Speed of the Projectile.")]
    public float speed;

    [Tooltip("Determines if Projectile can teleport.")]
    public bool canTeleport;

    [Tooltip("Determines how long it will take for Projectile to dissapear.\n(Time in Seconds)")]
    public float lifeTime;
    #endregion

    #region Read Only Variables
    [Tooltip("Set to true if shot by Player and false if shot by Enemy.")]
    [ReadOnlyField] public bool shotByPlayer;

    [Tooltip("Pass your PlayerStats/EnemyStats damage multiplier.")]
    [ReadOnlyField] public float damageMultiplier;

    [Tooltip("Name of the Last Portal Projectile interacted with.")]
    [ReadOnlyField] public string lastPortalName;

    [Tooltip("Determines if Projectile has Teleported.")]
    [ReadOnlyField] public bool hasTeleported;
    #endregion
}
