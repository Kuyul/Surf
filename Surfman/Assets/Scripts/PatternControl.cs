using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternControl : MonoBehaviour {

    private Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameControl.instance.isDead == true)
        {
            rb.velocity = Vector3.zero;
        }
    }

}
