using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    Camera cursorCamera;
    ParticleSystem particles;

    private void Awake()
    {
        cursorCamera = GameObject.Find("Cursor Camera").GetComponent<Camera>();
        particles = GetComponentInChildren<ParticleSystem>();
        Cursor.visible = false;
    }
    void Update()
    {
        transform.position = new Vector3(cursorCamera.ScreenToWorldPoint(Input.mousePosition).x, cursorCamera.ScreenToWorldPoint(Input.mousePosition).y - 0.78f, transform.position.z);
        if (Input.GetKeyDown(KeyCode.Mouse0)) OnClick();
    }

    void OnClick() 
    {
        particles.Play();
    }
}
