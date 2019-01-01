using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementDesGardesV2 : MonoBehaviour {

    public float normalSpeed;
    public float bonusSpeed;
    [SerializeField]
    private float speed;

    private FieldOfViewGarde fov;
    public float walkDistance;

    Rigidbody2D rigiBoy;
    Vector2 velocity;

    public Vector2 lastKnownPosition;

    private Vector2 startPosition;
    private Vector2 currentPosition;

    public Transform[] tourDeGarde;
    private int nombreDePositionDuTour;
    private int actuelPositionDuTour;

    private bool canMove = true;
    public bool eardSomething = false;
    private bool isAfraid = false;
    private bool isTargetSeen = false;
    public bool isAgressive = true;
    private bool sawPlayer = false;

    public Transform armyLocation;

    Vector3[] path;
    int targetIndex;
    public Transform target;

    void Start()
    {
        nombreDePositionDuTour = tourDeGarde.Length;
        actuelPositionDuTour = 0;

        speed = normalSpeed;
        startPosition = transform.position;

        rigiBoy = GetComponent<Rigidbody2D>();
        fov = GetComponent<FieldOfViewGarde>();

        StartCoroutine(searchPath());
    }

    // Update is called once per frame
    void Update () {
        currentPosition = new Vector2(transform.position.x, transform.position.y);
        if (fov.visibleCreature.Count > 0 && !isAfraid)
        {
            isAfraid = true;
            speed = normalSpeed + bonusSpeed;
            if (actuelPositionDuTour - 1 >= 0)
            {
                actuelPositionDuTour--;
            }
            else
            {
                actuelPositionDuTour = tourDeGarde.Length - 1;
            }

            canMove = true;
        }
        else if (fov.visiblePlayer.Count > 0 && !isAfraid && !sawPlayer)
        {
            speed = normalSpeed + bonusSpeed;
            if (isAgressive)
            {
                lastKnownPosition = fov.visiblePlayer[0].position;
                isTargetSeen = true;
            }
            else
            {
                sawPlayer = true;
            }
        }
        else if (eardSomething && !isAfraid)
        {
            speed = normalSpeed;
            if(Vector2.Distance(lastKnownPosition, currentPosition) <= walkDistance)
            {
                eardSomething = false;
            }
        }
        else if (isTargetSeen)
        {
            if (Vector2.Distance(lastKnownPosition, currentPosition) <= walkDistance)
            {
                StartCoroutine(waitHere(5));
                isTargetSeen = false;
            }
        }
        else if(sawPlayer)
        {
            StartCoroutine(waitHere(2));
            lastKnownPosition = armyLocation.position;
        }
        else if (canMove)
        {
            if (isAfraid)
            {
                speed = normalSpeed + bonusSpeed;
            }
            else
            {
                speed = normalSpeed;
            }
            faitSaRonde();
        }
        else
        {
            speed = 0;
        }
	}

    Vector2 beforeLastPosition = Vector2.zero;

    IEnumerator searchPath()
    {
        if(beforeLastPosition != lastKnownPosition)
        {
            PathRequestManager.RequestPath(transform.position, lastKnownPosition, OnPathFound);
        }
        beforeLastPosition = lastKnownPosition;
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(searchPath());
    }

    void faitSaRonde()
    {
        if (Vector2.Distance(lastKnownPosition, currentPosition) <= walkDistance)
        {
            if (isAfraid)
            {
                isAfraid = false;
                StartCoroutine(waitHere(2));
            }
            else
            {
                actuelPositionDuTour++;
                if (actuelPositionDuTour >= nombreDePositionDuTour)
                {
                    actuelPositionDuTour = 0;
                }
                StartCoroutine(waitHere(2));
            }
        }
        lastKnownPosition = tourDeGarde[actuelPositionDuTour].position;
    }

    IEnumerator waitHere(float secondToWait)
    {
        canMove = false;
        yield return new WaitForSeconds(secondToWait);
        canMove = true;
    }

   

    // Pathfinding

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        while (true)
        {
            if (Vector2.Distance(currentWaypoint, currentPosition) <= 0.5f)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
                velocity = Vector2.zero;
            }
            else if (Vector2.Distance(lastKnownPosition, currentPosition) > walkDistance)
            {
                velocity = new Vector2(currentWaypoint.x - transform.position.x, currentWaypoint.y - transform.position.y).normalized * speed;
            }
            else
            {
                velocity = Vector2.zero;
            }

            //transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            
            yield return null;

        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rigiBoy.MovePosition(rigiBoy.position + velocity * Time.fixedDeltaTime);
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }

        Color newColor = Color.blue;
        newColor.a = 0.2f;
        Gizmos.color = newColor;
        Gizmos.DrawSphere(transform.position, -walkDistance);
    }
}
