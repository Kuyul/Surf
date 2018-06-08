using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour {

    private Rigidbody2D rb; 

	void Start () {
        rb = GetComponent<Rigidbody2D>();

        if (gameObject.tag == "Background")
        {
            rb.velocity = new Vector2(-GameControl.instance.scrollSpeed, 0);
        }

        if (gameObject.tag == "Wave1")
        {
            rb.velocity = new Vector2(-GameControl.instance.waveOneSpeed, 0);
        }

        if (gameObject.tag == "Wave2")
        {
            rb.velocity = new Vector2(-GameControl.instance.waveTwoSpeed, 0);
        }

        if (gameObject.tag == "Wave3")
        {
            rb.velocity = new Vector2(-GameControl.instance.waveThreeSpeed, 0);
        }

        if (gameObject.tag == "Wave4")
        {
            rb.velocity = new Vector2(-GameControl.instance.waveFourSpeed, 0);
        }

        if (gameObject.tag == "Wave5")
        {
            rb.velocity = new Vector2(-GameControl.instance.waveFiveSpeed, 0);
        }

    }
	

	void Update () {
		if(GameControl.instance.isDead==true && gameObject.tag=="Background")
        {
            rb.velocity = Vector3.zero;
        }
	}
}
