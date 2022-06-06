using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallCheck : MonoBehaviour
{
    PlayerStats playerStats;

    [Tooltip("If Disabled, Player won't be able to Wall Jump.")]
    public bool canWallJump;
    Vector3 originalPosition;

    private void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        var ogPosX = playerStats.isMovingBackwards ? -originalPosition.x : originalPosition.x;
        transform.localPosition = new Vector3(ogPosX, transform.localPosition.y, transform.localPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Layer 7 is Enemy layer, have to use layer bcs enemy checks don't have enemy tag
        canWallJump = (other.tag != "Player" && other.gameObject.layer != 7 && other.tag != "Portal" && other.tag != "EnemySpawnPoint");
    }

    private void OnTriggerStay(Collider other)
    {
        canWallJump = (other.tag != "Player" && other.gameObject.layer != 7 && other.tag != "Portal" && other.tag != "EnemySpawnPoint");
    }

    private void OnTriggerExit(Collider other)
    {
        canWallJump = (other.tag != "Player" && other.gameObject.layer != 7 && other.tag != "Portal" && other.tag != "EnemySpawnPoint");
    }
}
