using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    PlayerStats playerStats;
    PlayerMovement playerMovement;

    [Tooltip("Must have the exact same name as tag you'd like to filter.")]
    public List<string> ignoreTags;

    [Tooltip("Must have the exact same name as layer you'd like to filter.")]
    public List<string> ignoreLayers;

    int numberOfJumps;

    private void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();
        numberOfJumps = playerStats.numberOfJumps;

        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var tag in ignoreTags) { if (other.CompareTag(tag)) return; }
        foreach (var layer in ignoreLayers) { if (other.gameObject.layer == LayerMask.NameToLayer(layer)) return; }

        playerStats.jumpsLeft = numberOfJumps;
    }
}