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

    void Start()
    {
        nombreDePositionDuTour = tourDeGarde.Length;
        actuelPositionDuTour = 0;

        speed = normalSpeed;
        startPosition = transform.position;

        rigiBoy = GetComponent<Rigidbody2D>();
        fov = GetComponent<FieldOfViewGarde>();

        lastKnownPosition = transform.position;
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
        else if (fov.visiblePlayer.Count > 0 && !isAfraid)
        {
            speed = normalSpeed + bonusSpeed;
            lastKnownPosition = fov.visiblePlayer[0].position;
            isTargetSeen = true;
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
                isTargetSeen = false;
                StartCoroutine(waitHere(5));
            }
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

        if (Vector2.Distance(lastKnownPosition, currentPosition) > walkDistance)
        {
            velocity = new Vector2(lastKnownPosition.x - currentPosition.x, lastKnownPosition.y - currentPosition.y).normalized * speed;
        }
        else
        {
            velocity = Vector2.zero;
        }
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

    private void FixedUpdate()
    {
        rigiBoy.MovePosition(rigiBoy.position + velocity * Time.fixedDeltaTime);
    }

    private void OnDrawGizmos()
    {
        Color newColor = Color.blue;
        newColor.a = 0.2f;
        Gizmos.color = newColor;
        Gizmos.DrawSphere(transform.position, -walkDistance);
    }
}
