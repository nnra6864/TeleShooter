using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallCheck : MonoBehaviour
{
    PlayerStats playerStats;

    [Tooltip("If Disabled, Player won't be able to Wall Jump.")]
    public bool shouldWallJump;

    [Tooltip("Must have the exact same name as tag you'd like to filter.")]
    public List<string> ignoreTags;

    [Tooltip("Must have the exact same name as layer you'd like to filter.")]
    public List<string> ignoreLayers;

    Vector3 originalPosition;

    private void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        transform.localPosition = new Vector3(playerStats.isMovingBackwards ? -originalPosition.x : originalPosition.x, transform.localPosition.y, transform.localPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var tag in ignoreTags) { if (other.CompareTag(tag)) return; }
        foreach (var layer in ignoreLayers) { if (other.gameObject.layer == LayerMask.NameToLayer(layer)) return; }
        shouldWallJump = true;
    }

    private void OnTriggerStay(Collider other)
    {
        foreach (var tag in ignoreTags) { if (other.CompareTag(tag)) return; }
        foreach (var layer in ignoreLayers) { if (other.gameObject.layer == LayerMask.NameToLayer(layer)) return; }
        shouldWallJump = true;
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (var tag in ignoreTags) { if (other.CompareTag(tag)) return; }
        foreach (var layer in ignoreLayers) { if (other.gameObject.layer == LayerMask.NameToLayer(layer)) return; }
        shouldWallJump = false;
    }
}
