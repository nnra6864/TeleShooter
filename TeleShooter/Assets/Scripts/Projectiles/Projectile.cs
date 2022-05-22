using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Dissapear());
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * 25;
    }

    IEnumerator Dissapear() 
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
