using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewMesh : MonoBehaviour {

    Mesh mesh;
    FieldOfViewGarde fow;
    RaycastHit2D hit;
    Vector3[] vertices;
    int[] triangles;
    public float meshResolution;
    int stepCount;

	// Use this for initialization
	void Start () {
        mesh = GetComponent<MeshFilter>().mesh;
        fow = GetComponentInParent<FieldOfViewGarde>();
	}
	
	// Update is called once per frame
	void Update () {
        MakeMesh();
	}

    void MakeMesh()
    {
        stepCount = Mathf.RoundToInt(fow.viewAngle * meshResolution);
        float stepAngle = fow.viewAngle / stepCount;

        List<Vector3> viewVertex = new List<Vector3>();

        hit = new RaycastHit2D();

        for(int i = 0; i<=stepCount; i++)
        {
            float angle = fow.transform.eulerAngles.y - fow.viewAngle / 2 + stepAngle * i;
            Vector3 dir = fow.dirFromAngle(angle);
            hit = Physics2D.Raycast(fow.transform.position, dir, fow.distanceVue, fow.obstacleMask);

            if(hit.collider == null)
            {
                viewVertex.Add(transform.position + dir.normalized * fow.distanceVue);
            }
            else
            {
                viewVertex.Add(transform.position + dir.normalized * hit.distance);
            }
        }

        int vertexCount = viewVertex.Count - 1;

        vertices = new Vector3[vertexCount];
        triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;

        for(int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewVertex[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 2] = i + 1;
                triangles[i * 3 + 1] = i + 2;
            }
        }

        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
