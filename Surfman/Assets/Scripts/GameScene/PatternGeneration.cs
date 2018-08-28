using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternGeneration : MonoBehaviour {

    public GameObject patternSpawn;
    private bool spawned = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject[] obj = GameControl.instance.FetchPatterns();

        if (collision.gameObject.tag == "player" && spawned == false)
        {
            Transform tr = patternSpawn.GetComponent<Transform>();
            Instantiate(obj[Random.Range(0, obj.GetLength(0))], tr.position, Quaternion.identity);
            spawned = true;
        }

    }
}
