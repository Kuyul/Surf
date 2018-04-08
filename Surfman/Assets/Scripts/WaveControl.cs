using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveControl : MonoBehaviour {

    private Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(-5.0f, 0.0f, 0.0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
