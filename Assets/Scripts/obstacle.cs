using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class obstacle : MonoBehaviour
{
    public float speed = 4.0f;
    private Rigidbody2D rb;
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
    }

    private Boolean checkPos()
    {
        Vector2 screenPosition = _cam.WorldToScreenPoint(transform.position);
        if (screenPosition.x < -5)
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        // if(transform.position.x < screenBounds.x * 2){
        //     Destroy(this.gameObject);
        // }
        if (checkPos())
        {
            Destroy(this.gameObject);
        }
    }
}
