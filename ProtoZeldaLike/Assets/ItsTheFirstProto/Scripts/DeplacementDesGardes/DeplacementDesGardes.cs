using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementDesGardes : MonoBehaviour {

    public float normalSpeed;
    public float bonusSpeed;
    [SerializeField]
    private float speed;

    private FieldOfViewGarde fov;
    public float walkDistance;

    Rigidbody2D rigiBoy;
    Vector2 velocity;

    bool isTargetSeen;
    Transform target;

    public Vector2 lastKnownPosition;

    private Vector2 startPosition;

    public Transform[] tourDeGarde;
    private int nombreDePositionDuTour;
    private int actuelPositionDuTour;

    private bool estDeGarde = true;
    private bool revientEnGarde = false;
    private bool isWaiting = false;
    private bool isAffraid = false;

	void Start () {
        nombreDePositionDuTour = tourDeGarde.Length;
        actuelPositionDuTour = 0;

        speed = normalSpeed;
        startPosition = transform.position;

        rigiBoy = GetComponent<Rigidbody2D>();
        fov = GetComponent<FieldOfViewGarde>();

        lastKnownPosition = transform.position;
	}


	void Update () {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);

        if (!isAffraid) {
            if (fov.visibleCreature.Count > 0)
            {
                isAffraid = true;
                actuelPositionDuTour--;
                if (actuelPositionDuTour < 0)
                {
                    actuelPositionDuTour = nombreDePositionDuTour - 1;
                }
                lastKnownPosition = tourDeGarde[actuelPositionDuTour].position;
            }
            else if (fov.visiblePlayer.Count > 0 && target == null)
            {
                target = fov.visiblePlayer[0];
            }
            else if (target != null)
            {
                revientEnGarde = true;
                StartCoroutine(chercheAlentour());
                target = null;
            }
        }

        if(target!= null)
        {
            startPosition = transform.position;
            estDeGarde = false;
            isTargetSeen = true;
        }
        else
        {
            isTargetSeen = false;
        }

        if(isTargetSeen)
        {
            lastKnownPosition = new Vector2(target.position.x, target.position.y);
        }

        if (Vector2.Distance(lastKnownPosition, currentPosition) > walkDistance)
        {
            if(isTargetSeen || isAffraid)
            {
                speed = normalSpeed + bonusSpeed;
            }
            else
            {
                speed = normalSpeed;
            }
            velocity = new Vector2(lastKnownPosition.x - currentPosition.x, lastKnownPosition.y - currentPosition.y).normalized * speed;
        }
        else if (isAffraid)
        {
            lastKnownPosition = tourDeGarde[actuelPositionDuTour].position;
            if (Vector2.Distance(lastKnownPosition, currentPosition) <= walkDistance)
            {
                isAffraid = false;
                StartCoroutine(chercheAlentour());
            }
        }
        else if (isWaiting)
        {
            velocity = Vector2.zero;
        }
        else if (revientEnGarde)
        {
            revientEnGarde = false;
            estDeGarde = true;
            lastKnownPosition = tourDeGarde[actuelPositionDuTour].position;
        }
        else if (estDeGarde)
        {
            ilEstDeGarde();
        }
        else
        {
            velocity = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        rigiBoy.MovePosition(rigiBoy.position + velocity * Time.fixedDeltaTime);
    }

    void ilEstDeGarde()
    {
        actuelPositionDuTour++;
        if(actuelPositionDuTour>=nombreDePositionDuTour)
        {
            actuelPositionDuTour = 0;
        }
        lastKnownPosition = tourDeGarde[actuelPositionDuTour].position;
    }

    private void OnDrawGizmos()
    {
        Color newColor = Color.blue;
        newColor.a = 0.2f;
        Gizmos.color = newColor;
        Gizmos.DrawSphere(transform.position, -walkDistance);
    }

    IEnumerator chercheAlentour()
    {
        isWaiting = true;
        yield return new WaitForSeconds(5f);
        isWaiting = false;
    }
}
