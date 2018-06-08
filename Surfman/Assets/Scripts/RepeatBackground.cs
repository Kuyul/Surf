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
        if(playerTransform.position.x >= transform.position.x+sprite.bounds.extents.x*2 && gameObject.tag=="Background")
        {
            Vector3 newTransform = new Vector3(sprite.bounds.extents.x * 4 + transform.position.x, 25,20);
            transform.position = newTransform;  
        }

        if (playerTransform.position.x >= transform.position.x + sprite.bounds.extents.x * 2 && gameObject.tag == "Wave1")
        {
            Vector3 newTransform = new Vector3(sprite.bounds.extents.x * 4 + transform.position.x, (float)-3.56, 0);
            transform.position = newTransform;
        }

        if (playerTransform.position.x >= transform.position.x + sprite.bounds.extents.x * 2 && gameObject.tag == "Wave2")
        {
            Vector3 newTransform = new Vector3(sprite.bounds.extents.x * 4 + transform.position.x, (float)-2.59, 0);
            transform.position = newTransform;
        }

        if (playerTransform.position.x >= transform.position.x + sprite.bounds.extents.x * 2 && gameObject.tag == "Wave3")
        {
            Vector3 newTransform = new Vector3(sprite.bounds.extents.x * 4 + transform.position.x, (float)-1.1, 0);
            transform.position = newTransform;
        }

        if (playerTransform.position.x >= transform.position.x + sprite.bounds.extents.x * 2 && gameObject.tag == "Wave4")
        {
            Vector3 newTransform = new Vector3(sprite.bounds.extents.x * 4 + transform.position.x, (float)-0.17, 0);
            transform.position = newTransform;
        }

        if (playerTransform.position.x >= transform.position.x + sprite.bounds.extents.x * 2 && gameObject.tag == "Wave5")
        {
            Vector3 newTransform = new Vector3(sprite.bounds.extents.x * 4 + transform.position.x, (float)0.97, 0);
            transform.position = newTransform;
        }
    }
}
