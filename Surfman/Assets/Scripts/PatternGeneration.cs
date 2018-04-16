using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternGeneration : MonoBehaviour {

    public GameObject[] obj;
    public GameObject patternSpawn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            Transform tr = patternSpawn.GetComponent<Transform>();
            Instantiate(obj[Random.Range(0, obj.GetLength(0))], tr.position, Quaternion.identity);
        }

    }
}
