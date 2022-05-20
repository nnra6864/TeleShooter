using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallCheck : MonoBehaviour
{
    public PlayerStats playerStats;
    public Rigidbody rb;

    public bool canWallJump;
    public Vector3 originalPosition;

    private void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();
        rb = GetComponentInParent<Rigidbody>();

        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        if (playerStats.IsMovingBackwards)
        {
            transform.localPosition = new Vector3(-originalPosition.x, transform.localPosition.y, transform.localPosition.z);
        }
        else
        {
            transform.localPosition = new Vector3(originalPosition.x, transform.localPosition.y, transform.localPosition.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Layer 7 is Enemy layer, have to use layer bcs enemy checks don't have enemy tag
        if (other.tag != "Player" && other.gameObject.layer != 7 && other.tag != "Portal" && other.tag != "EnemySpawnPoint")
        {
            canWallJump = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player" && other.gameObject.layer != 7 && other.tag != "Portal" && other.tag != "EnemySpawnPoint")
        {
            canWallJump = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player" && other.gameObject.layer != 7 && other.tag != "Portal" && other.tag != "EnemySpawnPoint")
        {
            canWallJump = false;
        }
    }
}
