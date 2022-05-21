using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    PlayerStats playerStats;
    PlayerMovement playerMovement;

    int numberOfJumps;

    private void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();
        numberOfJumps = playerStats.NumberOfJumps;

        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Player" && other.tag != "Portal" && other.tag != "EnemySpawnPoint")
        {
            playerStats.JumpsLeft = numberOfJumps;
        }
    }
}