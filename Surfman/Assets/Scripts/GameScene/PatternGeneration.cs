using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternGeneration : MonoBehaviour {

    public GameObject patternSpawn;
    private bool spawned = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player" && spawned == false)
        {
            GameObject pattern = PatternControl.Instance.FeedPattern();
            Transform tr = patternSpawn.GetComponent<Transform>();
            Instantiate(pattern, tr.position, Quaternion.identity);
            spawned = true;
        }

    }
}
