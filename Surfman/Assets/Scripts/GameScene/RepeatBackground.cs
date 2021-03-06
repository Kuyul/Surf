﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour {

    public Transform playerTransform;

    private SpriteRenderer sprite;

	void Start () {

        sprite = GetComponent<SpriteRenderer>();
        
        //Debug.Log(-sprite.bounds.extents.x);
        //Debug.Log(sprite.bounds.extents.x);

        if (gameObject.tag == "Wave1duplicate")
        {
            Vector3 newTransform = new Vector3(sprite.bounds.extents.x * 2 + transform.position.x, transform.position.y, transform.position.z);
            transform.position = newTransform;
        }

        if (gameObject.tag == "Wave2duplicate")
        {
            Vector3 newTransform = new Vector3(sprite.bounds.extents.x * 2 + transform.position.x, transform.position.y, transform.position.z);
            transform.position = newTransform;
        }

        if (gameObject.tag == "Wave3duplicate")
        {
            Vector3 newTransform = new Vector3(sprite.bounds.extents.x * 2 + transform.position.x, transform.position.y, transform.position.z);
            transform.position = newTransform;
        }

        if (gameObject.tag == "Wave4duplicate")
        {
            Vector3 newTransform = new Vector3(sprite.bounds.extents.x * 2 + transform.position.x, transform.position.y, transform.position.z);
            transform.position = newTransform;
        }

        if (gameObject.tag == "Wave5duplicate")
        {
            Vector3 newTransform = new Vector3(sprite.bounds.extents.x * 2 + transform.position.x, transform.position.y, transform.position.z);
            transform.position = newTransform;
        }

    }

    void Update()
    {
        if(playerTransform.position.x >= transform.position.x+sprite.bounds.extents.x*2 && gameObject.tag=="Background")
        {
            Vector3 newTransform = new Vector3(sprite.bounds.extents.x * 4 + transform.position.x - 3, transform.position.y, transform.position.z);
            transform.position = newTransform;  
        }

        if (playerTransform.position.x >= transform.position.x + sprite.bounds.extents.x * 2 && (gameObject.tag == "Wave1" || gameObject.tag == "Wave1duplicate"))
        {
            Vector3 newTransform = new Vector3(sprite.bounds.extents.x * 4 + transform.position.x, transform.position.y, transform.position.z);
            transform.position = newTransform;
        }

        if (playerTransform.position.x >= transform.position.x + sprite.bounds.extents.x * 2 && (gameObject.tag == "Wave2" || gameObject.tag == "Wave2duplicate"))
        {
            Vector3 newTransform = new Vector3(sprite.bounds.extents.x * 4 + transform.position.x, transform.position.y, transform.position.z);
            transform.position = newTransform;
        }

        if (playerTransform.position.x >= transform.position.x + sprite.bounds.extents.x * 2 && (gameObject.tag == "Wave3" || gameObject.tag == "Wave3duplicate"))
        {
            Vector3 newTransform = new Vector3(sprite.bounds.extents.x * 4 + transform.position.x, transform.position.y, transform.position.z);
            transform.position = newTransform;
        }

        if (playerTransform.position.x >= transform.position.x + sprite.bounds.extents.x * 2 && (gameObject.tag == "Wave4" || gameObject.tag == "Wave4duplicate"))
        {
            Vector3 newTransform = new Vector3(sprite.bounds.extents.x * 4 + transform.position.x, transform.position.y, transform.position.z);
            transform.position = newTransform;
        }

        if (playerTransform.position.x >= transform.position.x + sprite.bounds.extents.x * 2 && (gameObject.tag == "Wave5" || gameObject.tag == "Wave5duplicate"))
        {
            Vector3 newTransform = new Vector3(sprite.bounds.extents.x * 4 + transform.position.x, transform.position.y, transform.position.z);
            transform.position = newTransform;
        }

        if (playerTransform.position.x >= transform.position.x + 25 && gameObject.tag == "boat")
        {
            Vector3 newTransform = new Vector3(playerTransform.position.x + 55, transform.position.y, transform.position.z);
            transform.position = newTransform;
        }

        if (playerTransform.position.x >= transform.position.x + 25 && gameObject.tag == "cloud1")
        {
            Vector3 newTransform = new Vector3(playerTransform.position.x + 35, transform.position.y, transform.position.z);
            transform.position = newTransform;
        }

        if (playerTransform.position.x >= transform.position.x + 25 && gameObject.tag == "cloud2")
        {
            Vector3 newTransform = new Vector3(playerTransform.position.x + 35, transform.position.y, transform.position.z);
            transform.position = newTransform;
        }

        if (playerTransform.position.x >= transform.position.x + 25 && gameObject.tag == "cloud3")
        {
            Vector3 newTransform = new Vector3(playerTransform.position.x + 35, transform.position.y, transform.position.z);
            transform.position = newTransform;
        }

        if (playerTransform.position.x >= transform.position.x + 30 && gameObject.tag == "island")
        {
            Vector3 newTransform = new Vector3(playerTransform.position.x + 50, transform.position.y, transform.position.z);
            transform.position = newTransform;
        }
    }
}
