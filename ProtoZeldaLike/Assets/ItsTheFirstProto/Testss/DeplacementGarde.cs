using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeplacementGarde : MonoBehaviour {

    public float speed;
    public float distanceVue;
    public float distanceOuie;

    public List<Transform> tourDeGarde;

    private Vector2 velocity;
    private Rigidbody2D rigiBoy;

    public LayerMask targetMask;
    public LayerMask obstalceMask;

    public List<Transform> visibleTargets = new List<Transform>();

    public float meshResolution;

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

	// Use this for initialization
	void Start () {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        rigiBoy = GetComponent<Rigidbody2D>();

        StartCoroutine(FindTargetWithDelay(0.2f));
	}
	
	// Update is called once per frame
	void Update () {
        DrawFieldOfView();
	}

    IEnumerator FindTargetWithDelay(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTarget();
        }
    }

    void FindVisibleTarget()
    {
        visibleTargets.Clear();
        Collider2D[] targetInViewRadius = Physics2D.OverlapAreaAll(transform.position-new Vector3(distanceVue, distanceVue, 0), transform.position + new Vector3(distanceVue, distanceVue, 0), targetMask);
        for (int i = 0; i< targetInViewRadius.Length; i++)
        {
            Transform target = targetInViewRadius[i].transform;
            Vector3 dirTarget = (target.position - transform.position).normalized;
            float distTarget = Vector3.Distance(transform.position, target.position);

            if(!Physics2D.Raycast(transform.position, dirTarget,distTarget, obstalceMask))
            {
                if (distTarget <= distanceVue)
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(360 * meshResolution);
        float stepAngleSize = 360f / stepCount;
        List<Vector3> viewPoint = new List<Vector3>();

        for(int i = 0; i<=stepCount; i++)
        {
            float angle = stepAngleSize * i;

            ViewCastInfo newViewCast = ViewCast();
            viewPoint.Add(newViewCast.point);
            //Debug.DrawLine(transform.position, transform.position + new Vector3(dirFromAngle(angle).x, dirFromAngle(angle).y, 0) * distanceVue, Color.red);
        }

        int vertexCount = viewPoint.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;

        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoint[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    ViewCastInfo ViewCast()
    {
        Vector3 dir = dirFromAngle(360);
        RaycastHit hit;

        if(Physics.Raycast(transform.position, dir, out hit, 360, obstalceMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * distanceVue, 360);
        }

    }

    public Vector2 dirFromAngle(float angleInDegrees)
    {
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dist;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dist)
        {
            hit = _hit;
            point = _point;
            dist = _dist;
        }
    }
}
