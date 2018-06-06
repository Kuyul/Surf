using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour {

    public Transform playerTransform;

    private SpriteRenderer sprite;

	void Start () {

        sprite = GetComponent<SpriteRenderer>();
        Debug.Log(-sprite.bounds.extents.x);
        Debug.Log(sprite.bounds.extents.x);

    }

    void Update()
    {
        if(playerTransform.position.x >= transform.position.x+sprite.bounds.extents.x*2)
        {

            Vector3 newTransform = new Vector3(sprite.bounds.extents.x * 4 + transform.position.x, 25,20);
            transform.position = newTransform;
           
        }
    }
}
