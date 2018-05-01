using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour {

    private Rigidbody2D rb; 

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-GameControl.instance.scrollSpeed, 0);
	}
	

	void Update () {
		if(GameControl.instance.isDead==true)
        {
            rb.velocity = Vector3.zero;
        }
	}
}
