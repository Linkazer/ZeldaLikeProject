using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RondeSoldat : MonoBehaviour {

    public float speed = 5;

    Vector2 velocity;
    Rigidbody2D rigiBoy;

    public List<Vector2> pointsRonde;
    public List<Transform> pointRonde3D;
    private int nextPoint=0;
    private Vector2 currentTarget;

    private Vector2 currentPos2D;

    public bool testVit;

    // Use this for initialization
    void Start()
    {
        rigiBoy = GetComponent<Rigidbody2D>();
        foreach(Transform point in pointRonde3D)
        {
            pointsRonde.Add(new Vector2(point.position.x, point.position.y));
        }
        currentTarget = pointsRonde[0];
        transform.position = new Vector3(currentTarget.x, currentTarget.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        currentPos2D = new Vector2(transform.position.x, transform.position.y);
        /*if (currentPos2D != pointsRonde[nextPoint])
        {
            currentTarget = pointsRonde[nextPoint];
            velocity = new Vector2(pointsRonde[nextPoint].x - currentPos2D.x, pointsRonde[nextPoint].y - currentPos2D.y).normalized * speed;
        }
        Vector2 targetAroundPlus = new Vector2(currentTarget.x + velocity.x * Time.fixedDeltaTime, currentTarget.y + velocity.y * Time.fixedDeltaTime);
        Vector2 targetAroundMinus = new Vector2(currentTarget.x - velocity.x * Time.fixedDeltaTime, currentTarget.y - velocity.y * Time.fixedDeltaTime);
        if ((currentPos2D.x < targetAroundMinus.x && currentPos2D.y < targetAroundMinus.y) || (currentPos2D.x > targetAroundPlus.x && currentPos2D.y > targetAroundPlus.y))
        {
            nextPoint++;
            if (nextPoint > pointsRonde.Count - 1)
            {
                nextPoint = 0;
            }
        }*/
        if (currentPos2D == currentTarget)
        {
            nextPoint++;
            if (nextPoint > pointsRonde.Count - 1)
            {
                nextPoint = 0;
            }
            currentTarget = pointsRonde[nextPoint];
        }
        if (!testVit)
        {
            transform.position = Vector2.MoveTowards(currentPos2D, currentTarget, speed * Time.deltaTime);
        }

    }

    private void FixedUpdate()
    {
        if(testVit)
            transform.position = Vector2.MoveTowards(currentPos2D, currentTarget, speed * Time.fixedDeltaTime);
        //rigiBoy.MovePosition(rigiBoy.position + velocity * Time.fixedDeltaTime);
    }
}
