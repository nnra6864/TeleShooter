using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleStats : MonoBehaviour
{
    [Tooltip("Damage dealt when colliding with this Obstacle.")]
    public float Damage;

    [Tooltip("Cooldown before dealing damage again.")]
    public float Cooldown;
}
