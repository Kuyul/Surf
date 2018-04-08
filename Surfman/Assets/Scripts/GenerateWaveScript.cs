using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWaveScript : MonoBehaviour {

    [Range (0.1f, 20.0f)]
    public float heighScale = 5.0f;
    [Range(0.1f, 40.0f)]
    public float detailScale = 5.0f;

    private Mesh myMesh;
    private Vector3[] vertices;

    private void Update()
    {
        GenerateWave();
    }

    void GenerateWave()
    {
        myMesh = this.GetComponent<MeshFilter>().mesh;
        vertices = myMesh.vertices;

        int xlevel = 0;///
        int yLevel = 0;///

        for(int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                CalculationMethod(xlevel, yLevel); //changing vertices here and applying them later
                xlevel++;
            }
            yLevel++;
        }

        myMesh.vertices = vertices;
        myMesh.RecalculateBounds();
        myMesh.RecalculateNormals();

        Destroy(gameObject.GetComponent<MeshCollider>());
        MeshCollider collider = gameObject.AddComponent<MeshCollider>();
        collider.sharedMesh = null;
        collider.sharedMesh = myMesh;
    }

    public bool waves = false;
    public float wavesSpeed = 5.0f;
    void CalculationMethod(int i, int j)
    {
        if (waves)
        {
            vertices[i].z = Mathf.PerlinNoise(
                Time.time/wavesSpeed + 
                (vertices[i].x + transform.position.x) / detailScale,
                Time.time/wavesSpeed +
                (vertices[i].y + transform.position.y) / detailScale)
                * heighScale;
            vertices[i].z -= j;
        }
        else if (!waves)
        {
            vertices[i].z = Mathf.PerlinNoise(
                (vertices[i].x + transform.position.x) / detailScale,
                (vertices[i].y + transform.position.y) / detailScale)
                * heighScale;
            vertices[i].z -= j;
        }
    }
}
